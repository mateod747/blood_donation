using bloodDonation.Common;
using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class PasswordHash : IPasswordHash
    {
        public int SaltByteSize = 24;
        public int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public int Pbkdf2Iterations = 1000;
        public int IterationIndex = 0;
        public int SaltIndex = 1;
        public int Pbkdf2Index = 2;

        private readonly string connectionString = @"Server=mdubinjak;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<bool> HashPassword(string username, string password, Guid donorId, bool admin)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);

            var hashedPassword = Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);

            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Insert into LoginData values(@donorId,
                                                                @username, 
                                                                @hash,
                                                                @admin)";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@donorId", donorId.ToString());
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@hash", hashedPassword);
                command.Parameters.AddWithValue("@admin", Convert.ToInt32(admin));

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<(string, Guid, bool)> ValidatePassword(string username, string password)
        {
            var donors = new List<LoginData>();
            var connectionString = @"Server=mdubinjak;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (
                SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from LoginData where username = @username;";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();

                    try
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                donors.Add(
                                    new LoginData()
                                    {
                                        DonorID = Guid.Parse(reader["donorID"].ToString()),
                                        Username = reader["username"].ToString(),
                                        PasswordHash = reader["passwordHash"].ToString(),
                                        Admin = (bool)reader["admin"]
                                    });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            char[] delimiter = { ':' };

            foreach (var donor in donors)
            {
                var split = donor.PasswordHash.Split(delimiter);
                var iterations = Int32.Parse(split[IterationIndex]);
                var salt = Convert.FromBase64String(split[SaltIndex]);
                var hash = Convert.FromBase64String(split[Pbkdf2Index]);

                var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);

                if (SlowEquals(hash, testHash))
                {
                    return (donor.Admin ? JWTAuth.GenerateToken("Admin") : JWTAuth.GenerateToken("User"), donor.DonorID, donor.Admin);
                }
            }
            return ("", Guid.Empty, false);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}

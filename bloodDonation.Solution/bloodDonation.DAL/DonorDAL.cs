using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class DonorDAL: IDonorDAL
    {
        private readonly string connectionString = @"Server=mdubinjak;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<DonorModel> GetDonor(Guid id)
        {
            var donor = new DonorModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from Donor where donorID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        donor.FirstName = reader["firstName"].ToString();
                        donor.LastName = reader["lastName"].ToString();
                        donor.DonorID = Guid.Parse(reader["donorID"].ToString());
                        donor.Address = reader["address"].ToString();
                        donor.Email = reader["email"].ToString();
                        donor.Phone = reader["phone"].ToString();
                        donor.BloodType = reader["bloodType"].ToString();
                        donor.Gender = (Gender)reader["gender"];
                        donor.Age = (int)reader["age"];
                    }
                }
            }
            return donor;
        }

        public async Task<Guid> GetDonorIdByUsername(string username)
        {
            var donorId = Guid.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select donorID from LoginData where username = @username;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        donorId = Guid.Parse(reader["donorID"].ToString());
                    }
                }
            }
            return donorId;
        }



        public async Task<bool> PostDonor(DonorModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Insert into Donor values(@id,
                                                                @firstName, 
                                                                @lastName, 
                                                                @gender,
                                                                @address, 
                                                                @email,
                                                                @phone,
                                                                @bloodType,
                                                                @age);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@firstName", model.FirstName);
                command.Parameters.AddWithValue("@lastName", model.LastName);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);
                command.Parameters.AddWithValue("@bloodType", model.BloodType);
                command.Parameters.AddWithValue("@gender", (int)model.Gender);
                command.Parameters.AddWithValue("@id", model.DonorID);
                command.Parameters.AddWithValue("@age", model.Age);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> EditDonor(DonorModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Update Donor set firstName = @firstName, 
                                                        lastName = @lastName,  
                                                        address = @address, 
                                                        email = @email,
                                                        phone = @phone,
                                                        bloodType = @bloodType,
                                                        gender = @gender,
                                                        age = @age
                                                        where donorID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@firstName", model.FirstName);
                command.Parameters.AddWithValue("@lastName", model.LastName);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);
                command.Parameters.AddWithValue("@bloodType", model.BloodType);
                command.Parameters.AddWithValue("@id", model.DonorID);
                command.Parameters.AddWithValue("@gender", (int)model.Gender);
                command.Parameters.AddWithValue("@age", model.Age);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteDonor(Guid id)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Delete from Donor where donorID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteDonorLogin(Guid id)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Delete from LoginData where donorID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }
    }
}

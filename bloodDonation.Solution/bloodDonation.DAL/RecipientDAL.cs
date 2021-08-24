using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class RecipientDAL : IRecipientDAL
    {
        private readonly string connectionString = @"Server=mdubinjak;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<RecipientModel> GetRecipient(Guid id)
        {
            var recipient = new RecipientModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from Recipient where recipientID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        recipient.Name = reader["name"].ToString();
                        recipient.RecipientID = Guid.Parse(reader["recipientID"].ToString());
                        recipient.Address = reader["address"].ToString();
                        recipient.Email = reader["email"].ToString();
                        recipient.Phone = reader["phone"].ToString();
                        recipient.BloodType = reader["bloodType"].ToString();
                        recipient.Anon = (Anon)reader.GetOrdinal("anon");
                    }
                }
            }
            return recipient;
        }

        public async Task<bool> PostRecipient(RecipientModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Insert into Recipient values(@id,
                                                                @name 
                                                                @address, 
                                                                @email,
                                                                @phone,
                                                                @bloodType,
                                                                @anon);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);
                command.Parameters.AddWithValue("@bloodType", model.BloodType);
                command.Parameters.AddWithValue("@id", model.RecipientID);
                command.Parameters.AddWithValue("@anon", (int)model.Anon);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> EditRecipient(RecipientModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Update Recipient set name = @name, 
                                                        address = @address, 
                                                        email = @email,
                                                        phone = @phone,
                                                        bloodType = @bloodType,
                                                        anon = @anon,
                                                        where recipientID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);
                command.Parameters.AddWithValue("@bloodType", model.BloodType);
                command.Parameters.AddWithValue("@id", model.RecipientID);
                command.Parameters.AddWithValue("@anon", (int)model.Anon);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteRecipient(Guid id)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Delete from Recipient where recipientID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }
    }
}

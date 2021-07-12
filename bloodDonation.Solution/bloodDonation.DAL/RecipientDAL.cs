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
        private readonly string connectionString = @"Server=DESKTOP-5LE39JF\SQLEXPRESS;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<RecipientModel> GetRecipient(int id)
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
                        recipient.FirstName = reader["firstName"].ToString();
                        recipient.LastName = reader["lastName"].ToString();
                        recipient.RecipientID = reader.GetInt32(reader.GetOrdinal("recipientID"));
                        recipient.Address = reader["address"].ToString();
                        recipient.Email = reader["email"].ToString();
                        recipient.Phone = reader["phone"].ToString();
                        recipient.BloodType = reader["bloodType"].ToString();
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
                string queryString = @"Insert into Recipient values(@firstName, 
                                                                @lastName,  
                                                                @address, 
                                                                @email,
                                                                @phone,
                                                                @bloodType);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@firstName", model.FirstName);
                command.Parameters.AddWithValue("@lastName", model.LastName);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);
                command.Parameters.AddWithValue("@bloodType", model.BloodType);

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
                string queryString = @"Update Recipient set firstName = @firstName, 
                                                        lastName = @lastName,  
                                                        address = @address, 
                                                        email = @email,
                                                        phone = @phone,
                                                        bloodType = @bloodType
                                                        where recipientID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@firstName", model.FirstName);
                command.Parameters.AddWithValue("@lastName", model.LastName);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);
                command.Parameters.AddWithValue("@bloodType", model.BloodType);
                command.Parameters.AddWithValue("@id", model.RecipientID);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteRecipient(int id)
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

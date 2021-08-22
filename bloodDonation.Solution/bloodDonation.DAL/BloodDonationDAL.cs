using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class BloodDonationDAL : IBloodDonationDAL
    {
        private readonly string connectionString = @"Server=mdubinjak;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<BloodDonationModel> GetBloodDonation(int id)
        {
            var bloodDonation = new BloodDonationModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from BloodDonation where bloodID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bloodDonation.BloodID = reader.GetInt32(reader.GetOrdinal("bloodID"));
                        bloodDonation.DonorID = reader.GetInt32(reader.GetOrdinal("donorID"));
                        bloodDonation.Quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                        bloodDonation.DateDonated = DateTime.Parse(reader["dateDonated"].ToString());
                    }
                }
            }
            return bloodDonation;
        }

        public async Task<List<BloodDonationModel>> GetBloodDonationsAsync(int id)
        {
            var bloodDonations = new List<BloodDonationModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from BloodDonation where donorID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bloodDonations.Add(
                            new BloodDonationModel()
                            {
                                BloodID = reader.GetInt32(reader.GetOrdinal("bloodID")),
                                DonorID = reader.GetInt32(reader.GetOrdinal("donorID")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                DateDonated = DateTime.Parse(reader["dateDonated"].ToString())
                            }
                        );
                    }
                }
            }
            return bloodDonations;
        }

        public async Task<bool> PostBloodDonation(BloodDonationModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Insert into BloodDonation values(@donorID, 
                                                                @quantity,  
                                                                @dateDonated);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@donorID", model.DonorID);
                command.Parameters.AddWithValue("@quantity", model.Quantity);
                command.Parameters.AddWithValue("@dateDonated", model.DateDonated);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> EditBloodDonation(BloodDonationModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Update BloodDonation set donorID = @donorID, 
                                                        quantity = @quantity,  
                                                        dateDonated = @dateDonated
                                                        where bloodID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@dateDonated", model.DateDonated);
                command.Parameters.AddWithValue("@quantity", model.Quantity);
                command.Parameters.AddWithValue("@donorID", model.DonorID);
                command.Parameters.AddWithValue("@id", model.BloodID);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteBloodDonation(int id)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Delete from BloodDonation where bloodID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }
    }
}

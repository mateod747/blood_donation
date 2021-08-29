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

        public async Task<BloodDonationModel> GetBloodDonation(int year, int month, int day, Guid donorId)
        {
            var bloodDonation = new BloodDonationModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Select * from BloodDonation WHERE donorID = @donorId 
                                                                AND (DATEPART(yy, dateDonated) = @year)
                                                                AND (DATEPART(mm, dateDonated) = @month)
                                                                AND (DATEPART(dd, dateDonated) = @day)";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@donorId", donorId);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@day", day);


                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bloodDonation.BloodID = Guid.Parse(reader["bloodID"].ToString());
                        bloodDonation.DonorID = Guid.Parse(reader["donorID"].ToString());
                        bloodDonation.DateDonated = DateTime.Parse(reader["dateDonated"].ToString());
                    }
                }
            }
            return bloodDonation;
        }

        public async Task<List<BloodDonationModel>> GetBloodDonationsAsync(Guid id)
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
                                BloodID = Guid.Parse(reader["bloodID"].ToString()),
                                DonorID = Guid.Parse(reader["donorID"].ToString()),
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
                string queryString = @"Insert into BloodDonation values(@id, 
                                                                        @donorID, 
                                                                        @dateDonated);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", model.BloodID);
                command.Parameters.AddWithValue("@donorID", model.DonorID);
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
                                                        dateDonated = @dateDonated
                                                        where bloodID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@dateDonated", model.DateDonated);
                command.Parameters.AddWithValue("@donorID", model.DonorID);
                command.Parameters.AddWithValue("@id", model.BloodID);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteBloodDonation(Guid id)
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

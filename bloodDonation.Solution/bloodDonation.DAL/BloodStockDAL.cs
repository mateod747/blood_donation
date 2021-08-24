using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class BloodStockDAL : IBloodStockDAL
    {
        private readonly string connectionString = @"Server=mdubinjak;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<BloodStockModel> GetBloodStockAsync()
        {
            var stock = new BloodStockModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from BloodStock";

                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        stock.ZeroMinus = (int)reader["zeroMinus"];
                        stock.ZeroPlus = (int)reader["zeroPlus"];
                        stock.AMinus = (int)reader["aMinus"];
                        stock.APlus = (int)reader["aPlus"];
                        stock.BMinus =(int)reader["bMinus"];
                        stock.BPlus = (int)reader["bPlus"];
                        stock.ABMinus = (int)reader["abMinus"];
                        stock.ABPlus = (int)reader["abPlus"];
                    }
                }
            }
            return stock;
        }

        public async Task<bool> EditBloodStockAsync(BloodStockModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Update BloodStock set zeroMinus = @zeroMinus, 
                                                        zeroPlus = @zeroPlus,  
                                                        aMinus = @aMinus, 
                                                        aPlus = @aPlus,
                                                        bMinus = @bMinus,
                                                        bPlus = @bPlus,
                                                        abMinus = @abMinus,
                                                        abPlus = @abPlus";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@zeroMinus", model.ZeroMinus);
                command.Parameters.AddWithValue("@zeroPlus", model.ZeroPlus);
                command.Parameters.AddWithValue("@aMinus", model.AMinus);
                command.Parameters.AddWithValue("@aPlus", model.APlus);
                command.Parameters.AddWithValue("@bMinus", model.BMinus);
                command.Parameters.AddWithValue("@bPlus", model.BPlus);
                command.Parameters.AddWithValue("@abMinus", model.ABMinus);
                command.Parameters.AddWithValue("@abPlus", model.ABPlus);


                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

    }
}

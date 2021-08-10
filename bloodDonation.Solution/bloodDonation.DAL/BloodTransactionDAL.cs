using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class BloodTransactionDAL : IBloodTransactionDAL
    {
        private readonly string connectionString = @"Server=mdubinjak;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<BloodTransactionModel> GetBloodTransaction(int id)
        {
            var bloodTransaction = new BloodTransactionModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from BloodTransaction where bloodID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bloodTransaction.BloodID = reader.GetInt32(reader.GetOrdinal("bloodID"));
                        bloodTransaction.TransactID = reader.GetInt32(reader.GetOrdinal("transactID"));
                        bloodTransaction.RecipientID = reader.GetInt32(reader.GetOrdinal("recipientID"));
                        bloodTransaction.EmpID = reader.GetInt32(reader.GetOrdinal("empID"));
                        bloodTransaction.Quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                        bloodTransaction.DateOut = DateTime.Parse(reader["dateOut"].ToString());
                    }
                }
            }
            return bloodTransaction;
        }

        public async Task<bool> PostBloodTransaction(BloodTransactionModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Insert into BloodTransaction values(@empID, 
                                                                @dateOut,  
                                                                @quantity,
                                                                @recipientID,
                                                                @bloodID);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@empID", model.EmpID);
                command.Parameters.AddWithValue("@dateOut", model.DateOut);
                command.Parameters.AddWithValue("@quantity", model.Quantity);
                command.Parameters.AddWithValue("@recipientID", model.RecipientID);
                command.Parameters.AddWithValue("@bloodID", model.BloodID);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> EditBloodTransaction(BloodTransactionModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Update BloodTransaction set empID = @empID, 
                                                        quantity = @quantity,  
                                                        dateOut = @dateOut,
                                                        recipientID = @recipientID,
                                                        bloodID = @bloodID
                                                        where transactID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@empID", model.EmpID);
                command.Parameters.AddWithValue("@quantity", model.Quantity);
                command.Parameters.AddWithValue("@dateOut", model.DateOut);
                command.Parameters.AddWithValue("@recipientID", model.RecipientID);
                command.Parameters.AddWithValue("@bloodID", model.BloodID);
                command.Parameters.AddWithValue("@id", model.TransactID);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteBloodTransaction(int id)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Delete from BloodTransaction where transactID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }
    }
}

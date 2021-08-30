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

        public async Task<BloodTransactionModel> GetBloodTransaction(Guid id)
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
                        bloodTransaction.BloodID = Guid.Parse(reader["bloodID"].ToString());
                        bloodTransaction.TransactID = Guid.Parse(reader["transactID"].ToString());
                        bloodTransaction.RecipientID = Guid.Parse(reader["recipientID"].ToString());
                        bloodTransaction.EmpID = Guid.Parse(reader["empID"].ToString());
                        bloodTransaction.Quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                        bloodTransaction.DateOut = DateTime.Parse(reader["dateOut"].ToString());
                        bloodTransaction.Hemoglobin = (int)reader["hemoglobin"];
                        bloodTransaction.BloodPressure = reader["bloodPressure"].ToString();
                        bloodTransaction.Notes = reader["notes"].ToString();
                        bloodTransaction.Success = (bool)reader["success"];
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
                string queryString = @"Insert into BloodTransaction values(@id,
                                                                @empID, 
                                                                @dateOut,  
                                                                @quantity,
                                                                @hemoglobin,
                                                                @bloodPressure,
                                                                @notes,
                                                                @recipientID,
                                                                @bloodID,
                                                                @success);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", model.TransactID);
                command.Parameters.AddWithValue("@empID", model.EmpID);
                command.Parameters.AddWithValue("@dateOut", model.DateOut);
                command.Parameters.AddWithValue("@quantity", model.Quantity);
                command.Parameters.AddWithValue("@recipientID", model.RecipientID);
                command.Parameters.AddWithValue("@bloodID", model.BloodID);
                command.Parameters.AddWithValue("@hemoglobin", model.Hemoglobin);
                command.Parameters.AddWithValue("@bloodPressure", model.BloodPressure);
                command.Parameters.AddWithValue("@notes", model.Notes);
                command.Parameters.AddWithValue("@success", model.Success);


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
                string queryString = @"Update BloodTransaction set  
                                                        recipientID = @recipientID,
                                                        notes = @notes,
                                                        dateOut = @dateOut
                                                        where transactID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@dateOut", model.DateOut);
                command.Parameters.AddWithValue("@recipientID", model.RecipientID);
                command.Parameters.AddWithValue("@id", model.TransactID);
                command.Parameters.AddWithValue("@notes", model.Notes);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteBloodTransaction(Guid id)
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

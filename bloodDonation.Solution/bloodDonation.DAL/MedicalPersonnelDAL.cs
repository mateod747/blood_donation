using bloodDonation.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class MedicalPersonnelDAL : IMedicalPersonnelDAL
    {
        private readonly string connectionString = @"Server=DESKTOP-5LE39JF\SQLEXPRESS;Database=blood_donation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task<MedicalPersonnelModel> GetMedicalPersonnel(int id)
        {
            var medicalPersonnel = new MedicalPersonnelModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "Select * from MedicalPersonnel where empID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        medicalPersonnel.FirstName = reader["firstName"].ToString();
                        medicalPersonnel.LastName = reader["lastName"].ToString();
                        medicalPersonnel.EmpID = reader.GetInt32(reader.GetOrdinal("empID"));
                        medicalPersonnel.Address = reader["address"].ToString();
                        medicalPersonnel.Email = reader["email"].ToString();
                        medicalPersonnel.Phone = reader["phone"].ToString();
                    }
                }
            }
            return medicalPersonnel;
        }

        public async Task<bool> PostMedicalPersonnel(MedicalPersonnelModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Insert into MedicalPersonnel values(@firstName, 
                                                                @lastName,  
                                                                @address, 
                                                                @email,
                                                                @phone);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@firstName", model.FirstName);
                command.Parameters.AddWithValue("@lastName", model.LastName);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> EditMedicalPersonnel(MedicalPersonnelModel model)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Update MedicalPersonnel set firstName = @firstName, 
                                                        lastName = @lastName,  
                                                        address = @address, 
                                                        email = @email,
                                                        phone = @phone,
                                                        where empID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@firstName", model.FirstName);
                command.Parameters.AddWithValue("@lastName", model.LastName);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@phone", model.Phone);
                command.Parameters.AddWithValue("@id", model.EmpID);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }

        public async Task<bool> DeleteMedicalPersonnel(int id)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"Delete from MedicalPersonnel where empID = @id;";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                if ((await command.ExecuteNonQueryAsync()) > 0) success = true;
            }
            return success;
        }
    }
}

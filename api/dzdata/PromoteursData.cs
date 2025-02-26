using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace dzdata
{
    public class PromoteurDTO
    {
        public PromoteurDTO(int id, int userId, string companyName, string registrationNumber, string address)
        {
            Id = id;
            UserId = userId;
            CompanyName = companyName;
            RegistrationNumber = registrationNumber;
            Address = address;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string Address { get; set; }
    }

    public class PromoteursData
    {
        private static readonly string _connectionString = Connection_data._connectionString;

        public static List<PromoteurDTO> GetPromoteurs()
        {
            var promoteurs = new List<PromoteurDTO>();
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, User_Id, Company_Name, Registration_Number, Address FROM Promoteurs", con))
            {
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            promoteurs.Add(new PromoteurDTO(
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return promoteurs;
        }

        public static PromoteurDTO GetPromoteurById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, User_Id, Company_Name, Registration_Number, Address FROM Promoteurs WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PromoteurDTO(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        );
                    }
                }
            }
            return null;
        }

        public static int AddPromoteur(int userId, string companyName, string registrationNumber, string address)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("INSERT INTO Promoteurs (User_Id, Company_Name, Registration_Number, Address) OUTPUT INSERTED.Id VALUES (@UserId, @CompanyName, @RegistrationNumber, @Address)", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                cmd.Parameters.AddWithValue("@Address", address);
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public static bool UpdatePromoteur(int id, int userId, string companyName, string registrationNumber, string address)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("UPDATE Promoteurs SET User_Id = @UserId, Company_Name = @CompanyName, Registration_Number = @RegistrationNumber, Address = @Address WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                cmd.Parameters.AddWithValue("@Address", address);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public static bool DeletePromoteur(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Promoteurs WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

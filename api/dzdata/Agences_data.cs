using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace dzdata
{
    public class AgencesDTO
    {
        public AgencesDTO(int id, int userId, string companyName, string address, string website)
        {
            Id = id;
            UserId = userId;
            CompanyName = companyName;
            Address = address;
            Website = website;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
    }

    public class Agences_data
    {
        private static readonly string _connectionString = Connection_data._connectionString;

        public static List<AgencesDTO> GetAgences()
        {
            var agences = new List<AgencesDTO>();
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, User_Id, Company_Name, Address, Website FROM Agences", con))
            {
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agences.Add(new AgencesDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetInt32(reader.GetOrdinal("User_Id")),
                                reader.GetString(reader.GetOrdinal("Company_Name")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("Website"))
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return agences;
        }

        public static AgencesDTO GetAgenceById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, User_Id, Company_Name, Address, Website FROM Agences WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new AgencesDTO(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetInt32(reader.GetOrdinal("User_Id")),
                            reader.GetString(reader.GetOrdinal("Company_Name")),
                            reader.GetString(reader.GetOrdinal("Address")),
                            reader.GetString(reader.GetOrdinal("Website"))
                        );
                    }
                }
            }
            return null;
        }

        public static bool UpdateAgence(int id, int userId, string companyName, string address, string website)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("UPDATE Agences SET User_Id = @UserId, Company_Name = @CompanyName, Address = @Address, Website = @Website WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Website", website);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteAgence(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Agences WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static int AddAgence(int userId, string companyName, string address, string website)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("INSERT INTO Agences (User_Id, Company_Name, Address, Website) OUTPUT INSERTED.Id VALUES (@UserId, @CompanyName, @Address, @Website)", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Website", website);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
    }
}
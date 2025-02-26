using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace dzdata
{
    public class Status_PropertiesDTO
    {
        public Status_PropertiesDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Status_Properties_data
    {
        private static readonly string _connectionString = Connection_data._connectionString;

        public static List<Status_PropertiesDTO> GetStatusProperties()
        {
            var properties = new List<Status_PropertiesDTO>();
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, Name FROM Status_Properties", con))
            {
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            properties.Add(new Status_PropertiesDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name"))
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return properties;
        }

        public static Status_PropertiesDTO GetStatusPropertyById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, Name FROM Status_Properties WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Status_PropertiesDTO(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name"))
                        );
                    }
                }
            }
            return null;
        }

        public static bool UpdateStatusProperty(int id, string name)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("UPDATE Status_Properties SET Name = @Name WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteStatusProperty(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Status_Properties WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static int AddStatusProperty(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("INSERT INTO Status_Properties (Name) OUTPUT INSERTED.Id VALUES (@Name)", connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
    }
}
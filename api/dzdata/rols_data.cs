using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace dzdata
{
    public class RoleDTO
    {
        public RoleDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RolesData
    {
        private static string _connectionString = "Server=localhost;Database=DB_dzimmo;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

        public static List<RoleDTO> GetRoles()
        {
            var roles = new List<RoleDTO>();
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, Name FROM Roles", con))
            {
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new RoleDTO(
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
            return roles;
        }

        public static RoleDTO GetRoleById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, Name FROM Roles WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new RoleDTO(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name"))
                        );
                    }
                }
            }
            return null;
        }

        public static bool UpdateRole(int id, string name)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("UPDATE Roles SET Name = @Name WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteRole(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Roles WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static int AddRole(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("INSERT INTO Roles (Name) OUTPUT INSERTED.Id VALUES (@Name)", connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

    }
}

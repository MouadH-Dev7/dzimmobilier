using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace dzdata
{
    public class TypeData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TypeData() { }

        public TypeData(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private static readonly string _connectionString = Connection_data._connectionString;


        public static int AddType(string name)
        {
            int newId = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Types (Name) VALUES (@name); SELECT SCOPE_IDENTITY();", conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    newId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return newId;
        }


        public static bool UpdateType(int id, string name)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Types SET Name = @name WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public static bool DeleteType(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Types WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public static TypeData GetTypeById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Types WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TypeData(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
            }
            return null;
        }


        public static List<TypeData> GetAllTypes()
        {
            List<TypeData> types = new List<TypeData>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Types", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            types.Add(new TypeData(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            return types;
        }
    }
}

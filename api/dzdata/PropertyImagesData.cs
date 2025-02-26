using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace dzdata
{
    public class PropertyImageDTO
    {
        public PropertyImageDTO(int id, int propertyId, string imageUrl)
        {
            Id = id;
            PropertyId = propertyId;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; }
    }
    public class PropertyImagesData
    {
        private static readonly string _connectionString = Connection_data._connectionString;

        public static List<PropertyImageDTO> GetPropertyImages()
        {
            var images = new List<PropertyImageDTO>();
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, Property_Id, Image_Url FROM Property_Images", con))
            {
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            images.Add(new PropertyImageDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetInt32(reader.GetOrdinal("Property_Id")),
                                reader.GetString(reader.GetOrdinal("Image_Url"))
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return images;
        }

        public static PropertyImageDTO GetPropertyImageById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Id, Property_Id, Image_Url FROM Property_Images WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PropertyImageDTO(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetInt32(reader.GetOrdinal("Property_Id")),
                            reader.GetString(reader.GetOrdinal("Image_Url"))
                        );
                    }
                }
            }
            return null;
        }

        public static int AddPropertyImage(int propertyId, string imageUrl)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("INSERT INTO Property_Images (Property_Id, Image_Url) OUTPUT INSERTED.Id VALUES (@PropertyId, @ImageUrl)", connection))
            {
                command.Parameters.AddWithValue("@PropertyId", propertyId);
                command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public static bool UpdatePropertyImage(int id, int propertyId, string imageUrl)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("UPDATE Property_Images SET Property_Id = @PropertyId, Image_Url = @ImageUrl WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@PropertyId", propertyId);
                cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeletePropertyImage(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Property_Images WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

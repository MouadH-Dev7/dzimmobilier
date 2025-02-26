using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace dzdata
{
    public class PropertyDTO
    {
        public PropertyDTO(int id, int userId, int communeId, int categoryId, int typeId, string title, string description, decimal price, double area, int bedrooms, int bathrooms, double latitude, double longitude, DateTime createdAt, int statusId)
        {
            Id = id;
            UserId = userId;
            CommuneId = communeId;
            CategoryId = categoryId;
            TypeId = typeId;
            Title = title;
            Description = description;
            Price = price;
            Area = area;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Latitude = latitude;
            Longitude = longitude;
            CreatedAt = createdAt;
            StatusId = statusId;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommuneId { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
    }

    public class PropertiesData
    {
        private static readonly string _connectionString = Connection_data._connectionString;

        public static List<PropertyDTO> GetProperties()
        {
            var properties = new List<PropertyDTO>();
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM Properties", con))
            {
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        properties.Add(new PropertyDTO(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4),
                            reader.GetString(5),
                            reader.GetString(6),
                            reader.GetDecimal(7),
                            reader.GetDouble(8),
                            reader.GetInt32(9),
                            reader.GetInt32(10),
                            reader.GetDouble(11),
                            reader.GetDouble(12),
                            reader.GetDateTime(13),
                            reader.GetInt32(14)
                        ));
                    }
                }
            }
            return properties;
        }

        public static PropertyDTO GetPropertyById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM Properties WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PropertyDTO(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4),
                            reader.GetString(5),
                            reader.GetString(6),
                            reader.GetDecimal(7),
                            reader.GetDouble(8),
                            reader.GetInt32(9),
                            reader.GetInt32(10),
                            reader.GetDouble(11),
                            reader.GetDouble(12),
                            reader.GetDateTime(13),
                            reader.GetInt32(14)
                        );
                    }
                }
            }
            return null;
        }

        public static int AddProperty(int userId, int communeId, int categoryId, int typeId, string title, string description, decimal price, double area, int bedrooms, int bathrooms, double latitude, double longitude, int statusId)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "INSERT INTO Properties (User_Id, Commune_Id, Category_Id, Type_Id, Title, Description, Price, Area, Bedrooms, Bathrooms, Latitude, Longitude, Created_At, Status_Id_Properties) " +
                "OUTPUT INSERTED.Id VALUES (@UserId, @CommuneId, @CategoryId, @TypeId, @Title, @Description, @Price, @Area, @Bedrooms, @Bathrooms, @Latitude, @Longitude, GETDATE(), @StatusId)", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CommuneId", communeId);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@TypeId", typeId);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Area", area);
                cmd.Parameters.AddWithValue("@Bedrooms", bedrooms);
                cmd.Parameters.AddWithValue("@Bathrooms", bathrooms);
                cmd.Parameters.AddWithValue("@Latitude", latitude);
                cmd.Parameters.AddWithValue("@Longitude", longitude);
                cmd.Parameters.AddWithValue("@StatusId", statusId);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public static bool UpdateProperty(int id, int userId, int communeId, int categoryId, int typeId, string title, string description, decimal price, double area, int bedrooms, int bathrooms, double latitude, double longitude, int statusId)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "UPDATE Properties SET User_Id = @UserId, Commune_Id = @CommuneId, Category_Id = @CategoryId, Type_Id = @TypeId, Title = @Title, Description = @Description, " +
                "Price = @Price, Area = @Area, Bedrooms = @Bedrooms, Bathrooms = @Bathrooms, Latitude = @Latitude, Longitude = @Longitude, Status_Id_Properties = @StatusId WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CommuneId", communeId);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@TypeId", typeId);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Area", area);
                cmd.Parameters.AddWithValue("@Bedrooms", bedrooms);
                cmd.Parameters.AddWithValue("@Bathrooms", bathrooms);
                cmd.Parameters.AddWithValue("@Latitude", latitude);
                cmd.Parameters.AddWithValue("@Longitude", longitude);
                cmd.Parameters.AddWithValue("@StatusId", statusId);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteProperty(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Properties WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

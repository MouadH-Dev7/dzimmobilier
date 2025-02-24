using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dzdata
{


    public class UserDTO
    {
        public UserDTO(int id, string name, string name2, string email, string password, string phone, int roleId, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Name2 = name2;
            Email = email;
            Password = password;
            Phone = phone;
            RoleId = roleId;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
    }


   public class users_data
    {

       

   
        static string _connectionString = "Server=localhost;Database=DB_dzimmo;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

        public static List<UserDTO> GetAllUsers()
        {
            var usersList = new List<UserDTO>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usersList.Add(new UserDTO(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetString(reader.GetOrdinal("Name2")),
                            reader.GetString(reader.GetOrdinal("Email")),
                            reader.GetString(reader.GetOrdinal("Password")),
                            reader.GetString(reader.GetOrdinal("Phone")),
                            reader.GetInt32(reader.GetOrdinal("Role_Id")),
                            reader.GetDateTime(reader.GetOrdinal("Created_At"))
                        ));
                    }
                }
            }
            return usersList;
        }

        public static bool IsEmailExists(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }

        public static bool IsPhoneExists(string phone)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Phone = @Phone", connection))
            {
                command.Parameters.AddWithValue("@Phone", phone);
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }

        public static UserDTO GetUserById(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT * FROM Users WHERE Id = @UserId", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserDTO(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetString(reader.GetOrdinal("Name2")),
                            reader.GetString(reader.GetOrdinal("Email")),
                            reader.GetString(reader.GetOrdinal("Password")),
                            reader.GetString(reader.GetOrdinal("Phone")),
                            reader.GetInt32(reader.GetOrdinal("Role_Id")),
                            reader.GetDateTime(reader.GetOrdinal("Created_At"))
                        );
                    }
                }
            }
            return null;
        }

        public static int AddUser(UserDTO user)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("INSERT INTO Users (Name, Name2, Email, Password, Phone, Role_Id) OUTPUT INSERTED.Id VALUES (@Name, @Name2, @Email, @Password, @Phone, @Role_Id)", connection))
            {
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Name2", user.Name2);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Role_Id", user.RoleId);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public static bool UpdateUser(UserDTO user)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("UPDATE Users SET Name = @Name, Name2 = @Name2, Email = @Email, Password = @Password, Phone = @Phone, Role_Id = @Role_Id WHERE Id = @UserId", connection))
            {
                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Name2", user.Name2);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Role_Id", user.RoleId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("DELETE FROM Users WHERE Id = @UserId", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public static UserDTO Login(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT * FROM Users WHERE Email = @Email AND Password = @Password", connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserDTO(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetString(reader.GetOrdinal("Name2")),
                            reader.GetString(reader.GetOrdinal("Email")),
                            reader.GetString(reader.GetOrdinal("Password")),
                            reader.GetString(reader.GetOrdinal("Phone")),
                            reader.GetInt32(reader.GetOrdinal("Role_Id")),
                            reader.GetDateTime(reader.GetOrdinal("Created_At"))
                        );
                    }
                }
            }
            return null;
        }

    }
}

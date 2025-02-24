using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class Users
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public UserDTO UDTO
        {
            get { return new UserDTO(this.ID, this.Name, this.Name2, this.Email, this.Password, this.Phone, this.RoleId, this.CreatedAt); }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Users(UserDTO UDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = UDTO.Id;
            this.Name = UDTO.Name;
            this.Name2 = UDTO.Name2;
            this.Email = UDTO.Email;
            this.Password = UDTO.Password;
            this.Phone = UDTO.Phone;
            this.RoleId = UDTO.RoleId;
            this.CreatedAt = UDTO.CreatedAt;
            Mode = cMode;
        }

        private bool _AddNewUser()
        {
            this.ID = users_data.AddUser(UDTO);
            return (this.ID != -1);
        }

        private bool _UpdateUser()
        {
            return users_data.UpdateUser(UDTO);
        }

        public static List<UserDTO> GetAllUsers()
        {
            return users_data.GetAllUsers();
        }

        public static Users Find(int ID)
        {
            UserDTO UDTO = users_data.GetUserById(ID);
            if(UDTO != null)
            {
            
            
            

                return new Users(UDTO, enMode.Update);
            }
            else
                return null;
        }

          
        

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static bool DeleteUser(int ID)
        {
            return users_data.DeleteUser(ID);
        }

        public static UserDTO Login(string email, string password)
        {
            return users_data.Login(email, password);
        }

        public static bool IsEmailExists(string email)
        {
            return users_data.IsEmailExists(email);
        }

        public static bool IsPhoneExists(string phone)
        {
            return users_data.IsPhoneExists(phone);
        }
    }
}
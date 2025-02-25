using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class Roles
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public RoleDTO RDTO
        {
            get { return new RoleDTO(this.ID, this.Name); }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public Roles(RoleDTO RDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = RDTO.Id;
            this.Name = RDTO.Name;
            Mode = cMode;
        }

        private bool _AddNewRole()
        {
            this.ID = RolesData.AddRole(Name);
            return this.ID != -1;
        }

        private bool _UpdateRole()
        {
            return RolesData.UpdateRole(ID, Name);
        }

        public static List<RoleDTO> GetAllRoles()
        {
            return RolesData.GetRoles();
        }

        public static Roles Find(int ID)
        {
            RoleDTO RDTO = RolesData.GetRoleById(ID);
            if (RDTO != null)
                return new Roles(RDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewRole())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateRole();
            }
            return false;
        }

        public static bool DeleteRole(int ID)
        {
            return RolesData.DeleteRole(ID);
        }
    }
}

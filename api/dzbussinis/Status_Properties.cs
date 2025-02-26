using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class Status_Properties
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public Status_PropertiesDTO SPDTO
        {
            get { return new Status_PropertiesDTO(this.ID, this.Name); }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public Status_Properties(Status_PropertiesDTO spDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = spDTO.Id;
            this.Name = spDTO.Name;
            Mode = cMode;
        }

        private bool _AddNewStatusProperty()
        {
            this.ID = Status_Properties_data.AddStatusProperty(Name);
            return this.ID != -1;
        }

        private bool _UpdateStatusProperty()
        {
            return Status_Properties_data.UpdateStatusProperty(ID, Name);
        }

        public static List<Status_PropertiesDTO> GetAllStatusProperties()
        {
            return Status_Properties_data.GetStatusProperties();
        }

        public static Status_Properties Find(int ID)
        {
            Status_PropertiesDTO spDTO = Status_Properties_data.GetStatusPropertyById(ID);
            if (spDTO != null)
                return new Status_Properties(spDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewStatusProperty())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateStatusProperty();
            }
            return false;
        }

        public static bool DeleteStatusProperty(int ID)
        {
            return Status_Properties_data.DeleteStatusProperty(ID);
        }
    }
}

using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class Properties
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public PropertyDTO PDTO
        {
            get
            {
                return new PropertyDTO(this.ID, this.UserId, this.CommuneId, this.CategoryId, this.TypeId,
                                       this.Title, this.Description, this.Price, this.Area, this.Bedrooms,
                                       this.Bathrooms, this.Latitude, this.Longitude, this.CreatedAt, this.StatusId);
            }
        }

        public int ID { get; set; }
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

        public Properties(PropertyDTO pDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = pDTO.Id;
            this.UserId = pDTO.UserId;
            this.CommuneId = pDTO.CommuneId;
            this.CategoryId = pDTO.CategoryId;
            this.TypeId = pDTO.TypeId;
            this.Title = pDTO.Title;
            this.Description = pDTO.Description;
            this.Price = pDTO.Price;
            this.Area = pDTO.Area;
            this.Bedrooms = pDTO.Bedrooms;
            this.Bathrooms = pDTO.Bathrooms;
            this.Latitude = pDTO.Latitude;
            this.Longitude = pDTO.Longitude;
            this.CreatedAt = pDTO.CreatedAt;
            this.StatusId = pDTO.StatusId;

            Mode = cMode;
        }

        private bool _AddNewProperty()
        {
            this.ID = PropertiesData.AddProperty(UserId, CommuneId, CategoryId, TypeId, Title, Description, Price, Area, Bedrooms, Bathrooms, Latitude, Longitude, StatusId);
            return this.ID != -1;
        }

        
        private bool _UpdateProperty()
        {
            return PropertiesData.UpdateProperty(ID, UserId, CommuneId, CategoryId, TypeId, Title, Description, Price, Area, Bedrooms, Bathrooms, Latitude, Longitude, StatusId);
        }

        public static List<PropertyDTO> GetAllProperties()
        {
            return PropertiesData.GetProperties();
        }

        public static Properties Find(int ID)
        {
            PropertyDTO pDTO = PropertiesData.GetPropertyById(ID);
            if (pDTO != null)
                return new Properties(pDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewProperty())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateProperty();
            }
            return false;
        }

        public static bool DeleteProperty(int ID)
        {
            return PropertiesData.DeleteProperty(ID);
        }
    }
}

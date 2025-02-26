using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class PropertyImages
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public PropertyImageDTO PDTO
        {
            get { return new PropertyImageDTO(this.ID, this.PropertyID, this.ImageUrl); }
        }

        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string ImageUrl { get; set; }

        public PropertyImages(PropertyImageDTO pDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = pDTO.Id;
            this.PropertyID = pDTO.PropertyId;
            this.ImageUrl = pDTO.ImageUrl;
            Mode = cMode;
        }

        private bool _AddNewPropertyImage()
        {
            this.ID = PropertyImagesData.AddPropertyImage(PropertyID, ImageUrl);
            return this.ID != -1;
        }

        private bool _UpdatePropertyImage()
        {
            return PropertyImagesData.UpdatePropertyImage(ID, PropertyID, ImageUrl);
        }

        public static List<PropertyImageDTO> GetAllPropertyImages()
        {
            return PropertyImagesData.GetPropertyImages();
        }

        public static PropertyImages Find(int ID)
        {
            PropertyImageDTO pDTO = PropertyImagesData.GetPropertyImageById(ID);
            if (pDTO != null)
                return new PropertyImages(pDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPropertyImage())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdatePropertyImage();
            }
            return false;
        }

        public static bool DeletePropertyImage(int ID)
        {
            return PropertyImagesData.DeletePropertyImage(ID);
        }
    }
}

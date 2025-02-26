using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class Promoteurs
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public PromoteurDTO PDTO
        {
            get { return new PromoteurDTO(this.ID, this.UserId, this.CompanyName, this.RegistrationNumber, this.Address); }
        }

        public int ID { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string Address { get; set; }

        public Promoteurs(PromoteurDTO pDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = pDTO.Id;
            this.UserId = pDTO.UserId;
            this.CompanyName = pDTO.CompanyName;
            this.RegistrationNumber = pDTO.RegistrationNumber;
            this.Address = pDTO.Address;
            Mode = cMode;
        }

        private bool _AddNewPromoteur()
        {
            this.ID = PromoteursData.AddPromoteur(UserId, CompanyName, RegistrationNumber, Address);
            return this.ID != -1;
        }

        private bool _UpdatePromoteur()
        {
            return PromoteursData.UpdatePromoteur(ID, UserId, CompanyName, RegistrationNumber, Address);
        }

        public static List<PromoteurDTO> GetAllPromoteurs()
        {
            return PromoteursData.GetPromoteurs();
        }

        public static Promoteurs Find(int ID)
        {
            PromoteurDTO pDTO = PromoteursData.GetPromoteurById(ID);
            if (pDTO != null)
                return new Promoteurs(pDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPromoteur())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdatePromoteur();
            }
            return false;
        }

        public static bool DeletePromoteur(int ID)
        {
            return PromoteursData.DeletePromoteur(ID);
        }
    }
}

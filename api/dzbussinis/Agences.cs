using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class Agences
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public AgencesDTO AgenceDTO
        {
            get { return new AgencesDTO(this.ID, this.UserId, this.CompanyName, this.Address, this.Website); }
        }

        public int ID { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }

        public Agences(AgencesDTO agenceDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = agenceDTO.Id;
            this.UserId = agenceDTO.UserId;
            this.CompanyName = agenceDTO.CompanyName;
            this.Address = agenceDTO.Address;
            this.Website = agenceDTO.Website;
            Mode = cMode;
        }

        private bool _AddNewAgence()
        {
            this.ID = Agences_data.AddAgence(UserId, CompanyName, Address, Website);
            return this.ID != -1;
        }

        private bool _UpdateAgence()
        {
            return Agences_data.UpdateAgence(ID, UserId, CompanyName, Address, Website);
        }

        public static List<AgencesDTO> GetAllAgences()
        {
            return Agences_data.GetAgences();
        }

        public static Agences Find(int ID)
        {
            AgencesDTO agenceDTO = Agences_data.GetAgenceById(ID);
            if (agenceDTO != null)
                return new Agences(agenceDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAgence())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateAgence();
            }
            return false;
        }

        public static bool DeleteAgence(int ID)
        {
            return Agences_data.DeleteAgence(ID);
        }
    }
}
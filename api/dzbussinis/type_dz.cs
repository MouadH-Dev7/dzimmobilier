using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class type_dz
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public TypeData TDTO
        {
            get { return new TypeData(this.ID, this.Name); }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public type_dz(TypeData TDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = TDTO.Id;
            this.Name = TDTO.Name;
            Mode = cMode;
        }

        private bool _AddNewType()
        {
            this.ID = TypeData.AddType(Name);
            return this.ID != 0;
        }

        private bool _UpdateType()
        {
            return TypeData.UpdateType(ID, Name);
        }

        public static List<TypeData> GetAllTypes()
        {
            return TypeData.GetAllTypes();
        }

        public static type_dz Find(int ID)
        {
            TypeData TDTO = TypeData.GetTypeById(ID);
            if (TDTO != null)
                return new type_dz(TDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateType();
            }
            return false;
        }

        public static bool DeleteType(int ID)
        {
            return TypeData.DeleteType(ID);
        }
    }
}

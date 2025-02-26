using System;
using System.Collections.Generic;
using dzdata;

namespace dzbussinis
{
    public class Categories
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public CategoryDTO CDTO
        {
            get { return new CategoryDTO(this.ID, this.Name); }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public Categories(CategoryDTO cDTO, enMode cMode = enMode.AddNew)
        {
            this.ID = cDTO.Id;
            this.Name = cDTO.Name;
            Mode = cMode;
        }

        private bool _AddNewCategory()
        {
            this.ID = CategoriesData.AddCategory(Name);
            return this.ID != -1;
        }

        private bool _UpdateCategory()
        {
            return CategoriesData.UpdateCategory(ID, Name);
        }

        public static List<CategoryDTO> GetAllCategories()
        {
            return CategoriesData.GetCategories();
        }

        public static Categories Find(int ID)
        {
            CategoryDTO cDTO = CategoriesData.GetCategoryById(ID);
            if (cDTO != null)
                return new Categories(cDTO, enMode.Update);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCategory())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateCategory();
            }
            return false;
        }

        public static bool DeleteCategory(int ID)
        {
            return CategoriesData.DeleteCategory(ID);
        }
    }
}

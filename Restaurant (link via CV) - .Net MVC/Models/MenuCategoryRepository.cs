using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class MenuCategoryRepository:Repository<MenuCategory>
    {
        public void AddMenuCategory(MenuCategory mc)
        {
            Add(mc); Save();
        }

        public MenuCategory MenuCategory(string id)
        {
            return DbSet.Where(mc => mc.MenuCategoryId.Equals(id)).SingleOrDefault();
        }

        public MenuCategory MenuCategoryOnCategoryName(string CatName)
        {
            return DbSet.Where(mc => mc.MenuCategoryName.Equals(CatName)).SingleOrDefault();
        }

        public List<MenuCategory> AllMenuCategories()
        {
            return DbSet.ToList();
        }

        public List<MenuCategory> MenuCategoriesOnMenuType(string id)
        {
            return DbSet.Where(i=>i.MenuTypeId.Equals(id)).ToList();
        }

        public void EntryDetatch(MenuCategory mc)
        {
            EntryDetached(mc);
        }

        public void EntryAttach(MenuCategory mc)
        {
            EntryAttach(mc);
        }

        public List<MenuCategory> MenuCategories(string id)
        {
            return DbSet.Where(i => i.MenuCategoryId.Equals(id)).ToList();
        }

        public List<MenuCategory> MenuCategoriesOnMenuTypeId(string id)
        {
            return DbSet.Where(i => i.MenuTypeId.Equals(id)).ToList();
        }

        public void DeleteMenuCategories(List<MenuCategory> Mc_list)
        {
            DeleteRange(Mc_list); Save();
        }

        public void DeleteMenuCategory(MenuCategory mc)
        {
            Delete(mc); Save();
        }

        public Boolean IfExists(string id)
        {
            if (DbSet.Any(ie => ie.MenuCategoryId.Equals(id)))
                return true;
            else
                return false;
        }

        public void UpdateMenuCategory(MenuCategory mc)
        {
            Entry(mc); Save();
        }
    }
}
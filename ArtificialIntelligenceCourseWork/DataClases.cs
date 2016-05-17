using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ArtificialIntelligenceCourseWork
{
    [Serializable]
    public class DishCategory
    {
        public int id { set; get; }
        public string name { set; get; }
        public DishCategory()
        {
            id = 0;
            name = "";
        }
        public DishCategory(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
    public class DishCategories
    {
        private static string fileName = "DishCategories.xml";
        public DishCategory[] categories { set; get; }
        public DishCategories()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(DishCategory[]));
                FileStream file = new FileStream(fileName, FileMode.Open);
                categories = (DishCategory[])ser.Deserialize(file);
                file.Close();
            }
            catch
            {
                categories = new DishCategory[]{};
            }
        }
        public void save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(DishCategory[]));
            TextWriter writer = new StreamWriter(fileName);
            ser.Serialize(writer, categories);
            writer.Close();
        }
        public int getNewId()
        {
            return categories.Length + 1;
        }
        public void add(string name)
        {
            this.add(new DishCategory(this.getNewId(), name));
        }
        public void add(DishCategory newCategory)
        {
            DishCategory[] categories = (DishCategory[])this.categories.Clone();
            Array.Resize(ref categories, categories.Length + 1);
            categories[this.categories.Length] = newCategory;
            this.categories = (DishCategory[])categories.Clone();
            save();
        }
        public bool checkName(string name)
        {
            foreach (DishCategory category in categories)
                if (name.ToLower() == category.name.ToLower()) return false;
            return true;
        }
        public int getId(string name)
        {
            foreach (DishCategory category in categories)
                if (name.ToLower() == category.name.ToLower()) return category.id;
            return -1;
        }
        public List<string> getNamesList()
        {
            List<string> names = new List<string>();
            foreach (DishCategory category in categories)
                names.Add(category.name);
            return names;
        }
        public int count()
        {
            return categories.Length;
        }
        public string get(int n = -1)
        {
            if (n > 0)
            {
                foreach (DishCategory category in categories)
                    if (category.id == n) return category.name;
            }
            else return categories[categories.Length - 1].name;
            return "Ошибка";
        }
    }
    [Serializable]
    public class Dish
    {
        public int id { set; get; }
        public string name { set; get; }
        public int category_id { set; get; }
        public int calories { set; get; }
        public Dish()
        {
            id = 0;
            name = "";
            category_id = 0;
            calories = 0;
        }
        public Dish(int id, string name, int category_id, int calories)
        {
            this.id = id;
            this.name = name;
            this.category_id = category_id;
            this.calories = calories;
        }
    }
    public class Dishes
    {
        private static string fileName = "Dishes.xml";
        public Dish[] dishes { set; get; }
        public Dishes()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(Dish[]));
                FileStream file = new FileStream(fileName, FileMode.Open);
                dishes = (Dish[])ser.Deserialize(file);
                file.Close();
            }
            catch
            {
                dishes = new Dish[]{};
            }
        }
        public void save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Dish[]));
            TextWriter writer = new StreamWriter(fileName);
            ser.Serialize(writer, dishes);
            writer.Close();
        }
        public int getNewId()
        {
            return dishes.Length + 1;
        }
        public void add(string name, int category_id, int calories)
        {
            this.add(new Dish(this.getNewId(), name, category_id, calories));
        }
        public void add(Dish newDish)
        {
            Dish[] dishes = (Dish[]) this.dishes.Clone();
            Array.Resize(ref dishes, dishes.Length + 1);
            dishes[this.dishes.Length] = newDish;
            this.dishes = (Dish[])dishes.Clone();
            save();
        }
        public bool checkName(string name)
        {
            foreach (Dish dish in dishes)
                if (name.ToLower() == dish.name.ToLower()) return false;
            return true;
        }
        public int count()
        {
            return dishes.Length;
        }
    }
}

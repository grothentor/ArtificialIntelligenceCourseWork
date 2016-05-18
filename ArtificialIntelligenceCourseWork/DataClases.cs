using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ArtificialIntelligenceCourseWork
{
    public class DataTemplate<T>
    {
        private string fileName = "DataTemplate.xml";
        public T[] variables { set; get; }
        public DataTemplate(string fileName)
        {
            this.fileName = fileName;
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T[]));
                FileStream file = new FileStream(fileName, FileMode.Open);
                variables = (T[])ser.Deserialize(file);
                file.Close();
            }
            catch
            {
                variables = new T[] { };
            }
        }
        public void save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(T[]));
            TextWriter writer = new StreamWriter(fileName);
            ser.Serialize(writer, variables);
            writer.Close();
        }
        public int getNewId()
        {
            return variables.Length + 1;
        }
        public void add(string name, params double[] points)
        {
            Variable variable = new Variable(this.getNewId(), name, points);
            this.add((T)Convert.ChangeType(variable, typeof(T)));
        }
        public void add(Variable eatVariant, Variable eatCount, Variable peopleCount)
        {
            this.add((T)Convert.ChangeType(new Condition(eatVariant, eatCount, peopleCount), typeof(T)));
        }
        public void add(T newVariable)
        {
            T[] variables = (T[])this.variables.Clone();
            Array.Resize(ref variables, variables.Length + 1);
            variables[this.variables.Length] = newVariable;
            this.variables = (T[])variables.Clone();
            save();
        }
        public bool checkName(string name)
        {
            if (typeof(T) == typeof(Variable))
            {
                Variable[] newVariables = variables as Variable[];
                foreach (Variable variable in newVariables)
                    if (name.ToLower() == variable.name.ToLower()) return false;
            }
            return true;
        }
        public int count()
        {
            return variables.Length;
        }
        public Variable get(string name)
        {
            if (typeof(T) == typeof(Variable))
            {
                Variable[] newVariables = variables as Variable[];
                foreach (Variable variable in newVariables)
                    if (variable.name == name) return variable;
            }
            return null;
        }
        public List<string> getNamesList()
        {
            List<string> names = new List<string>();
            if (typeof(T) == typeof(Variable))
            {
                Variable[] newVariables = variables as Variable[];
                foreach (Variable variable in newVariables)
                    names.Add(variable.name);
                return names;
            }
            return null;
        }
    }
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
        public DishCategory get(string name)
        {
            foreach (DishCategory category in categories)
                if (category.name == name) return category;
            return null;
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
    [Serializable]
    public class Variable
    {
        public int id { set; get; }
        public string name { set; get; }
        public Chart chart { set; get; }
        public Variable()
        {
            id = 0;
            name = "";
            chart = new Chart();
        }
        public Variable(int id, string name, params double[] points)
        {
            this.id = id;
            this.name = name;
            this.chart = new Chart(points);
        }
    }
    public class PeopleCountes : DataTemplate<Variable>
    {
        public PeopleCountes()
            : base("PeopleCountes.xml")
        {

        }
    }
    public class EatVariantes : DataTemplate<Variable>
    {
        public EatVariantes()
            : base("EatVariantes.xml")
        {

        }
    }
    public class EatCountes : DataTemplate<Variable>
    {
        public EatCountes()
            : base("EatCountes.xml")
        {

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
    public class PereferenceData
    {
        public DishCategory[] dishCategories { set; get; }
        public Variable eatVariant { set; get; }
        public int peopleNumber { set; get; }
        public PereferenceData()
        {
            dishCategories = null;
            eatVariant = null;
            peopleNumber = 0;
        }
        public PereferenceData(Variable eatVariant, int peopleNumber, params DishCategory[] dishCategories)
        {
            this.eatVariant = eatVariant;
            this.dishCategories = dishCategories;
            this.peopleNumber = peopleNumber;
        }
    }
    [Serializable]
    public class Condition
    {
        public Variable eatVariant { set; get; }
        public Variable peopleCount { set; get; }
        public Variable eatCount { set; get; }
        public Condition()
        {
            eatCount = new Variable();
            eatVariant = new Variable();
            peopleCount = new Variable();
        }
        public Condition(Variable eatVariant, Variable eatCount, Variable peopleCount)
        {
            this.eatCount = eatCount;
            this.eatVariant = eatVariant;
            this.peopleCount = peopleCount;
        }
    }
    public class Conditions : DataTemplate<Condition>
    {
        public Conditions() : base("Conditions.xml") { 

}
    }
}

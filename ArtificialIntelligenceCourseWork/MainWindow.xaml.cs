using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArtificialIntelligenceCourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dishes dishes { set; get; }
        private DishCategories dishCategories { set; get; }
        private EatVariantes eatVariantes { set; get; }
        private PereferenceData pereferenceData { set; get; }
        private PeopleCountes peopleCountes { set; get; }
        private EatCountes eatCountes { set; get; }
        private Conditions conditions { set; get; }
        private double calories { set; get; }
        private double peopleCount { set; get; }
        public MainWindow()
        {
            InitializeComponent();

            calories = 0;
            peopleCount = 0;
            dishes = new Dishes();
            dishCategories = new DishCategories();
            eatVariantes = new EatVariantes();
            peopleCountes = new PeopleCountes();
            eatCountes = new EatCountes();
            conditions = new Conditions();
            pereferenceData = null;
        }
        public void generateEatVariants()
        {
            int radioHeight = 23, i = 0;
            foreach (Variable eatVariant in eatVariantes.variables)
            {
                eatVariantsGb.Children.Add(createRadioButton(eatVariant.name, 10, radioHeight * i, check: i == 0));
                i++;
            }
        }
        /*public void generateCategories()
        {
            int checkHeight = 23, i = 0;
            foreach (DishCategory dishCategory in dishCategories.categories)
            {
                categoriesGb.Children.Add(createCheckBox(dishCategory.name, 10, checkHeight * i));
                i++;
            }
        }
        public void readFromFields()
        {
            List<DishCategory> dishCategories = new List<DishCategory>(){};
            Variable eatVariant = new Variable();
            foreach(RadioButton radioButton in eatVariantsGb.Children.OfType<RadioButton>())
                if ((bool) radioButton.IsChecked) eatVariant = this.eatVariantes.get(radioButton.Content.ToString());
            foreach(CheckBox checkBox in categoriesGb.Children.OfType<CheckBox>())
                if ((bool)checkBox.IsChecked) dishCategories.Add(this.dishCategories.get(checkBox.Content.ToString()));
            pereferenceData = new PereferenceData(eatVariant, int.Parse(peopleCountTb.Text), dishCategories.ToArray());
        }*/
        public CheckBox createCheckBox(string text, int left, int top)
        {
            return new CheckBox() { 
                Content = text,
                Margin = new Thickness(left, top, 0, 10),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
        }
        public RadioButton createRadioButton(string text, int left, int top, string group = "eatGroup", bool check = false)
        {
            return new RadioButton()
            {
                IsChecked = check,
                GroupName = group,
                Content = text,
                Margin = new Thickness(left, top, 0, 10),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategory window = new AddCategory(dishCategories);
            window.ShowDialog();
        }
        private void AddDish_Click(object sender, RoutedEventArgs e)
        {
            AddDish window = new AddDish(dishes, dishCategories);
            window.ShowDialog();
        }
        private void Calculate(object sender, RoutedEventArgs e)
        {
            calories = double.Parse(caloriesTb.Text);
            peopleCount = double.Parse(peopleCountTb.Text);
            fazzyfication();
        }
        private void fazzyfication()
        {
            double[] conditionValue = new double[conditions.count()];
            for (int i = 0; i < conditions.count(); i++)
                conditionValue[i] = Math.Min(
                    conditions.variables[i].eatVariant.chart.find(calories), 
                    conditions.variables[i].peopleCount.chart.find(peopleCount));
            int index = 0;
            double maxValue = conditionValue[0];
            for (int i = 1; i < conditionValue.Length; i++)
                if (maxValue < conditionValue[i])
                {
                    maxValue = conditionValue[i];
                    index = i;
                }
            double result1 = conditions.variables[index].eatCount.chart.findReverse(maxValue),
                result2 = conditions.variables[index].eatCount.chart.findReverse(maxValue, false);
            MessageBox.Show("Методом левого модального значения, найдено:" + result1 +
                    "\nМетодом правого модального значения, найдено:" + result2);
        }
        private void RedactEatCategores_Click(object sender, RoutedEventArgs e)
        {
            RedactVariable window = new RedactVariable(eatVariantes);
            window.ShowDialog();
        }
        private void RedactEatCountes_Click(object sender, RoutedEventArgs e)
        {
            RedactVariable window = new RedactVariable(eatCountes);
            window.ShowDialog();
        }
        private void RedactPeopleCountes_Click(object sender, RoutedEventArgs e)
        {
            RedactVariable window = new RedactVariable(peopleCountes);
            window.ShowDialog();
        }

        private void AddConditions_Click(object sender, RoutedEventArgs e)
        {
            AddConditions window = new AddConditions(eatVariantes, peopleCountes, eatCountes);
            window.ShowDialog();
        }
    }
}

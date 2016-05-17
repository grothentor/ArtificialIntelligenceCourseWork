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
        
        public MainWindow()
        {
            InitializeComponent();

            dishes = new Dishes();
            dishCategories = new DishCategories();
            eatVariantes = new EatVariantes();
            pereferenceData = null;
            generateFields();
        }
        public void generateFields()
        {
            generateEatVariants();
            generateCategories();
        }
        public void generateEatVariants()
        {
            int radioHeight = 23, i = 0;
            foreach (EatVariant eatVariant in eatVariantes.eatVariantes)
            {
                eatVariantsGb.Children.Add(createRadioButton(eatVariant.name, 10, radioHeight * i, check: i == 0));
                i++;
            }
        }
        public void generateCategories()
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
            EatVariant eatVariant = new EatVariant();
            foreach(RadioButton radioButton in eatVariantsGb.Children.OfType<RadioButton>())
                if ((bool) radioButton.IsChecked) eatVariant = this.eatVariantes.get(radioButton.Content.ToString());
            foreach(CheckBox checkBox in categoriesGb.Children.OfType<CheckBox>())
                if ((bool)checkBox.IsChecked) dishCategories.Add(this.dishCategories.get(checkBox.Content.ToString()));
            pereferenceData = new PereferenceData(eatVariant, int.Parse(peopleCountTb.Text), dishCategories.ToArray());
        }
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
            readFromFields();
        }
    }
}

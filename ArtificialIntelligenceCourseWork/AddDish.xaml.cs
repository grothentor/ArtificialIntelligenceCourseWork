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
using System.Windows.Shapes;

namespace ArtificialIntelligenceCourseWork
{
    /// <summary>
    /// Логика взаимодействия для AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        private DishCategories dishCategories { set; get; }
        private Dishes dishes { set; get; }
        public AddDish(Dishes dishes, DishCategories dishCategories)
        {
            InitializeComponent();
            this.dishCategories = dishCategories;
            this.dishes = dishes;
            categoriesCb.ItemsSource = this.dishCategories.getNamesList();  
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (dishes.checkName(nameTb.Text))
            {
                dishes.add(nameTb.Text, dishCategories.getId((string)categoriesCb.SelectedValue), int.Parse(caloriesTb.Text));
                if (MessageBoxResult.Yes != MessageBox.Show(
                        "Блюдо успешно добавлена\nХотите продолжить добавление блюд?",
                        "Сообщение",
                        MessageBoxButton.YesNo)) this.Close();
                else nameTb.Text = "";
            }
            else MessageBox.Show("Название не подходит либо уже используется", "Ошибка");
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            int length = dishCategories.count();
            AddCategory window = new AddCategory(dishCategories, true);
            window.ShowDialog();
            if (length != dishCategories.count())
            {
                categoriesCb.ItemsSource = new List<string>();
                categoriesCb.ItemsSource = this.dishCategories.getNamesList();
                categoriesCb.SelectedValue = dishCategories.get();
            }
        }
    }
}

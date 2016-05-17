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
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        private DishCategories dishCategories { set; get; }
        private bool addOne = false;
        public AddCategory(DishCategories dishCategories, bool addOne = false)
        {
            InitializeComponent();
            this.dishCategories = dishCategories;
            this.addOne = addOne;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (dishCategories.checkName(nameTb.Text))
            {
                dishCategories.add(nameTb.Text);
                if (addOne || MessageBoxResult.Yes != MessageBox.Show(
                        "Категория успешно добавлена\nХотите продолжить добавление категорий",
                        "Сообщение",
                        MessageBoxButton.YesNo)) this.Close();
                else nameTb.Text = "";
            }
            else MessageBox.Show("Название не подходит либо уже используется", "Ошибка");
        }
    }
}

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
    /// Логика взаимодействия для RedactVariable.xaml
    /// </summary>
    public partial class RedactVariable : Window
    {
        public DataTemplate<Variable> dataTemplate;
        public RedactVariable(DataTemplate<Variable> dataTemplate)
        {
            InitializeComponent();
            this.dataTemplate = dataTemplate;
            VariableCb.ItemsSource = dataTemplate.getNamesList();
            VariableCb.SelectedIndex = 0;
        }

        private void Variable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Variable variable = dataTemplate.get(VariableCb.SelectedValue.ToString());
            NameTb.Text = variable.name;
            StartPoint.Text = variable.chart.startPoint.ToString().Replace('.', ',');
            SecondPoint.Text = variable.chart.secondPoint.ToString().Replace('.', ',');
            ThirdPoint.Text = variable.chart.thirdPoint.ToString().Replace('.', ',');
            EndPoint.Text = variable.chart.endPoint.ToString().Replace('.', ',');
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            foreach (TextBox text in MainGrid.Children.OfType<TextBox>())
                if (text.Text == "") flag = false;
            if (flag)
            {
                Variable variable = dataTemplate.get(VariableCb.SelectedValue.ToString());
                variable.name = NameTb.Text;
                variable.chart.startPoint = double.Parse(StartPoint.Text);
                variable.chart.secondPoint = double.Parse(SecondPoint.Text);
                variable.chart.thirdPoint = double.Parse(ThirdPoint.Text);
                variable.chart.endPoint = double.Parse(EndPoint.Text);
                dataTemplate.save();
                if (MessageBoxResult.Yes == MessageBox.Show(
                    "Изменения сохранены. \nПродолжить редактирование?",
                    "Уведомление",
                    MessageBoxButton.YesNo))
                {
                    if (0 != variable.id) VariableCb.SelectedIndex = 0;
                    else VariableCb.SelectedIndex = dataTemplate.count() - 1;
                    VariableCb.ItemsSource = dataTemplate.getNamesList();
                    VariableCb.SelectedIndex = variable.id;
                }
                else this.Close();

            }
            else MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
        }

    }
}

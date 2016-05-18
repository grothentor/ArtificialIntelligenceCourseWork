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
    /// Логика взаимодействия для AddConditions.xaml
    /// </summary>
    public partial class AddConditions : Window
    {
        private EatCountes eatCountes { set; get; }
        private EatVariantes eatVariantes { set; get; }
        private PeopleCountes peopleCountes { set; get; }
        private Conditions conditions { set; get; }
        public AddConditions(EatVariantes eatVariantes, PeopleCountes peopleCountes, EatCountes eatCountes)
        {
            conditions = new Conditions();
            InitializeComponent();
            this.eatCountes = eatCountes;
            this.peopleCountes = peopleCountes;
            this.eatVariantes = eatVariantes;
            eatCounts1.ItemsSource = eatCountes.getNamesList();
            peopleCounts1.ItemsSource = peopleCountes.getNamesList();
            eatVariantes1.ItemsSource = eatVariantes.getNamesList();
            DelCon1.Click += Delete_Click;
            BuildConditions();
        }
        public static void AddToChild(Grid G)
        {
            int i = 0, n = 0, MaxHeigth = 0;
            foreach (Control element in G.Children)
                if (MaxHeigth < (int)(element.Margin.Top + element.Height)) MaxHeigth = (int)(element.Margin.Top + element.Height);
            foreach (Label element in G.Children.OfType<Label>())
            {
                if (element.Name.IndexOf("1") != -1 && element.Name.IndexOf("1") == element.Name.Length - 1) n++;
                i++;
            }
            if (n == 0)
                foreach (TextBox element in G.Children.OfType<TextBox>())
                {
                    if (element.Name.IndexOf("1") != -1 && element.Name.IndexOf("1") == element.Name.Length - 1) n++;
                    i++;
                }
            if (n == 0)
                foreach (ComboBox element in G.Children.OfType<ComboBox>())
                {
                    if (element.Name.IndexOf("1") != -1 && element.Name.IndexOf("1") == element.Name.Length - 1) n++;
                    i++;
                }
            i = i / n;
            List<ComboBox> combos = new List<ComboBox>();
            foreach (ComboBox element in G.Children.OfType<ComboBox>())
            {
                if (element.Name.IndexOf("1") != -1)
                {
                    string name = element.Name.Replace("1", "") + (i + 1).ToString();
                    Thickness Thick = new Thickness { Left = element.Margin.Left, Top = MaxHeigth + element.Margin.Top - 6, Bottom = element.Margin.Bottom, Right = element.Margin.Right };
                    combos.Add(new ComboBox
                    {
                        SelectedIndex = -1,
                        Name = name,
                        Margin = Thick,
                        ItemsSource = element.ItemsSource,
                        HorizontalAlignment = element.HorizontalAlignment,
                        VerticalAlignment = element.VerticalAlignment,
                        Width = element.Width,
                        Height = element.Height,
                    });
                }
                else break;
            }
            List<DatePicker> dates = new List<DatePicker>();
            foreach (DatePicker element in G.Children.OfType<DatePicker>())
            {
                if (element.Name.IndexOf("1") != -1)
                {
                    string name = element.Name.Replace("1", "") + (i + 1).ToString();
                    Thickness Thick = new Thickness { Left = element.Margin.Left, Top = MaxHeigth + element.Margin.Top - 6, Bottom = element.Margin.Bottom, Right = element.Margin.Right };
                    dates.Add(new DatePicker
                    {
                        Name = name,
                        Text = element.Text,
                        Margin = Thick,
                        HorizontalAlignment = element.HorizontalAlignment,
                        VerticalAlignment = element.VerticalAlignment,
                        Width = element.Width,
                        Height = element.Height,
                    });
                }
                else break;
            }
            List<TextBox> texts = new List<TextBox>();
            foreach (TextBox element in G.Children.OfType<TextBox>())
            {
                if (element.Name.IndexOf("1") != -1)
                {
                    string name = element.Name.Replace("1", "") + (i + 1).ToString();
                    Thickness Thick = new Thickness { Left = element.Margin.Left, Top = MaxHeigth + element.Margin.Top - 6, Bottom = element.Margin.Bottom, Right = element.Margin.Right };
                    texts.Add(new TextBox
                    {
                        Name = name,
                        Margin = Thick,
                        Text = "0",
                        TextWrapping = element.TextWrapping,
                        HorizontalAlignment = element.HorizontalAlignment,
                        VerticalAlignment = element.VerticalAlignment,
                        Width = element.Width,
                        Height = element.Height,
                        IsReadOnly = element.IsReadOnly,

                    });
                }
                else break;
            }
            List<Label> labels = new List<Label>();
            foreach (Label element in G.Children.OfType<Label>())
            {
                if (element.Name.IndexOf("1") != -1)
                {
                    string name = element.Name.Replace("1", "") + (i + 1).ToString();
                    Thickness Thick = new Thickness { Left = element.Margin.Left, Top = MaxHeigth + element.Margin.Top - 6, Bottom = element.Margin.Bottom, Right = element.Margin.Right };
                    labels.Add(new Label
                    {
                        Name = name,
                        Margin = Thick,
                        Content = element.Content,
                        HorizontalAlignment = element.HorizontalAlignment,
                        VerticalAlignment = element.VerticalAlignment,
                    });
                }
                else break;
            }
            foreach (Button element in G.Children.OfType<Button>())
            {
                if (element.Name.IndexOf("Add") == -1 && element.Name != "" && element.Name.IndexOf("Delete") == -1)
                {
                    Thickness Thick = new Thickness { Left = element.Margin.Left, Top = MaxHeigth + element.Margin.Top - 6, Bottom = element.Margin.Bottom, Right = element.Margin.Right };
                    Button button1 = new Button
                    {
                        Content = "-",
                        Name = "Del" + G.Name.Remove(3) + (G.Children.OfType<Button>().Count()).ToString(),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = Thick,
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 15,
                        Height = 17,
                        Padding = new Thickness(0)
                    };
                    button1.Click += Delete_Click;
                    G.Children.Add(button1);
                    break;
                }
            }
            for (int k = 0; k < labels.Count; k++) G.Children.Add(labels[k]);
            for (int k = 0; k < combos.Count; k++) G.Children.Add(combos[k]);
            for (int k = 0; k < texts.Count; k++) G.Children.Add(texts[k]);
            for (int k = 0; k < dates.Count; k++) G.Children.Add(dates[k]);
        }
        public static void RemoveFromCild(Grid G, int i)
        {
            bool flag = true;
            while (flag)
            {
                foreach (TextBox element in G.Children.OfType<TextBox>())
                    if (element.Name.IndexOf(i.ToString()) != -1) { G.Children.Remove(element); flag = false; break; }
                if (!flag) flag = true; else flag = false;
            }
            flag = true;
            while (flag)
            {
                foreach (ComboBox element in G.Children.OfType<ComboBox>())
                    if (element.Name.IndexOf(i.ToString()) != -1) { G.Children.Remove(element); flag = false; break; }
                if (!flag) flag = true; else flag = false;
            }
            flag = true;
            while (flag)
            {
                foreach (DatePicker element in G.Children.OfType<DatePicker>())
                    if (element.Name.IndexOf(i.ToString()) != -1) { G.Children.Remove(element); flag = false; break; }
                if (!flag) flag = true; else flag = false;
            }
            flag = true;
            while (flag)
            {
                foreach (Label element in G.Children.OfType<Label>())
                    if (element.Name.IndexOf(i.ToString()) != -1) { G.Children.Remove(element); flag = false; break; }
                if (!flag) flag = true; else flag = false;
            }
            flag = true;
            while (flag)
            {
                foreach (Button element in G.Children.OfType<Button>())
                    if (element.Name.IndexOf(i.ToString()) != -1) { G.Children.Remove(element); flag = false; break; }
                if (!flag) flag = true; else flag = false;
            }
            RebildChild(G);
        }
        public static void RebildChild(Grid G)
        {
            int i = 0, n = 0, j = 0, MaxHeight = 0;
            foreach (Control element in G.Children)
            {
                if (element.Name.IndexOf("1") != -1)
                    if (MaxHeight < (int)(element.Margin.Top + element.Height)) MaxHeight = (int)(element.Margin.Top + element.Height);
            }
            if (MaxHeight == 0)
            {
                foreach (Control element in G.Children)
                {
                    if (element.Name.IndexOf("2") != -1)
                        if (MaxHeight < (int)(element.Margin.Top + element.Height)) MaxHeight = (int)(element.Margin.Top + element.Height);
                }
                MaxHeight -= 14;
                MaxHeight /= 2;
                MaxHeight += 10;
            }
            MaxHeight -= 6;
            string s = "1";
            foreach (TextBox element in G.Children.OfType<TextBox>())
            {
                if (element.Name.IndexOf(s) != -1) n++; else if (n > 0) break;
                if (n == 0) { s = "2"; if (element.Name.IndexOf(s) != -1) n++; else break; }
                if (n == 0) break;
            }
            i = 1;
            j = 1;
            foreach (TextBox element in G.Children.OfType<TextBox>())
            {
                if (element.Name.IndexOf(i.ToString()) == -1)
                {
                    element.Name = element.Name.Remove(element.Name.IndexOf((i + 1).ToString())) + i.ToString();
                    element.Margin = new Thickness(element.Margin.Left, element.Margin.Top - MaxHeight, element.Margin.Right, element.Margin.Bottom);
                }
                if (j == n) { i++; j = 1; } else j++;
            }
            n = 0;
            s = "1";
            foreach (DatePicker element in G.Children.OfType<DatePicker>())
            {
                if (element.Name.IndexOf(s) != -1) n++; else if (n > 0) break;
                if (n == 0) { s = "2"; if (element.Name.IndexOf(s) != -1) n++; else break; }
                if (n == 0) break;
            }
            i = 1;
            j = 1;
            foreach (DatePicker element in G.Children.OfType<DatePicker>())
            {
                if (element.Name.IndexOf(i.ToString()) == -1)
                {
                    element.Name = element.Name.Remove(element.Name.IndexOf((i + 1).ToString())) + i.ToString();
                    element.Margin = new Thickness(element.Margin.Left, element.Margin.Top - MaxHeight, element.Margin.Right, element.Margin.Bottom);
                }
                if (j == n) { i++; j = 1; } else j++;
            }
            n = 1;
            i = 2;
            foreach (Button element in G.Children.OfType<Button>())
            {
                if (element.Name != "Add" && element.Name != "")
                {
                    if (element.Name.IndexOf("Del") != -1) i--;
                    if (element.Name.IndexOf(i.ToString()) == -1)
                    {
                        element.Name = element.Name.Remove(element.Name.IndexOf((i + 1).ToString())) + i.ToString();
                        element.Margin = new Thickness(element.Margin.Left, element.Margin.Top - MaxHeight, element.Margin.Right, element.Margin.Bottom);
                    }
                    i++;
                    if (element.Name.IndexOf("Del") != -1) i++;
                }
            }
            n = 0;
            s = "1";
            foreach (ComboBox element in G.Children.OfType<ComboBox>())
            {
                if (element.Name.IndexOf(s) != -1) n++; else if (n > 0) break;
                if (n == 0) { s = "2"; if (element.Name.IndexOf(s) != -1) n++; else break; }
                if (n == 0) break;
            }
            i = 1;
            j = 1;
            foreach (ComboBox element in G.Children.OfType<ComboBox>())
            {
                if (element.Name.IndexOf(i.ToString()) == -1)
                {
                    element.Name = element.Name.Remove(element.Name.IndexOf((i + 1).ToString())) + i.ToString();
                    element.Margin = new Thickness(element.Margin.Left, element.Margin.Top - MaxHeight, element.Margin.Right, element.Margin.Bottom);
                }
                if (j == n) { i++; j = 1; } else j++;
            }
            n = 0;
            s = "1";
            foreach (Label element in G.Children.OfType<Label>())
            {
                if (element.Name.IndexOf(s) != -1) n++; else if (n > 0) break;
                if (n == 0) { s = "2"; if (element.Name.IndexOf(s) != -1) n++; else break; }
                if (n == 0) break;
            }
            i = 1;
            j = 1;
            foreach (Label element in G.Children.OfType<Label>())
            {
                if (element.Name.IndexOf(i.ToString()) == -1)
                {
                    element.Name = element.Name.Remove(element.Name.IndexOf((i + 1).ToString())) + i.ToString();
                    element.Margin = new Thickness(element.Margin.Left, element.Margin.Top - MaxHeight, element.Margin.Right, element.Margin.Bottom);
                }
                if (j == n) { i++; j = 1; } else j++;
            }
        }
        public static void Delete_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            Grid NewGrid = (Grid)((Button)e.OriginalSource).Parent;
            foreach (Button butt in NewGrid.Children.OfType<Button>())
                if (e.OriginalSource.Equals(butt)) i = int.Parse(butt.Name.Remove(0, 6));
            if (i > 0 && NewGrid.Children.OfType<Button>().Count() > 2) RemoveFromCild(NewGrid, i);
        }
        private void AddNewItems_Click(object sender, RoutedEventArgs e)
        {
            Grid NewGrid = (Grid)((Button)e.OriginalSource).Parent;
            AddToChild(NewGrid);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Condition> conditions = new List<Condition>() {};
            int i = 0, j = 0;
            foreach (ComboBox combo in ConditionsGrid.Children.OfType<ComboBox>())
            {
                if (0 == j)
                {
                    conditions.Add(new Condition());
                    conditions[i].eatVariant = eatVariantes.get(combo.SelectedValue.ToString());
                }
                if (1 == j) conditions[i].peopleCount = peopleCountes.get(combo.SelectedValue.ToString());
                if (2 == j)
                {
                    conditions[i].eatCount = eatCountes.get(combo.SelectedValue.ToString());
                    i++;
                    j = 0;
                }
                else j++;
            }
            this.conditions.variables = conditions.ToArray();
            this.conditions.save();
            MessageBox.Show("Условия успешно обновлены");
            this.Close();
        }
        public void BuildConditions()
        {
            for (int i = 0; i < conditions.count(); i++)
            {
                if (i > 0) AddToChild(ConditionsGrid);
                foreach (ComboBox combo in ConditionsGrid.Children.OfType<ComboBox>())
                    if (combo.Name.IndexOf((i + 1).ToString()) != -1)
                    {
                        if (-1 != combo.Name.IndexOf("eatCounts"))  combo.SelectedValue = conditions.variables[i].eatCount.name;
                        if (-1 != combo.Name.IndexOf("eatVariantes")) combo.SelectedValue = conditions.variables[i].eatVariant.name;
                        if (-1 != combo.Name.IndexOf("peopleCounts")) combo.SelectedValue = conditions.variables[i].peopleCount.name;
                    }
            }
        }
    }
}

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
using System.Data;

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for studentsPanel.xaml
    /// </summary>
    public partial class studentsPanel : Window
    {
        DataAccess db = new DataAccess();
        public Color normalColor = new Color();
        public Color selectedColor = new Color();
        List<Students> studentsList = new List<Students>();
        List<Object> students2List = new List<Object>();
        DataTable myDataTable = new DataTable();
        

        public studentsPanel()
        {
            InitializeComponent();
            this.Closed += new EventHandler(studentsPanel_Closed);
            normalColor = Color.FromArgb(0xFF, 0x42, 0x42, 0x42);
            selectedColor = Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E);

            changeBackgroundColor(seeAllViewButton);
            searchComboBox.Items.Add("نام کاربری");
            searchComboBox.Items.Add("کلمه عبور");
            searchComboBox.Items.Add("نام");
            searchComboBox.Items.Add("نام خانوادگی");
            searchComboBox.Items.Add("نام پدر");
            searchComboBox.Items.Add("شماره تماس");
            searchComboBox.Items.Add("آدرس");
            searchComboBox.Items.Add("توضیحات");
            searchComboBox.Items.Add("نام کلاس");
            searchComboBox.Items.Add("جنسیت");
        }
        void studentsPanel_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            studentsList = db.GetStudents();
            studentsDataGrid.ItemsSource = studentsList;
            studentsDataGrid.IsReadOnly = true;
            try
             {
                // studentsDataGrid.Columns.RemoveAt(7);
                 studentsDataGrid.Columns[0].Header = " Id ";
                 studentsDataGrid.Columns[1].Header = " نام کاربری";
                 studentsDataGrid.Columns[2].Header = " کلمه عبور ";
                 studentsDataGrid.Columns[3].Header = " نام ";
                 studentsDataGrid.Columns[4].Header = " نام خانوادگی ";
                 studentsDataGrid.Columns[5].Header = " نام پدر ";
                 studentsDataGrid.Columns[6].Header = " شماره تماس ";
                 studentsDataGrid.Columns[7].Header = " آدرس ";
                 studentsDataGrid.Columns[8].Header = " توضیحات ";
                 studentsDataGrid.Columns[9].Header = " نام کلاس ";
                 studentsDataGrid.Columns[10].Header = " جنسیت ";
            }
             catch { }
        }
        private void changeBackgroundColor(Button sender)
        {
            addViewButton.Background = new SolidColorBrush(normalColor);
            changeViewButton.Background = new SolidColorBrush(normalColor);
            seeAllViewButton.Background = new SolidColorBrush(normalColor);
            deleteViewButton.Background = new SolidColorBrush(normalColor);
            karnameViewButton.Background = new SolidColorBrush(normalColor);
            sender.Background = new SolidColorBrush(selectedColor);
        }
        private void addViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            studentsPanelPage.Content = new create_studentPage();
            VeiwAll(0);
        }

        private void changeViewButton_Click(object sender, RoutedEventArgs e)
        {

            changeBackgroundColor((Button)sender);
            studentsPanelPage.Content = new change_studentPage();
            VeiwAll(0);
        }

        public void seeAllViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            studentsPanelPage.Content = new seeAll_studentsPage();
            UpdateDataGrid();
            VeiwAll(1);
        }

        private void deleteViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            studentsPanelPage.Content = new delete_studentPage();
            VeiwAll(0);
        }

        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= studentsPanel_Closed;
            this.Close();
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        }
        private void VeiwAll(int status)
        {
            if (status == 0)
            {
                studentsDataGrid.Visibility = Visibility.Hidden;
                searchComboBox.Visibility = Visibility.Hidden;
                searchLable.Visibility = Visibility.Hidden;
                searchTextBox.Visibility = Visibility.Hidden;
                lable1.Visibility = Visibility.Hidden;
            }
            if (status == 1)
            {
                studentsDataGrid.Visibility = Visibility.Visible;
                searchComboBox.Visibility = Visibility.Visible;
                searchLable.Visibility = Visibility.Visible;
                searchTextBox.Visibility = Visibility.Visible;
                lable1.Visibility = Visibility.Visible;
            }
        }
        private void changeRightClick(object sender, RoutedEventArgs e)
        {
            Students mystudent = (Students)studentsDataGrid.SelectedItem;
            int selectedId = studentsDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                Globals.cache = mystudent.username;
                change_studentPage changepage = new change_studentPage();
                changeViewButton_Click(changeViewButton, e);
            }
        }
        private void karnameRightClick(object sender, RoutedEventArgs e)
        {
            Students mystudent = (Students)studentsDataGrid.SelectedItem;
            int selectedId = studentsDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                Globals.cache = mystudent.username;
                change_studentPage changepage = new change_studentPage();
                karnameViewButton_Click(karnameViewButton, e);
            }
        }
        private void deleteRightClick(object sender, RoutedEventArgs e)
        {
            Students mystudent = (Students)studentsDataGrid.SelectedItem;
            int selectedId = studentsDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                Globals.cache = mystudent.username;
                change_studentPage changepage = new change_studentPage();
                deleteViewButton_Click(deleteViewButton, e);
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Students> searchStudentsList = new List<Students>();
            int selectedIndex = searchComboBox.SelectedIndex;
            string[] indextext = { "username", "_password", "_name","lastname","fathername", "phone", "_address", "info", "classname","gender" };
            string searchValue = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                if (selectedIndex != -1)
                {
                    searchStudentsList = db.SearchStudents(indextext[selectedIndex], searchValue);
                    studentsDataGrid.ItemsSource = searchStudentsList;
                    studentsDataGrid.IsReadOnly = true;
                }
            }
            else
            {
                UpdateDataGrid();
            }
        }

        private void karnameViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            studentsPanelPage.Content = new Karname_page();
            VeiwAll(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // This part of the code is very useful
            //But it is not used anywhere
            string query = "select _name , _phonenumber from students";
            myDataTable = db.Get_Table(query);
            
            List<DataRow> list = myDataTable.AsEnumerable().ToList();
            
            string mystring = list[5]["_name"].ToString();
            // MessageBox.Show(mystring);

            //DataRow myrow = (DataRow)studentsDataGrid.SelectedItem;
            //DataRow myrow2 = ((DataRowView)studentsDataGrid.SelectedItem).Row;
            //DataRowView row = (DataRowView)studentsDataGrid.Items.CurrentItem;
            int itemId = studentsDataGrid.SelectedIndex;
            if (itemId>=0)
                MessageBox.Show(myDataTable.Rows[itemId][0].ToString());


            studentsDataGrid.ItemsSource = myDataTable.DefaultView;
        }
    }
}


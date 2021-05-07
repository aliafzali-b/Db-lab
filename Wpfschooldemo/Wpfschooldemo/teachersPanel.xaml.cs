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
    /// Interaction logic for teachersPanel.xaml
    /// </summary>
    public partial class teachersPanel : Window
    {
        DataAccess db = new DataAccess();
        public Color normalColor = new Color();
        public Color selectedColor = new Color();
        List<Teachers> teachersList = new List<Teachers>();
        DataTable myDataTable = new DataTable();

        public teachersPanel()
        {
            InitializeComponent();
            this.Closed += new EventHandler(teachersPanel_Closed);
            normalColor = Color.FromArgb(0xFF, 0x42, 0x42, 0x42);
            selectedColor = Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E);
            changeBackgroundColor(seeAllViewButton);
        }
        void teachersPanel_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            teachersList = db.GetTeachers();
            teachersDataGrid.ItemsSource = teachersList;
            teachersDataGrid.IsReadOnly = true;
            try
            {
                // studentsDataGrid.Columns.RemoveAt(7);
                teachersDataGrid.Columns[0].Header = " Id ";
                teachersDataGrid.Columns[1].Header = " نام کاربری";
                teachersDataGrid.Columns[2].Header = " کلمه عبور ";
                teachersDataGrid.Columns[3].Header = " نام ";
                teachersDataGrid.Columns[4].Header = " نام خانوادگی ";
                teachersDataGrid.Columns[5].Header = " نام پدر ";
                teachersDataGrid.Columns[6].Header = " شماره تماس ";
                teachersDataGrid.Columns[7].Header = " آدرس ";
                teachersDataGrid.Columns[8].Header = " توضیحات ";
                teachersDataGrid.Columns[9].Header = " نام کلاس ";
                teachersDataGrid.Columns[10].Header = " جنسیت ";
            }
            catch { }
        }
        private void changeBackgroundColor(Button sender)
        {
            addViewButton.Background = new SolidColorBrush(normalColor);
            changeViewButton.Background = new SolidColorBrush(normalColor);
            specialtyViewButton.Background = new SolidColorBrush(normalColor);
            seeAllViewButton.Background = new SolidColorBrush(normalColor);
            deleteViewButton.Background = new SolidColorBrush(normalColor);
            sender.Background = new SolidColorBrush(selectedColor);
        }

        ///-------------------------------------------------------------------------------------buttons functions
        private void addViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            teachersPanelPage.Content = new create_teacherPage();
            VeiwAll(0);
        }
        private void changeViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            teachersPanelPage.Content = new change_teacherPage();
            VeiwAll(0);
        }
        private void specialtyViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            teachersPanelPage.Content = new teacherClasses();
            VeiwAll(0);
        }
        public void seeAllViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            teachersPanelPage.Content = new seeAll_teachersPage();
            UpdateDataGrid();
            VeiwAll(1);
        }
        private void deleteViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            teachersPanelPage.Content = new delete_teacherPage();
            VeiwAll(0);
        }
        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= teachersPanel_Closed;
            this.Close();
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        }
        private void VeiwAll(int status)
        {
            if (status == 0)
            {
                teachersDataGrid.Visibility = Visibility.Hidden;
                searchComboBox.Visibility = Visibility.Hidden;
                searchLable.Visibility = Visibility.Hidden;
                searchTextBox.Visibility = Visibility.Hidden;
                lable1.Visibility = Visibility.Hidden;
            }
            if (status == 1)
            {
                teachersDataGrid.Visibility = Visibility.Visible;
                searchComboBox.Visibility = Visibility.Visible;
                searchLable.Visibility = Visibility.Visible;
                searchTextBox.Visibility = Visibility.Visible;
                lable1.Visibility = Visibility.Visible;
            }
        }
        ///------------------------------------------------------------------------------------button functions ends
       


        ///------------------------------------------------------------------------------------Right click functions
        private void changeRightClick(object sender, RoutedEventArgs e)
        {
            Teachers myteacher = (Teachers)teachersDataGrid.SelectedItem;
            int selectedId = teachersDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                Globals.cache = myteacher.username;
                //change_studentPage changepage = new change_studentPage();
                changeViewButton_Click(changeViewButton, e);
            }
        }
        private void specialtyRightClick(object sender, RoutedEventArgs e)
        {
            Teachers myteacher = (Teachers)teachersDataGrid.SelectedItem;
            int selectedId = teachersDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                Globals.cache = myteacher.username;
                //change_studentPage changepage = new change_studentPage();
                specialtyViewButton_Click(specialtyViewButton, e);
            }
        }
        private void deleteRightClick(object sender, RoutedEventArgs e)
        {
            Teachers myteacher = (Teachers)teachersDataGrid.SelectedItem;
            int selectedId = teachersDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                Globals.cache = myteacher.username;
                //change_studentPage changepage = new change_studentPage();
                deleteViewButton_Click(deleteViewButton, e);
            }
        }
        ///------------------------------------------------------------------------------------Right click functions ends
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Students> searchStudentsList = new List<Students>();
            int selectedIndex = searchComboBox.SelectedIndex;
            string[] indextext = { "username", "_password", "_name", "lastname", "fathername", "phone", "_address", "info", "classname", "gender" };
            string searchValue = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                if (selectedIndex != -1)
                {
                    searchStudentsList = db.SearchStudents(indextext[selectedIndex], searchValue);
                    teachersDataGrid.ItemsSource = searchStudentsList;
                    teachersDataGrid.IsReadOnly = true;
                }
            }
            else
            {
                UpdateDataGrid();
            }
        }

    }
}

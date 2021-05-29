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

            searchComboBox.Items.Add("نام کاربری");
            searchComboBox.Items.Add("کلمه عبور");
            searchComboBox.Items.Add("نام");
            searchComboBox.Items.Add("نام خانوادگی");
            searchComboBox.Items.Add("تخصص");
            searchComboBox.Items.Add("شماره تماس");
            searchComboBox.Items.Add("پست الکترونیک");
            searchComboBox.Items.Add("جنسیت");
            searchComboBox.Items.Add("کلاس");
            searchComboBox.Items.Add("درس");
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
            teachersDataGrid.ContextMenu.IsEnabled = true;
            try
            {
                teachersDataGrid.Columns.RemoveAt(9);
                teachersDataGrid.Columns[0].Header = " Id ";
                teachersDataGrid.Columns[1].Header = " نام کاربری";
                teachersDataGrid.Columns[2].Header = " کلمه عبور ";
                teachersDataGrid.Columns[3].Header = " نام ";
                teachersDataGrid.Columns[4].Header = " نام خانوادگی ";
                teachersDataGrid.Columns[5].Header = " تخصص ";
                teachersDataGrid.Columns[6].Header = " شماره تماس ";
                teachersDataGrid.Columns[7].Header = " پست الکترونیک ";
                teachersDataGrid.Columns[8].Header = " جنسیت ";
                teachersDataGrid.Columns[9].Header = " کلاس ";
                teachersDataGrid.Columns[10].Header = " درس ";
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
            FunctionViewButton.Background = new SolidColorBrush(normalColor);
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
            searchTextBox.Text = "";
            UpdateDataGrid();
            VeiwAll(1);
        }
        private void deleteViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            teachersPanelPage.Content = new delete_teacherPage();
            VeiwAll(0);
        }
        private void FunctionViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            teachersPanelPage.Content = new function_teacherPage();
            VeiwAll(0);
        }
        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Globals.cache = "";
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
            List<Teachers> searchTeachersList = new List<Teachers>();
            int selectedIndex = searchComboBox.SelectedIndex;
            string[] indextext = { "username", "_password", "_name", "lastname", "Expert", "phone", "email", "gender","courses","class"};
            string searchValue = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                if (selectedIndex != -1 && selectedIndex<8)
                {
                    searchTeachersList = db.SearchTeachers(indextext[selectedIndex], searchValue);
                    teachersDataGrid.ItemsSource = searchTeachersList;
                    teachersDataGrid.IsReadOnly = true;
                }
                if (selectedIndex == 8)
                {
                    DataTable myResult = db.Get_Table("select distinct Teacher.*,Courses._Name,Class._Name from teacher,cocot,Courses,Class where Teacher.TeacherID=CoCoT.TeacherID and Courses.CourseID=CoCoT.CourseID and CoCoT.ClassID=Class.ClassID and Class._Name like N'%"+ searchValue +"%'");
                    teachersDataGrid.Columns.Clear();
                    teachersDataGrid.ItemsSource = myResult.DefaultView;
                    teachersDataGrid.IsReadOnly = true;
                    teachersDataGrid.ContextMenu.IsEnabled = false;
                }
                if (selectedIndex == 9)
                {
                    DataTable myResult = db.Get_Table("select distinct Teacher.*,Courses._Name,Class._Name from teacher,cocot,Courses,Class where Teacher.TeacherID=CoCoT.TeacherID and Courses.CourseID=CoCoT.CourseID and CoCoT.ClassID=Class.ClassID and Courses._Name like N'%" + searchValue + "%'");
                    teachersDataGrid.Columns.Clear();
                    teachersDataGrid.ItemsSource = myResult.DefaultView;
                    teachersDataGrid.IsReadOnly = true;
                    teachersDataGrid.ContextMenu.IsEnabled = false;
                }
                try
                {
                    teachersDataGrid.Columns.RemoveAt(9);
                    teachersDataGrid.Columns[0].Header = " Id ";
                    teachersDataGrid.Columns[1].Header = " نام کاربری";
                    teachersDataGrid.Columns[2].Header = " کلمه عبور ";
                    teachersDataGrid.Columns[3].Header = " نام ";
                    teachersDataGrid.Columns[4].Header = " نام خانوادگی ";
                    teachersDataGrid.Columns[5].Header = " تخصص ";
                    teachersDataGrid.Columns[6].Header = " شماره تماس ";
                    teachersDataGrid.Columns[7].Header = " پست الکترونیک ";
                    teachersDataGrid.Columns[8].Header = " جنسیت ";
                    teachersDataGrid.Columns[8].Header = " کلاس ";
                    teachersDataGrid.Columns[8].Header = " درس ";
                }
                catch { }
            }
            else
            {
                UpdateDataGrid();
            }
        }
    }
}

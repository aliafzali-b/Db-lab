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

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for teacherClasses.xaml
    /// </summary>
    public partial class teacherClasses : Page
    {
        DataAccess db = new DataAccess();
        List<CoCoT> CoCoTList = new List<CoCoT>();
        List<Courses> coursesList = new List<Courses>();
        List<Classes> classesList = new List<Classes>();
        public teacherClasses()
        {
            InitializeComponent();
            usernameTextbox.Text = Globals.cache;
            ViewAddingPanel(0);
            if (db.GetTeacherIdByUsername(Globals.cache)>=0)
                ViewAddingPanel(1);
            foreach (Classes sample in db.GetClasses())
            {
                classesComboBox.Items.Add(sample._name);
            }
            foreach (Courses sample in db.GetCourses())
            {
                coursesComboBox.Items.Add(sample._name);
            }
        }
        void teacherClasses_Closed(object sender, EventArgs e)
        {
            Globals.cache = "";
        }
        private void usernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (usernameTextbox.Text == "")
                errorImage.Visibility = Visibility.Hidden;
            else
                errorImage.Visibility = Visibility.Visible;
            trueImage.Visibility = Visibility.Hidden;
            //deleteButton.IsEnabled = false;
            if (db.GetTeacherIdByUsername(usernameTextbox.Text) >= 0)
            {
                trueImage.Visibility = Visibility.Visible;
                errorImage.Visibility = Visibility.Hidden;
                ViewAddingPanel(1);
                UpdateDataGrid();
            }
            else
            {
                CoCoTdataGrid.ItemsSource = null;
                ViewAddingPanel(0);
                classesComboBox.SelectedItem = -1;
                coursesComboBox.SelectedItem = -1;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int courseid = coursesComboBox.SelectedIndex;
            int classid = classesComboBox.SelectedIndex;
            int teacherid = db.GetTeacherIdByUsername(usernameTextbox.Text);
            if (courseid >=0 && classid>=0)
            {
                db.insertIntoCoCoT(courseid, classid, teacherid);
                UpdateDataGrid();
            }
            else
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        void UpdateDataGrid()
        {
            int teacherId = db.GetTeacherIdByUsername(usernameTextbox.Text);
            CoCoTList = db.GetTeacherCoCotById(teacherId);
            CoCoTdataGrid.ItemsSource = CoCoTList;
            CoCoTdataGrid.IsReadOnly = true;
        }
        void ViewAddingPanel(int status) 
        {
            if (status == 0)
            {
                classesComboBox.Visibility = Visibility.Hidden;
                coursesComboBox.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Hidden;
                CoCoTdataGrid.Visibility = Visibility.Hidden;
                Lable1.Visibility = Visibility.Hidden;
                Lable2.Visibility = Visibility.Hidden;
                Lable3.Visibility = Visibility.Hidden;
            }
            else if (status == 1)
            {
                classesComboBox.Visibility = Visibility.Visible;
                coursesComboBox.Visibility = Visibility.Visible;
                addButton.Visibility = Visibility.Visible;
                CoCoTdataGrid.Visibility = Visibility.Visible;
                Lable1.Visibility = Visibility.Visible;
                Lable2.Visibility = Visibility.Visible;
                Lable3.Visibility = Visibility.Visible;
            }
        }
        private void deleteRightClick(object sender, RoutedEventArgs e)
        {
            CoCoT myCoCoT = (CoCoT)CoCoTdataGrid.SelectedItem;
            int selectedId = CoCoTdataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                if (MessageBox.Show("آیا میخواهید معلم دیگر این درس را تدریس نکند؟", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ///db.deleteStudentOFClassesList(db.GetStudentIdByUsername(username));
                    int teacherId = db.GetTeacherIdByUsername(usernameTextbox.Text);
                    db.deleteCoCoT(myCoCoT.courseid,myCoCoT.classid,teacherId);
                    UpdateDataGrid();
                }
            }
        }
    }
}

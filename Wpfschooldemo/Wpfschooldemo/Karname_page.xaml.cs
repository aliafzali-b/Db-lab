using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for Karname_page.xaml
    /// </summary>
    public partial class Karname_page : Page
    {
        DataAccess db = new DataAccess();
        public Karname_page()
        {
            InitializeComponent();
            usernameTextbox.Text = Globals.cache;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            changeName();
        }
        void Karname_page_Closed(object sender, EventArgs e)
        {
            Globals.cache = "";
        }

        private void usernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.karname_List.Clear();
            if (db.GetStudentIdByUsername(usernameTextbox.Text) > 0)
            {
                Students mystudent = db.SearchStudents("_username", usernameTextbox.Text)[0];
                Globals.cache = mystudent.username;
                int counter;
                List<Courses> CoursesList = db.GetCourses();
                List<Students> StudentsList = new List<Students>();
                foreach (Courses sample in CoursesList)
                {
                    char[] classesBinary = sample.classes.ToCharArray();
                    counter = 1;
                    foreach (char result in classesBinary)
                    {
                        if (result == '1' && counter ==mystudent.classid)
                        {
                            Karname mytaple = db.GetStudentsGradeIn_course_a_of_class_b(mystudent.id, sample.id, mystudent.classid);
                            mytaple.courseName = CoursesList[sample.id-1].name;
                            Globals.karname_List.Add(mytaple);
                        }
                        counter++;
                    }
                } 
            }
            UpdateDataGrid();

        }
        private void UpdateDataGrid()
        {
            karnameDataGrid.ItemsSource = null;
            karnameDataGrid.ItemsSource = Globals.karname_List;
            karnameDataGrid.IsReadOnly = true;
            changeName();
        }
        private void changeName()
        {
            try {
                karnameDataGrid.Columns[0].Header = " نام درس ";
                karnameDataGrid.Columns[1].Header = " نمره 1 ";
                karnameDataGrid.Columns[2].Header = " نمره 2 ";
                karnameDataGrid.Columns[3].Header = " نمره 3 ";
                karnameDataGrid.Columns[4].Header = " نمره 4 ";
                karnameDataGrid.Columns[5].Header = " نمره 5 ";
                karnameDataGrid.Columns[6].Header = " نمره 6 ";
                karnameDataGrid.Columns[7].Header = " نمره 7 ";
                karnameDataGrid.Columns[8].Header = " نمره 8 ";
                karnameDataGrid.Columns[9].Header = " نمره 9 ";
                karnameDataGrid.Columns[10].Header = " نمره 10 ";
                karnameDataGrid.Columns[11].Header = " معدل ";
            } catch { }
        }
    }
}

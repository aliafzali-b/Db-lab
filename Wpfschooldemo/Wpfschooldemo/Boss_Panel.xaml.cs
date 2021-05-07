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
using System.Media;

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for Boss_Panel.xaml
    /// </summary>
    public partial class Boss_Panel : Window
    {
        DataAccess db = new DataAccess();
        List<Students> StudentsList = new List<Students>();
        List<Classes> ClassesList = new List<Classes>();
        List<Courses> CoursesList = new List<Courses>();
        public Boss_Panel()
        {
            InitializeComponent();
            this.Closed += new EventHandler(Boss_Panel_Closed);

            /*db.createTeachersTable();
            db.createClassesTable();
            db.createCoursesTable();
            db.createStudentsTable();
            //----Ali Afzali
            //db.createExamTable();
            //db.createStuCourseTable();
            db.createExamTable2();*/
            Globals.LogedIn = true;
        }
        void Boss_Panel_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void teacherImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= Boss_Panel_Closed;
            this.Close();
            //var create_teacher_page = new create_teacher_page();
            var teachersPanel = new teachersPanel();
            teachersPanel.Show();
        }

        private void studentImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= Boss_Panel_Closed;
            this.Close();
            var studentsPanel = new studentsPanel();
            studentsPanel.Show();
        }

        private void courseImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= Boss_Panel_Closed;
            this.Close();
            var create_courses = new create_courses();
            create_courses.Show();
        }

        private void classImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= Boss_Panel_Closed;
            this.Close();
            var create_class = new create_class();
            create_class.Show();
        }
        private void settingImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= Boss_Panel_Closed;
            this.Close();
            var create_Boss_Window = new create_Boss_Window();
            create_Boss_Window.Show();
        }
        private void helpImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= Boss_Panel_Closed;
            this.Close();
            var userGuidePage = new userGuidePage();
            userGuidePage.Show();
        }

        private void updateStudents_Click(object sender, RoutedEventArgs e)
        {
            //xxxxxx// agar celas danesh Amooz ra avaz konid pak nemish is list kelasi ke ghablan dar an boode
            db.AddStudentsOFClassLists();
        }

        private void createClassLists_Click(object sender, RoutedEventArgs e)
        {
            /*int counter;
            CoursesList = db.GetCourses();
            foreach (Courses sample in CoursesList)
            {
                char[] classesBinary = sample.classes.ToCharArray();
                counter = 1;
                foreach (char result in classesBinary)
                {
                    if (result == '1')
                        db.createcourse_a_ofclass_b_Table(sample.id, counter);
                    else
                        db.dropcourse_a_ofclass_b_Table(sample.id, counter);
                    counter++;
                }
            }*/
        }

        private void deleteStudents_Click(object sender, RoutedEventArgs e)
        {
            /*int counter;
            CoursesList = db.GetCourses();
            foreach (Courses sample in CoursesList)
            {
                char[] classesBinary = sample.classes.ToCharArray();
                counter = 1;
                foreach (char result in classesBinary)
                {
                    if (result == '1')
                        db.dropcourse_a_ofclass_b_Table(sample.id, counter);
                    else
                        db.dropcourse_a_ofclass_b_Table(sample.id, counter);
                    counter++;
                }
            }*/
        }
        private void Image_MouseEnter(object sender, RoutedEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            DependencyObject child = VisualTreeHelper.GetChild(canvas, 0);
            Border border = child as Border;
            border.BorderThickness = new Thickness(2.5);
            hoverPlaySound();
        }
        private void Image_MouseLeave(object sender, RoutedEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            DependencyObject child = VisualTreeHelper.GetChild(canvas, 0);
            Border border = child as Border;
            border.BorderThickness = new Thickness(1);
        }

        private void hoverPlaySound()
        {
            var uri = new Uri(@"../../hover.mp3", UriKind.RelativeOrAbsolute);
            var player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }
    }
}

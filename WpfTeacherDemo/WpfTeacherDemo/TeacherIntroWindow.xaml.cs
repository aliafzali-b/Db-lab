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

namespace WpfTeacherDemo
{
    /// <summary>
    /// Interaction logic for TeacherIntroWindow.xaml
    /// </summary>
    public partial class TeacherIntroWindow : Window
    {
        DataAccess db = new DataAccess();
        public Color normalColor = new Color();
        public Color selectedColor = new Color();
        List<Classes> ClassesList = new List<Classes>();
        List<Courses> CoursesList = new List<Courses>();
        List<Students> StudentsList = new List<Students>();
        List<StudentsOfClass> StudentsOfClassList = new List<StudentsOfClass>();
        public TeacherIntroWindow()
        {
            InitializeComponent();
            this.Closed += new EventHandler(TeacherIntroWindow_Closed);
            CoursesList = db.GetCourses();
            ClassesList = db.GetClasses();
            normalColor = Color.FromArgb(0xFF, 0x42, 0x42, 0x42);
            selectedColor = Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E);

            changeClassButton.Content = CoursesList[Globals.myTeacherSelectedCourseId-1].name + " کلاس " + ClassesList[Globals.myTeacherSelectedClassId-1].name;
            
            changeBackgroundColor(seeAllViewButton);
            studentsPanelPage.Content = new Datagrid_Page();


            //exams = db.FindClassExams(Globals.myTeacherSelectedCourseId, Globals.myTeacherSelectedClassId);


            /*foreach (StudentsOfClass sample in StudentsOfClassList)
            {

                foreach (int result in sample.examID)
                {
                    sample.
                }
            }*/


        }
        void TeacherIntroWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void changeClassButton_Click(object sender, RoutedEventArgs e)
        {
            this.Closed -= TeacherIntroWindow_Closed;
            this.Close();
            var changeClassDialog = new changeClassDialog();
            changeClassDialog.Show();

        }
        private void examVsiewButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void changeBackgroundColor(Button sender)
        {
            changeClassButton.Background = new SolidColorBrush(normalColor);
            seeAllViewButton.Background = new SolidColorBrush(normalColor);
            //gradeViewButton.Background = new SolidColorBrush(normalColor);
            examViewButton.Background = new SolidColorBrush(normalColor);
            sender.Background = new SolidColorBrush(selectedColor);
        }

        private void seeAllViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            studentsPanelPage.Content = new Datagrid_Page();
        }

        private void examViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeBackgroundColor((Button)sender);
            studentsPanelPage.Content = new AddExam_Page();
        }
    }
}

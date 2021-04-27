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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTeacherDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccess db = new DataAccess();
        List<Teachers> TeachersList = new List<Teachers>();
        List<int> classNumber = new List<int>();
        List<int> courseNumber = new List<int>();
        char[] s = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '(' };
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += new EventHandler(MainWindow_Closed);
            Globals.myTeacherSelectedClassId = 1;
            Globals.myTeacherSelectedCourseId = 1;
        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextbox.Text;
            string password = passwordBox.Password;
            if (db.isTeacherPasswordCorrect(username, password))
            {
                db.initialize_myTeacher_By_Username(username);


                char[] tchar = Globals.myTeacher.takhasos.ToCharArray();
                int courseid;
                int counter;
                char[][] cchar = { Globals.myTeacher.classes1.ToCharArray(), Globals.myTeacher.classes2.ToCharArray(), Globals.myTeacher.classes3.ToCharArray() };
                for (int i = 0; i < 3; i++)
                {
                    courseid = find_Symbol_In_S_Array(tchar[i]) + 1; //convert tchar[i] to int
                    if (courseid > 0)
                    {
                        counter = 0;
                        foreach (char sample in cchar[i])
                        {
                            if (sample == '1')
                            {
                                classNumber.Add(counter + 1);
                                courseNumber.Add(courseid);
                                Globals.myTeacherSelectedClassId = classNumber[0];
                                Globals.myTeacherSelectedCourseId = courseNumber[0];
                            }
                            counter++;
                        }
                    }

                }





                this.Hide();
                var TeacherIntroWindow = new TeacherIntroWindow();
                TeacherIntroWindow.Show();
            }
            else
            {
                MessageBox.Show("کلمه عبور یا نام کاربری نادرست است", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExitImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private int find_Symbol_In_S_Array(char input)
        {
            int counter = 0;
            foreach (char sample in s)
            {
                if (sample == input)
                    return counter;
                counter++;
            }
            return -1;
        }
    }

}

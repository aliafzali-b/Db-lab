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


namespace WpfTeacherDemo
{
    /// <summary>
    /// Interaction logic for changeClassDialog.xaml
    /// </summary>
    public partial class changeClassDialog : Window
    {
        DataAccess db = new DataAccess();
        DataTable CocotForTeacherFrontEnd = new DataTable();
        DataTable CocotForTeacherBackEnd = new DataTable();
        List<Classes> ClassesList = new List<Classes>();
        List<Courses> CoursesList = new List<Courses>();
        char[] s = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '(' };
        List<int> classNumber = new List<int>();
        List<int> courseNumber = new List<int>();
        public changeClassDialog()
        {
            InitializeComponent();
            this.Closed += new EventHandler(changeClassDialog_Closed);
            ClassesList = db.GetClasses();
            CoursesList = db.GetCourses();
            db.initialize_myTeacher_By_Username(Globals.myTeacher.username);
            /*
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
                            classComboBox.Items.Add(CoursesList[courseid - 1].name + " کلاس " + ClassesList[counter].name);
                            classNumber.Add(counter+1);
                            courseNumber.Add(courseid);
                        }
                        counter++;
                    }
                }

            }
            */
            int teacherId = Globals.myTeacher.teacherId;
            CocotForTeacherFrontEnd = db.Get_Table("select Courses._Name as courseName,Class._Name as className from CoCoT,Class,Courses where TeacherID="+ teacherId+" and CoCoT.ClassID=Class.ClassID and Courses.CourseID=CoCoT.CourseID");
            CocotForTeacherBackEnd = db.Get_Table("select Courses.courseId as courseId,Class.classID as classId from CoCoT,Class,Courses where TeacherID="+ teacherId+" and CoCoT.ClassID=Class.ClassID and Courses.CourseID=CoCoT.CourseID");
           
            foreach (DataRow sample in CocotForTeacherFrontEnd.Rows)
            {
                classComboBox.Items.Add(sample["courseName"] +" از "+ sample["className"]);
            }


        }
        void changeClassDialog_Closed(object sender, EventArgs e)
        {
            int selectedItem = classComboBox.SelectedIndex;

            if (selectedItem != -1)
            {
                //MessageBox.Show(classNumber[selectedItem].ToString() + courseNumber[selectedItem].ToString());

                int courseid = CocotForTeacherBackEnd.Rows[selectedItem].Field<int>(0);
                int classid = CocotForTeacherBackEnd.Rows[selectedItem].Field<int>(1);
                string courseName = CocotForTeacherFrontEnd.Rows[selectedItem].Field<string>(0);
                string className = CocotForTeacherFrontEnd.Rows[selectedItem].Field<string>(1);
                Globals.myTeacherSelectedClassId = classid;
                Globals.myTeacherSelectedCourseId = courseid;
                Globals.myTeacherSelectedClassName = className;
                Globals.myTeacherSelectedCourseName = courseName;
                /**MessageBox.Show("class id " + classid);
                MessageBox.Show("course id " + courseid);*/
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var TeacherIntroWindow = new TeacherIntroWindow();
            TeacherIntroWindow.Show();
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

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
using System.Data;

namespace WpfTeacherDemo
{
    /// <summary>
    /// Interaction logic for Datagrid_Page.xaml
    /// </summary>
    public partial class Datagrid_Page : Page
    {
        DataAccess db = new DataAccess();
        DataTable grades = new DataTable();
        List<StudentsOfClass> StudentsOfClassList = new List<StudentsOfClass>();
        List<int> exam_ids = new List<int>();
        public Datagrid_Page()
        {
            InitializeComponent();
            List<StudentsOfClass> StudentsOfClassList = new List<StudentsOfClass>();
            //StudentsOfClassList = db.Get_List_Course_a_Class_b(Globals.myTeacherSelectedCourseId, Globals.myTeacherSelectedClassId);
            //classDataGrid.ItemsSource = StudentsOfClassList;
            DataTable myResult= db.Get_Table("select Exam.ExamID from Exam where CourseID = "+ Globals.myTeacherSelectedCourseId+" and ClassID = "+ Globals.myTeacherSelectedClassId+"");
            exam_ids = myResult.AsEnumerable()
                        .Select(r => r.Field<int>("examID"))
                        .ToList();
            int counter = 1;
            foreach(int sample in exam_ids)
            {
                examComboBox.Items.Add("امتحان " + counter);
                counter++;
            }
            try
            {

                grades = db.Get_Table("select Student.StuID,Student._Name,Student.LastName,Student.FatherName,Grade.Grade from Student,Grade where Student.StuID = Grade.StuID and Grade.ExamID = " + exam_ids[0] + " /*and Student.ClassID=0*/");
                classDataGrid.Columns.Clear();
                classDataGrid.ItemsSource = grades.DefaultView;
                examComboBox.SelectedIndex = 0;
                Globals.myTeacherSelectedExam_Id = exam_ids[0];
                changeName();
            }
            catch { }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            changeName();
        }
        private void sabtButton(object sender, RoutedEventArgs e)
        {
            /*StudentsOfClass myStudent = (StudentsOfClass)classDataGrid.SelectedItem;
            int selectedId = classDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                db.UpdateTaple(myStudent);
            }*/
            //StudentsOfClassList = db.Get_List_Course_a_Class_b(Globals.myTeacherSelectedCourseId, Globals.myTeacherSelectedClassId);

            //MessageBox.Show(StudentsOfClassList[0].g10); 
            int stuCount = db.Get_Students_count_of_class_x(Globals.myTeacherSelectedClassId);
            int index = 0;
            foreach (DataRowView row in classDataGrid.Items)
            {
                int stuid = Int32.Parse(row.Row.ItemArray[0].ToString());
                if (row.Row.ItemArray[4].ToString() != "")
                {
                    float grade = float.Parse(row.Row.ItemArray[4].ToString());

                    //MessageBox.Show(row.Row.ItemArray[4].ToString()+" " + stuid + " " + grade);

                    db.UpdateTable(stuid, grade);
                }
                index++;
                if (index >= stuCount)
                    break;
            }
            MessageBox.Show("تغییر یافت","Changed",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            /*for (int index = 0; index < stuCount; index++)
            {

                DataGridRow row = classDataGrid.Rows[1];
                float grade = classDataGrid.Rows[0].Field<float>(3);
                MessageBox.Show(" Value at 0,0" + classDataGrid.Rows[0].Cells[0].Value);

                StudentsOfClass myStudent = (StudentsOfClass)classDataGrid.Items[counter];
                db.UpdateTaple(myStudent);
            }
            foreach(DataGridRow sample in classDataGrid)
            {


            }*/

        }

        private void examComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedItem = examComboBox.SelectedIndex;
            if (selectedItem > -1)
            {
                Globals.myTeacherSelectedExam_Id = exam_ids[selectedItem];
                /*grades = db.Get_Table("select Student.StuID,Student._Name,Student.LastName,Student.FatherName,Grade.Grade from Student,Grade where Student.StuID = Grade.StuID and Grade.ExamID = " + exam_ids[selectedItem] + "");
                if (grades.Rows.Count == stuCount)
                {
                    classDataGrid.Columns.Clear();
                    classDataGrid.ItemsSource = grades.DefaultView;
                }
                else
                {*/
                /*MessageBox.Show("classid "+Globals.myTeacherSelectedClassId.ToString());
                MessageBox.Show("examid "+Globals.myTeacherSelectedExam_Id.ToString());*/
                grades = db.Get_Table("select z.StuID, z._Name, z.LastName, z.FatherName,cast(sum(z.grade) as decimal(10,2)) as grade from(select Student.StuID, Student._Name, Student.LastName, Student.FatherName, null as grade from Student where Student.ClassID = " + Globals.myTeacherSelectedClassId+" union select Student.StuID, Student._Name, Student.LastName, Student.FatherName, Grade.Grade from Student, Grade where Student.StuID = Grade.StuID and Grade.ExamID = "+ exam_ids[selectedItem] + ") as Z group by Z.StuID, z._Name, z.LastName, z.FatherName");
                    classDataGrid.Columns.Clear();
                    classDataGrid.ItemsSource = grades.DefaultView;
                changeName();
                //}
            }
        }
        private void changeName()
        {
            try
            {
                classDataGrid.Columns[0].Header = " Id ";
                classDataGrid.Columns[1].Header = " نام دانش آموز ";
                classDataGrid.Columns[2].Header = " نام خانوادگی ";
                classDataGrid.Columns[3].Header = " نام پدر ";
                classDataGrid.Columns[4].Header = " نمره ";
            }
            catch { }
        }
    }
}

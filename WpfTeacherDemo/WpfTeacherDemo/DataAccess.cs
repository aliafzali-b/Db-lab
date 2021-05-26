using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Windows;

namespace WpfTeacherDemo
{
    class DataAccess
    {
        public List<Classes> GetClasses()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Classes>($"select * from class").ToList();
                return output;
            }
        }
        public List<Courses> GetCourses()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Courses>($"select * from courses").ToList();
                return output;
            }
        }
        public List<Students> GetStudents(int classid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Students>($"SELECT students._id, students._username, students._password, students._name, students._fathername, students._phonenumber, students._major,classes._id as _classid, classes._name as _classname FROM students INNER JOIN classes ON students._classid = classes._id order by _id; ").ToList();
                if (classid > -1)
                     output = connection.Query<Students>($"SELECT _name from students where _classid={classid} order by _id; ").ToList();
                return output;
            }
        }
        public bool isTeacherPasswordCorrect(string username, string password)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Teachers>($"select * from Teacher where username = '{username}' and _password = '{password}'").ToList();
                int range = output.Count();
                if (range > 0)
                    return true;
                else
                    return false;

            }
        }
        public void initialize_myTeacher_By_Username(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Teachers>($"select * from teacher where username = '{username}'").ToList();
                try
                {
                    Globals.myTeacher = (Teachers)output[0];
                }
                catch{}
            }
        }
        public List<StudentsOfClass> Get_List_Course_a_Class_b(int Courseid , int Classid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<StudentsOfClass>($"SELECT * from Courses{Courseid}_Of_class{Classid}").ToList();
                return output;
            }
        }
        public int Get_Students_count_of_class_x(int classid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<int>($"SELECT count(*) from Student where classId={classid}").ToList();
                return output[0];
            }
        }
        public List<int> FindClassExams(int courseid , int classid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<int>($"select (_examid ) from stucourse where _classid={classid} and _courseid={courseid}").ToList();
                return output;
            }
        }
        public void UpdateTable(int stuid,float grade)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"UPDATE Grade SET grade={grade} WHERE stuid = '{stuid}' and ExamId={Globals.myTeacherSelectedExam_Id};");
                    //MessageBox.Show("با موفقیت تغیر یافت", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("db error"); }
                try
                {
                    connection.Execute($"insert into grade values({Globals.myTeacherSelectedExam_Id},{stuid},{grade});");
                }
                catch { MessageBox.Show("db error"); }
            }
        }
        public void insertIntoExames(string examdate, float zarib,string info,int status)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    var outputAll = connection.Query($"SELECT * FROM Exam").ToList();
                    int examId = outputAll.Count();
                    //MessageBox.Show($"insert into Exam values({examId}, {Globals.myTeacherSelectedCourseId},{Globals.myTeacherSelectedClassId},'{examdate}',{zarib},N'{info}',{status}");
                    
                    connection.Execute($"insert into Exam values({examId}, {Globals.myTeacherSelectedCourseId},{Globals.myTeacherSelectedClassId},'{examdate}',{zarib},N'{info}',{status})");

                }
                catch { MessageBox.Show("db error"); }
            }
        }
        public DataTable Get_Table(string query)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                DataTable dataTable = new DataTable();
                // create data adapter
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, Helper.CnnVal("dbschool"));
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                da.Dispose(); // i dont know what this is
                return dataTable;

                /* HOW TO Bind to DataGrid
                 * 
                    using System.Data;

                    DataTable myResult = db.Get_Table(string query)
                    myDataGrid.Columns.Clear();
                    myDataGrid.ItemsSource = myResult.DefaultView;
                */

                /* How To get One columned datatable to List<int>
                    List<int> students_ids = dataTable.AsEnumerable()
                        .Select(r => r.Field<int>("_id"))
                        .ToList();
                 */
            }

        }
    }
}

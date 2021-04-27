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
                var output = connection.Query<Classes>($"select * from classes").ToList();
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
                var output = connection.Query<Teachers>($"select * from Teachers where _username = '{username}' and _password = '{password}'").ToList();
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
                var output = connection.Query<Teachers>($"select * from teachers where _username = '{username}'").ToList();
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
                var output = connection.Query<StudentsOfClass>($"SELECT * from Course{Courseid}_Of_class{Classid}").ToList();
                return output;
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
        public void UpdateTaple(StudentsOfClass myStudent)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"UPDATE Course{Globals.myTeacherSelectedCourseId}_Of_class{Globals.myTeacherSelectedClassId} SET _g1={myStudent.g1},_g2={myStudent.g2},_g3={myStudent.g3},_g4={myStudent.g4},_g5={myStudent.g5},_g6={myStudent.g6},_g7={myStudent.g7},_g8={myStudent.g8},_g9={myStudent.g9},_g10={myStudent.g10} WHERE _stuid = '{myStudent.stuid}';");
                    //MessageBox.Show("با موفقیت تغیر یافت", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("db error"); }
            }
        }
        public void insertIntoExames(string name, string date,string time ,string info,float zarib)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    var outputAll = connection.Query($"SELECT * FROM Exams").ToList();
                    var outputClass = connection.Query($"SELECT * FROM Exams Where _classid='{Globals.myTeacherSelectedClassId}' and _courseid='{Globals.myTeacherSelectedCourseId}'").ToList();
                    int examId = outputAll.Count();
                    int examNumber = outputClass.Count();
                    //MessageBox.Show("its id will be " + (range + 1).ToString());
                    connection.Execute($"insert into Exams values({examId + 1}, {Globals.myTeacherSelectedClassId},{Globals.myTeacherSelectedCourseId},{examNumber + 1},N'{name}',N'{date}',N'{time}',0,N'{info}',{zarib})");

                }
                catch { }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Windows;


namespace Wpfschooldemo
{
    class DataAccess
    {
        public void createClassesTable()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table classes(_id int,_name nvarchar(255))");
                }
                catch
                { //MessageBox.Show("The classes table already existed");
                }
            }
        }
        public void createBossTable()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                connection.Execute($"create table Boss(_username varchar(255),_password varchar(255),_name nvarchar(255),_email varchar(255),_rememberme int)");
            }
        }
        public void createCoursesTable()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table courses(_id int,_name nvarchar(255),_classes nvarchar(255))");
                }
                catch
                {
                    //MessageBox.Show("The courses table already existed"); 
                }
            }
        }
        public void createTeachersTable()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table Teachers (_id int,_username nvarchar(255),_password nvarchar(255),_name nvarchar(255),_takhasos nvarchar(255),_classes1 nvarchar(255),_classes2 nvarchar(255),_classes3 nvarchar(255))");
                }
                catch
                {
                    //MessageBox.Show("The table already existed");
                }
            }
        }
        public void createStudentsTable()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table Students (_id int,_username nvarchar(255),_password nvarchar(255),_name nvarchar(255),_fatherName nvarchar(255),_phoneNumber bigint,_major nvarchar(255),_classid int)");
                }
                catch
                {
                    //MessageBox.Show("The table already existed");
                }
            }
        }
        public void createExamTable(int i)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table Exam_of_class{i} (_id int,_course int,_name nvarchar(255),_date nvarchar(255),_time nvarchar(255),_status bit,_about nvarchar(255),_examNumber int,_zarib int)");
                }
                catch { MessageBox.Show("The table already existed"); }
            }
        }
        public void createExamTable2()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table Exams (_id int,_classid int,_courseid int,_examNumber int,_name nvarchar(255),_date nvarchar(255),_time nvarchar(255),_status int,_info nvarchar(255),_zarib smallmoney)");
                }
                catch
                {
                    //MessageBox.Show("The table already existed");
                }
            }
        }
        public void createcourse_a_ofclass_b_Table(int a, int b)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table Course{a}_Of_class{b} (_stuid int,_name nvarchar(255),_fathername nvarchar(255),_g1 smallmoney DEFAULT -1 ,_g2 smallmoney DEFAULT -1,_g3 smallmoney DEFAULT -1,_g4 smallmoney DEFAULT -1,_g5 smallmoney DEFAULT -1,_g6 smallmoney DEFAULT -1,_g7 smallmoney DEFAULT -1,_g8 smallmoney DEFAULT -1,_g9 smallmoney DEFAULT -1,_g10 smallmoney DEFAULT -1 )");
                }
                catch { }
            }
        }

        public void initializeBoss(string username, string password, string name, string lastname, string address, string email,char gender)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    var output = connection.Query($"SELECT * FROM Manager").ToList();
                    int range = output.Count();
                    //MessageBox.Show("its id will be " + (range + 1).ToString());
                    if (range == 0)
                        connection.Execute($"insert into Manager values(0,'{username}','{password}',N'{name}',N'{lastname}',N'{address}','{email}','{gender}', 0)");
                    else
                        connection.Execute($"UPDATE Manager SET username ='{username}', password ='{password}', Name = N'{name}', Lastname = N'{lastname}', Address = N'{address}', Email = '{email}' , gender = '{gender}',_rememberme = 0");
                    MessageBox.Show("با موفقیت ذخیره شد", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("cant"); }
            }
        }
        public void rememberBoss(int input)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"UPDATE Manager SET rememberme =  {input}");
                    //MessageBox.Show(" شد", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("cant"); }
            }
        }
        public void insertIntoClasses(string name,string Year,int MajorID,int ChairNum,int BranchNum)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {

                var output = connection.Query($"SELECT * FROM class").ToList();
                int range = output.Count();
                // MessageBox.Show("its id will be " + (range + 1).ToString());
                connection.Execute($"insert into class values({range}, N'{name}',{MajorID},{BranchNum},N'{Year}',{ChairNum})");


            }
        }

        internal void removeClassByName(string className)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    
                    int deleteID = GetClassIdByName(className);
                    /*connection.Execute($"delete from CoCoT where ClassID={deleteID}");
                    connection.Execute($"delete from exam where ClassID={deleteID}");
                    connection.Execute($"delete from Student where ClassID={deleteID}");
                    connection.Execute($"delete from Class where ClassID={deleteID}");
                    connection.Execute($"UPDATE Class SET ClassID = ClassID-1 WHERE ClassID > {deleteID}");
                    connection.Execute($"UPDATE student SET student.stuid = student.new_stuid-1 FROM (SELECT stuid, ROW_NUMBER() OVER (ORDER BY [stuid]) AS new_stuid FROM student) student");
                    connection.Execute($"declare @refrenceTable table (oldExamId int,newExamId int)");
                    connection.Execute($"insert @refrenceTable select * from gradeNewExamId()");
                    connection.Execute($"UPDATE exam SET exam.ExamID = exam.new_examid-1 FROM (SELECT ExamID, ROW_NUMBER() OVER (ORDER BY [examid]) AS new_examid FROM Exam) exam");
                    connection.Execute($"UPDATE grade set grade.ExamID=ref.newExamId from grade,@refrenceTable as ref where grade.ExamID=ref.oldExamId");
                    */

                    connection.Execute($"exec deleteclass {deleteID}");


                    MessageBox.Show("حذف گردید", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("db error"); }
            }
        }

        public void insertIntoCourses(string name,int unit,int code)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {

                var output = connection.Query($"SELECT * FROM courses").ToList();
                int range = output.Count();
                //MessageBox.Show("its id will be " + (range + 1).ToString());
                connection.Execute($"insert into courses values({range}, N'{name}',{unit},{code})");



            }
        }
        public void insertIntoCoCoT(int courseid, int classid,int teacherid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"insert into cocot values({courseid}, {classid},{teacherid})");
                }
                catch {
                    MessageBox.Show("این درس را کس دیگری تدریس میکند", "Error", MessageBoxButton.OK,MessageBoxImage.Warning);
                    if (MessageBox.Show("آیا مایلید این معلم آن درس را تدریس کند", "Change Teacher", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            connection.Execute($"Update cocot set TeacherId={teacherid} where courseId={courseid} and classId={classid}");
                        }
                        catch
                        {
                            MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
        public void insertIntoTeachersTable(string username, string password, string name,string lastname,string expert,long phone,string email,char gender,int rememberme)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    var output = connection.Query($"SELECT * FROM teacher").ToList();
                    int range = output.Count();
                    //MessageBox.Show("its id will be " + (range + 1).ToString());
                    connection.Execute($"insert into Teacher values({range},'{username}','{password}',N'{name}',N'{lastname}',N'{expert}',{phone},'{email}','{gender}',{rememberme})");
                    //MessageBox.Show("اضافه گردید", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        public void insertIntoStudentsTable(string username, string password, string name, string lastname, string fathername, long phone, string address, string info, int classid,char gender)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    var output = connection.Query($"SELECT * FROM Student").ToList();
                    int range = output.Count();
                    //MessageBox.Show("its id will be " + (range + 1).ToString());
                    connection.Execute($"insert into Student values({range},'{username}','{password}',N'{name}',N'{lastname}',N'{fathername}',{phone},N'{address}',N'{info}',{classid},'{gender}')");
                    //MessageBox.Show("اضافه گردید", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("cant"); }
            }
        }


        public void insertIntoMajorTable(string name,int code)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    var output = connection.Query($"SELECT * FROM Major").ToList();
                    int range = output.Count();
                    MessageBox.Show("its id will be " + (range).ToString());
                    connection.Execute($"insert into Major values({range},N'{name}',{code})");
                    //connection.Execute($"insert into Major values(4,N'{تست4',5)")

                    MessageBox.Show("اضافه گردید", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("cant"); }
            }
        }
        public List<Classes> GetClasses()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Classes>($"select * from class").ToList();
                return output;
            }
        }


        public List<Major> GetMajor()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Major>($"select * from Major").ToList();
                return output;
            }
        }
        public List<Boss> GetBoss()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Boss>($"select * from Manager").ToList();
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
        public List<Teachers> GetTeachers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Teachers>($"select * from Teacher").ToList();
                return output;
            }
        }
        public List<Students> GetStudents()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Students>($"SELECT * from student").ToList();
                return output;
            }
        }
        public List<Teachers> SearchTeachers(string type, string value)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Teachers>($"select * from teacher where {type} like N'%{value}%'").ToList();
                return output;
            }
        }
        public List<Students> SearchStudents(string type, string value)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                    var output = connection.Query<Students>($"SELECT * from student Where {type} like N'%{value}%'").ToList();
                    return output;
                
            }
        }
        public List<Students> SearchStudentsByClass(string value)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Students>($"SELECT student.* from student,class Where student.classid=class.classid and class._name like N'%{value}%'").ToList();
                return output;

            }
        }
        public int GetClassIdByName(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Classes>($"select * from class where _name = N'{name}'").ToList();
                try
                {
                    Classes sample = (Classes)output[0];
                    return sample.classid;
                }
                catch
                {
                    return -1;
                }
            }
        }
        public int GetCourseIdByName(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Courses>($"select * from courses where _name = N'{name}'").ToList();
                try
                {
                    Courses sample = (Courses)output[0];
                    return sample.courseid;
                }
                catch
                {
                    return -1;
                }
            }
        }
        public int GetTeacherIdByUsername(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Teachers>($"select * from teacher where username = '{username}'").ToList();
                try
                {
                    Teachers sample = (Teachers)output[0];
                    return sample.teacherid;
                }
                catch
                {
                    return -1;
                }
            }
        }
        public int GetStudentIdByUsername(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Students>($"select * from Student where username = '{username}'").ToList();
                try
                {
                    Students sample = (Students)output[0];
                    return sample.stuid;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public void dropExamTable(int i)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"drop table Exam_of_class{i}");
                }
                catch { MessageBox.Show("The table was not existed"); }
            }
        }
        public void dropExamTable2()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"drop table Exams");
                }
                catch { MessageBox.Show("The table was not existed"); }
            }
        }
        public void dropcourse_a_ofclass_b_Table(int a, int b)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"drop table Course{a}_Of_class{b}");
                }
                catch { }
            }
        }

        public void changeClassNameByName(string oldname, string newname,int Chairnum,int BranchNum,string Year,int MajorID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"UPDATE class SET _Name = N'{newname}',MajorID = {MajorID} , BranchNumber={BranchNum},_Year='{Year}',ChairNum = {Chairnum} WHERE _Name= N'{oldname}'; ");
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        public void updateCourseNameByName(string oldname, string newname,int code,int unit)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"UPDATE courses SET _name = N'{newname}', Unit = {unit},Ccode={code} WHERE _name= N'{oldname}'; ");
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        public void updateTeacherByUsername(string oldUsername, string newUsername, string password, string name, string lastname, string expert, long phone, string email, char gender)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"UPDATE teacher SET username = '{newUsername}', _password = '{password}', _name = N'{name}',lastname =N'{lastname}',expert= N'{expert}',phone = {phone},email='{email}',gender='{gender}' WHERE username = '{oldUsername}';");
                    MessageBox.Show("با موفقیت تغیر یافت", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        public void updateStudentByUsername(string oldUsername, string newUsername, string password, string name, string lastname, string fathername, long phone, string address, string info, int classid, char gender)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    //if (!string.IsNullOrEmpty(major))
                        connection.Execute($"UPDATE Student SET username = '{newUsername}', _password = '{password}', _name = N'{name}', lastname = N'{lastname}', fathername = N'{fathername}' , phone = {phone}, _address = N'{address}',info = N'{info}', classid= {classid},gender ='{gender}' WHERE username = '{oldUsername}';");
                    //else
                        //connection.Execute($"UPDATE Students SET _username = '{newUsername}', _password = '{password}', _name = N'{name}', _fathername = N'{fathername}' , _phoneNumber = {phone}, _major = NULL , _classid= {classid} WHERE _username = '{oldUsername}';");
                    MessageBox.Show("با موفقیت تغیر یافت", "successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        public bool isThereAnyBoss()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Boss>($"select * from manager").ToList();
                int range = output.Count();
                if (range > 0)
                    return true;
                else
                    return false;

            }
        }
        public bool isBossUsernamePasswordCorrect(string username, string password)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Boss>($"select * from manager where username = '{username}' and _password = '{password}'").ToList();
                int range = output.Count();
                if (range > 0)
                    return true;
                else
                    return false;

            }
        }
        public bool isCourseNameValid(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Courses>($"select * from courses where _name = N'{name}'").ToList();
                int range = output.Count();
                if (range == 0 && !string.IsNullOrWhiteSpace(name))
                    return true;
                else
                    return false;

            }
        }
        public bool isClassNameValid(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Classes>($"select * from class where _name = N'{name}'").ToList();
                int range = output.Count();
                if (range == 0 && !string.IsNullOrWhiteSpace(name))
                    return true;
                else
                    return false;

            }
        }
        public bool isTeacherNameValid(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Classes>($"select * from teacher where username = '{username}'").ToList();
                int range = output.Count();
                if (range == 0 && !string.IsNullOrWhiteSpace(username))
                    return true;
                else
                    return false;
            }
        }
        public bool isStudentNameValid(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Students>($"select * from Student where username = '{username}'").ToList();
                int range = output.Count();
                if (range == 0 && !string.IsNullOrWhiteSpace(username))
                    return true;
                else
                    return false;
            }
        }

        public bool isMajorNameValid(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Major>($"select * from Major where _Name = N'{name}'").ToList();
                int range = output.Count();
                if (range == 0 && !string.IsNullOrWhiteSpace(name))
                    return true;
                else
                    return false;

            }
        }

        public void deleteTeacherByUserName(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    /*db-lab var output = connection.Query($"SELECT * FROM teachers").ToList();
                    int range = output.Count();
                    int deleteID = GetTeacherIdByUsername(username);
                    //MessageBox.Show("its id will be " + (range + 1).ToString());
                    connection.Execute($"delete from Teachers where _username = '{username}'");
                    for (int i = deleteID; i < range; i++)
                    {
                        connection.Execute($"UPDATE teachers SET _id = {i} WHERE _id = {i + 1};");
                    }*/
                    int deleteID = GetTeacherIdByUsername(username);
                    connection.Execute($"delete from cocot where teacherid = {deleteID}");
                    connection.Execute($"DELETE FROM Teacher WHERE teacherid = {deleteID}");
                    connection.Execute($"UPDATE Teacher SET teacherid = teacherid-1 WHERE teacherid > {deleteID} ");
                    MessageBox.Show("حذف گردید", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        public void deleteCoCoT(int courseid,int classid , int teacherid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"Delete from CoCoT WHERE courseid = {courseid} and classid= {classid} and teacherid={teacherid} ");
                    MessageBox.Show("حذف گردید", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        public List<CoCoT> GetTeacherCoCotById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    /*db-lab var output = connection.Query($"SELECT * FROM teachers").ToList();
                    int range = output.Count();
                    int deleteID = GetTeacherIdByUsername(username);
                    //MessageBox.Show("its id will be " + (range + 1).ToString());
                    connection.Execute($"delete from Teachers where _username = '{username}'");
                    for (int i = deleteID; i < range; i++)
                    {
                        connection.Execute($"UPDATE teachers SET _id = {i} WHERE _id = {i + 1};");
                    }*/
                    var output = connection.Query<CoCoT>($"SELECT * FROM CoCoT where TeacherID = {id}").ToList();
                    return output;
                    //MessageBox.Show("حذف گردید", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error);}
                return null;
            }
        }
        public void deleteStudentByUserName(string username)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    int deleteID = GetStudentIdByUsername(username);
                    //MessageBox.Show("its id is " + deleteID.ToString());
                    //connection.Execute($"delete from Students where _username = '{username}'");
                    connection.Execute($"delete from grade where stuid = {deleteID}");
                    connection.Execute($"DELETE FROM Student WHERE Stuid = {deleteID}");
                    connection.Execute($"UPDATE student SET stuid=stuid-1 WHERE Stuid>{deleteID} ");
                    MessageBox.Show("حذف گردید", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch { MessageBox.Show("عملیات با شکست رو به رو شد", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        public void createStuCourseTable()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table Stucourse (_id int,_classid int,_courseid int,_stuid int,_year int,_examid int,_grade smallmoney)");
                }
                catch
                {
                    //MessageBox.Show("Erroroororor");
                }
            }
        }
        public void createExamTable()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute($"create table Exam (_examid int,_examNumber int,_name nvarchar(255),_date date,_info nvarchar(255),_status int,_zarib smallmoney)");
                }
                catch
                {
                    //MessageBox.Show("Erroroororor");
                }
            }
        }

        public bool should_We_Add_This_Student_To_Class(int stuid, int classid, int courseid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Classes>($"select * from Course{courseid}_Of_class{classid} where _stuid = {stuid}").ToList();
                int range = output.Count();
                if (range == 0)
                    return true;
                else
                    return false;

            }
        }
        public List<Students> GetStudentsByClassId(int classid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                var output = connection.Query<Students>($"SELECT students._id, students._username, students._password, students._name, students._fathername, students._phonenumber, students._major,classes._id as _classid, classes._name as _classname FROM students INNER JOIN classes ON students._classid = classes._id where students._classid={classid} order by _id; ").ToList();
                return output;
            }
        }
        public void AddStudentsOFClassLists()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {

                /*int counter;
                List<Courses> CoursesList = GetCourses();
                List<Students> StudentsList = new List<Students>();
                foreach (Courses sample in CoursesList)
                {
                    char[] classesBinary = sample.classes.ToCharArray();
                    counter = 1;
                    foreach (char result in classesBinary)
                    {
                        if (result == '1')
                        {
                            StudentsList = GetStudentsByClassId(counter);
                            foreach (Students mystudent in StudentsList)
                            {
                                if (should_We_Add_This_Student_To_Class(mystudent.stuid, counter, sample.id))
                                {
                                    connection.Execute($"INSERT INTO Course{sample.id}_Of_class{counter} (_stuid, _name, _fathername) values({mystudent.stuid},N'{mystudent._name}',N'{mystudent.fathername}')");
                                }
                            }
                        }
                        //createcourse_a_ofclass_b_Table(sample.id, counter);
                        counter++;
                    }
                }

                */
            }
        }

        public void deleteStudentOFClassesList(int stuid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {

               /* int counter;
                List<Courses> CoursesList = GetCourses();
                foreach (Courses sample in CoursesList)
                {
                    char[] classesBinary = sample.classes.ToCharArray();
                    counter = 1;
                    foreach (char result in classesBinary)
                    {
                        if (result == '1')
                        {
                            //StudentsList = GetStudentsByClassId(counter);

                            connection.Execute($"delete from Course{sample.id}_Of_class{counter} where _stuid={stuid}");
                        }
                        //createcourse_a_ofclass_b_Table(sample.id, counter);
                        counter++;
                    }
                }
               */
            }
        }

        public void Update_Student_Values_In_Classes_List(int stuid)
        {
            //this method will change your student properties in class lists
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
               /* int counter;
                List<Courses> CoursesList = GetCourses();
                List<Students> StudentsList = GetStudents();
                foreach (Courses sample in CoursesList)
                {
                    char[] classesBinary = sample.classes.ToCharArray();
                    counter = 1;
                    foreach (char result in classesBinary)
                    {
                        if (result == '1')
                        {
                            connection.Execute($"UPDATE Course{sample.id}_Of_class{counter} SET _name = N'{StudentsList[stuid - 1]._name}', _fathername = N'{StudentsList[stuid - 1].fathername}' WHERE _stuid = '{stuid}';");
                        }
                        //createcourse_a_ofclass_b_Table(sample.id, counter);
                        counter++;
                    }
                }
               */

            }
        }

        public void Add_One_Student_Values_In_Classes_List(int stuid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                /*List<Courses> CoursesList = GetCourses();
                List<Students> StudentsList = GetStudents();
                int classid = StudentsList[stuid - 1].classid;
                foreach (Courses sample in CoursesList)
                {
                    char[] classesBinary = sample.classes.ToCharArray();
                    if (classesBinary[classid-1] == '1' && should_We_Add_This_Student_To_Class(stuid, classid, sample.id))
                        connection.Execute($"INSERT INTO Course{sample.id}_Of_class{classid} (_stuid, _name, _fathername) values({stuid},N'{StudentsList[stuid - 1]._name}',N'{StudentsList[stuid - 1].fathername}')");
                }
                */
            }

        }
        public Karname GetStudentsGradeIn_course_a_of_class_b(int stuid, int courseid, int classid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                //MessageBox.Show(stuid.ToString() + " : " + courseid.ToString() + " : "+classid.ToString());
                var output = connection.Query<Karname>($"SELECT _name as _courseName, _g1, _g2, _g3,_g4,_g5,_g6,_g7,_g8,_g9,_g10 FROM Course{courseid}_Of_class{classid} Where _stuid = '{stuid}'").ToList();
                return output[0];
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

                    DataTable myResult = db.Get_Table(string query);
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

        public int Create_All_Tables()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute(
                        $"CREATE TABLE Major (MajorID int NOT NULL,_Name nvarchar(255) NOT NULL,MCode int NULL,PRIMARY KEY(MajorID));" +
                        $"CREATE TABLE Courses(CourseID int NOT NULL,_Name nvarchar(255) NOT NULL,Unit int NOT NULL,CCode int NULL,PRIMARY KEY(CourseID));" +
                        $"CREATE TABLE Class(ClassID int NOT NULL,_Name nvarchar(255) NOT NULL,MajorID int NULL,BranchNumber int NULL,_Year nvarchar(255) NULL,ChairNum int NULL,PRIMARY KEY(ClassID),FOREIGN KEY(MajorID) REFERENCES Major(MajorID) on update cascade);" +
                        $"CREATE TABLE Student(StuID int NOT NULL,Username varchar(255) NOT NULL UNIQUE,_Password varchar(255) NOT NULL,_Name nvarchar(255) NOT NULL,LastName nvarchar(255) NOT NULL,FatherName nvarchar(255) NOT NULL,Phone bigint NOT NULL,_Address nvarchar(255) NULL,Info nvarchar(255) NULL,ClassID int NULL,Gender char(1) NULL,PRIMARY KEY CLUSTERED(StuID),CONSTRAINT CheckStudent check(Gender = 'M' OR Gender = 'F'),FOREIGN KEY(ClassID) REFERENCES Class(ClassID) on update cascade);" +
                        $"CREATE TABLE Exam(ExamID int NOT NULL,CourseID int NOT NULL,ClassID int NOT NULL,_Date datetime NOT NULL,Ratio float(10) NOT NULL,Info nvarchar(255) NULL,Status char(1) NULL,PRIMARY KEY(ExamID),FOREIGN KEY(ClassID) REFERENCES Class(ClassID) on update cascade,FOREIGN KEY(CourseID) REFERENCES Courses(CourseID) on update cascade );" +
                        $"CREATE TABLE Grade(ExamID int NOT NULL,StuID int NOT NULL,Grade smallmoney NULL,FOREIGN KEY(ExamID) REFERENCES Exam(ExamID) on Delete cascade,FOREIGN KEY(StuID) REFERENCES Student(StuID) on update cascade);" +
                        $"CREATE TABLE Teacher(TeacherID int NOT NULL,UserName varchar(255) NOT NULL UNIQUE,_Password varchar(255) NOT NULL,_Name nvarchar(255) NOT NULL,Lastname nvarchar(255) NOT NULL,Expert nvarchar(255) NULL,Phone bigint NOT NULL,Email varchar(255) NOT NULL,Gender char(1) NULL,RememberMe int NOT NULL,PRIMARY KEY(TeacherID),CONSTRAINT CheckTeacher check(Gender = 'M' OR Gender = 'F'));" +
                        $"CREATE TABLE Manager(ManagerID int NOT NULL,UserName varchar(255) NOT NULL UNIQUE,_Password varchar(255) NOT NULL,_Name nvarchar(255) NOT NULL,Lastname nvarchar(255) NOT NULL,_Address nvarchar(255) NULL,Email varchar(255) NOT NULL,Gender char(1) NULL,RememberMe int NOT NULL,PRIMARY KEY(ManagerID),CONSTRAINT CheckManeger check(Gender = 'M' OR Gender = 'F'));" +
                        $"CREATE TABLE CoCoT(CourseID int NOT NULL,ClassID int NOT NULL,TeacherID int NOT NULL,FOREIGN KEY(CourseID) REFERENCES Courses(CourseID) on update cascade,FOREIGN KEY(ClassID) REFERENCES Class(ClassID) on update cascade,FOREIGN KEY(TeacherID) REFERENCES Teacher(TeacherID) on update cascade,CONSTRAINT PK_Cocot PRIMARY KEY (CourseID,ClassID));");
                    //MessageBox.Show("Success");
                }
                catch
                {
                    //MessageBox.Show("Tables allready exists");
                    return 0;
                }
            }
            return 1;
        }
        public int Create_All_Functions()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("dbschool")))
            {
                try
                {
                    connection.Execute(
                        $"go" +
                        $"create function nearstexams (@Username varchar(255))" +
                        $"returns @rettable table (_Name nvarchar(255),_Date datetime)" +
                        $"as" +
                        $"begin" +
                        $"declare @classid int;select @classid = ClassID from student where Username=@Username;declare @nearexam table (CourseID int,_Date datetime);insert @nearexam select CourseID,_Date from Exam where ClassID=@classid and (datediff(MINUTE,getdate(),_Date)>0);insert @rettable select Courses._Name,Exames._Date from Courses,@nearexam as Exames where Courses.CourseID=Exames.CourseID" +
                        $"return" +
                        $"end");/* +
                        $"go" +
                        $"create function xyzCommonTeachers (@teacher_Username1 varchar(255),@teacher_Username2 varchar(255),@teacher_Username3 varchar(255))" +
                        $"returns @rettable table (username varchar(255),_name nvarchar(255),lastname nvarchar(255),Classname nvarchar(255))" +
                        $"as" +
                        $"begin" +
                        $"declare @id1 int;declare @id2 int;declare @id3 int;select @id1=TeacherID from Teacher where UserName=@teacher_Username1;select @id2=TeacherID from Teacher where UserName=@teacher_Username2;select @id3=TeacherID from Teacher where UserName=@teacher_Username3;declare @classes1 table (ClassId int);declare @classes2 table (ClassId int);declare @classes3 table (ClassId int);insert @classes1 select ClassId from CoCoT where TeacherID=@id1;insert @classes2 select ClassId from CoCoT where TeacherID=@id2;insert @classes3 select ClassId from CoCoT where TeacherID=@id3;declare @commonClasses table (ClassId int);insert @commonClasses select * from @classes1 intersect select * from @classes2 intersect select * from @classes3;insert @rettable select Student.Username,Student._Name,Student.LastName,Class._Name from Student,Class,@commonClasses as commonClasses where commonClasses.ClassId=Student.ClassID and Student.ClassID=Class.ClassID" +
                        $"return" +
                        $"end"+
                        $"go" +
                        $"create function BestTeachers ()" +
                        $"returns @rettable table (teacherId int,_name nvarchar(255),LastName nvarchar(255),score float)" +
                        $"as" +
                        $"begin" +
                        $"declare @myTB table (TeacherID int,_name nvarchar(255),LastName nvarchar(255),CourseID int,ClassID int,StuID int,Grade float,Ratio float,ExamID int);insert @myTB select Teacher.TeacherID,Teacher._Name,Teacher.Lastname,CoCoT.CourseID,CoCoT.ClassID,Grade.StuID,Grade.Grade,Exam.Ratio,Exam.ExamID from Teacher,Exam,CoCoT,Grade where CoCoT.TeacherID=Teacher.TeacherID and Exam.ClassID=CoCoT.ClassID and Exam.CourseID=CoCoT.CourseID and Exam.ExamID=Grade.ExamID;insert @rettable select TeacherID,_Name,LastName,cast(sum(Grade*ratio)/sum(ratio) as decimal(10,3))from @myTB group by TeacherId,_Name,LastName;" +
                        $"return" +
                        $"end"+
                        $"go" +
                        $"create function karname (@Username varchar(255))" +
                        $"returns @rettable table (courseName nvarchar(255),Grade float)" +
                        $"as" +
                        $"begin" +
                        $"declare @classid int;select @classid = ClassID from student where Username=@Username;declare @stuid int;select @stuid = stuid from student where Username=@Username;declare @emtyTable table (CourseName nvarchar(255),Grade float);insert @emtyTable select Courses._Name,null as Grade  from  CoCoT,Courses where CoCoT.ClassID=@classid;declare @fullTable table (CourseName nvarchar(255),Grade float);insert @fullTable select Courses._Name,sum(Grade.Grade*Exam.ratio)/sum(Exam.ratio) as Grade  from  Grade,Exam,Courses where Grade.StuID=@stuid and Exam.ClassID=@classid and Exam.CourseID=Courses.CourseID and Grade.ExamID=Exam.ExamID Group By Courses._Name;insert @rettable select Z.CourseName,cast(sum(Z.grade) as decimal(10,2)) from (select * from @emtyTable union select * from @fullTable) as Z group by Z.CourseName;" +
                        $"return" +
                        $"end");*/
                    MessageBox.Show("Success");
                }
                catch
                {
                    MessageBox.Show("Tables allready exists");
                    return 0;
                }
            }
            return 1;
        }
    }
}

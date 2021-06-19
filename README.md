# Db-lab
سلام
برنامه را کلون کنید
1) fix app.config
    این فایل داخل هر دو پوشه هست و باید اسم اس کیو ال سرور خودتون رو داخل این فایل بنویسید
2) create "School" database in your Databases
    دیتابیسی با این نام داخل سرور خود ایجاد کنید
3)  توابع را به صورت دستی داخل دیتا بیس اضافه کنید

کد های توابع

/*  fuction 1 */
create function nearstexams (@Username varchar(255))
returns @rettable table (_Name nvarchar(255),_Date datetime)
as
begin
	declare @classid int;select @classid = ClassID from student where Username=@Username;declare @nearexam table (CourseID int,_Date datetime);insert @nearexam select CourseID,_Date from Exam where ClassID=@classid and (datediff(MINUTE,getdate(),_Date)>0);insert @rettable select Courses._Name,Exames._Date from Courses,@nearexam as Exames where Courses.CourseID=Exames.CourseID
	return
end

/*  fuction 2 */
go
create function xyzCommonTeachers (@teacher_Username1 varchar(255),@teacher_Username2 varchar(255),@teacher_Username3 varchar(255))
returns @rettable table (username varchar(255),_name nvarchar(255),lastname nvarchar(255),Classname nvarchar(255))
as
begin
	declare @id1 int;declare @id2 int;declare @id3 int;select @id1=TeacherID from Teacher where UserName=@teacher_Username1;select @id2=TeacherID from Teacher where UserName=@teacher_Username2;select @id3=TeacherID from Teacher where UserName=@teacher_Username3;declare @classes1 table (ClassId int);declare @classes2 table (ClassId int);declare @classes3 table (ClassId int);insert @classes1 select ClassId from CoCoT where TeacherID=@id1;insert @classes2 select ClassId from CoCoT where TeacherID=@id2;insert @classes3 select ClassId from CoCoT where TeacherID=@id3;declare @commonClasses table (ClassId int);insert @commonClasses select * from @classes1 intersect select * from @classes2 intersect select * from @classes3;insert @rettable select Student.Username,Student._Name,Student.LastName,Class._Name from Student,Class,@commonClasses as commonClasses where commonClasses.ClassId=Student.ClassID and Student.ClassID=Class.ClassID
	return
end
go
/*  fuction 3 */
create function BestTeachers ()
returns @rettable table (teacherId int,_name nvarchar(255),LastName nvarchar(255),score float)
as
begin
	declare @myTB table (TeacherID int,_name nvarchar(255),LastName nvarchar(255),CourseID int,ClassID int,StuID int,Grade float,Ratio float,ExamID int);insert @myTB select Teacher.TeacherID,Teacher._Name,Teacher.Lastname,CoCoT.CourseID,CoCoT.ClassID,Grade.StuID,Grade.Grade,Exam.Ratio,Exam.ExamID from Teacher,Exam,CoCoT,Grade where CoCoT.TeacherID=Teacher.TeacherID and Exam.ClassID=CoCoT.ClassID and Exam.CourseID=CoCoT.CourseID and Exam.ExamID=Grade.ExamID;insert @rettable select TeacherID,_Name,LastName,cast(sum(Grade*ratio)/sum(ratio) as decimal(10,3))from @myTB group by TeacherId,_Name,LastName;
	return
end
/*  another function */
go
create function karname (@Username varchar(255))
returns @rettable table (courseName nvarchar(255),Grade float)
as
begin
	declare @classid int;select @classid = ClassID from student where Username=@Username;declare @stuid int;select @stuid = stuid from student where Username=@Username;declare @emtyTable table (CourseName nvarchar(255),Grade float);insert @emtyTable select Courses._Name,null as Grade  from  CoCoT,Courses where CoCoT.ClassID=@classid;declare @fullTable table (CourseName nvarchar(255),Grade float);insert @fullTable select Courses._Name,sum(Grade.Grade*Exam.ratio)/sum(Exam.ratio) as Grade  from  Grade,Exam,Courses where Grade.StuID=@stuid and Exam.ClassID=@classid and Exam.CourseID=Courses.CourseID and Grade.ExamID=Exam.ExamID Group By Courses._Name;insert @rettable select Z.CourseName,cast(sum(Z.grade) as decimal(10,2)) from (select * from @emtyTable union select * from @fullTable) as Z group by Z.CourseName;
	return
end

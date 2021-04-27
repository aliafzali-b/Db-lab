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

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for create_courses.xaml
    /// </summary>
    public partial class create_courses : Window
    {
        List<Classes> classesList = new List<Classes>();
        List<Courses> coursesList = new List<Courses>();
        List<Teachers> teachersList = new List<Teachers>();
        bool have_We_any_Change;
        List<CheckBox> checkbx = null;
        DataAccess db = new DataAccess();
        char[] s = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '(' };
        public create_courses()
        {
            InitializeComponent();
            db.createCoursesTable();
            this.Closed += new EventHandler(create_courses_Closed);
           
            textBox1.Text = "for excute onchange text function";
            classesList = db.GetClasses();
            coursesList = db.GetCourses();
            checkbx = new List<CheckBox>() { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16, checkBox17, checkBox18, checkBox19, checkBox20, checkBox21};
            foreach (CheckBox sample in checkbx)
            {
                sample.Visibility = Visibility.Hidden;
            }
            foreach (Classes sample in classesList)
            {
                checkbx[sample.id - 1].Content = sample.name; // because id start's with 1 but checkbx's andise start's from 0
                show_checkBoxes(true);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            have_We_any_Change = false;
            updateDataGrid();
            try
            {
                dataGrid1.Columns.RemoveAt(2);
            }
            catch { }
        }

        void create_courses_Closed(object sender, EventArgs e)
        {
            if (have_We_any_Change == true)
            {
                updateListOfClasses();
            }
            waitImage.Visibility = Visibility.Hidden;
            this.Closed -= create_courses_Closed;
            this.Close();
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        }

        private void updateDataGrid()
        {
            coursesList = db.GetCourses();
            dataGrid1.ItemsSource = coursesList;
            dataGrid1.IsReadOnly = true;
            try
            {
                dataGrid1.Columns.RemoveAt(2);
            }
            catch { }
            textBox1.Text = "";//chon ba ezafe shodan item haye listbox in textbox meghdar migereft
            uncheckAll();
        }

        private void uncheckAll()
        {
            dataGrid1.Columns[0].Header = " Id ";
            dataGrid1.Columns[1].Header = " نام ";
            foreach (CheckBox sample in checkbx)
            {
                if (sample.Visibility == Visibility.Visible)
                    sample.IsChecked = false;
                //else
                    //break;
            }
        }
        private void show_checkBoxes(bool visibility)
        {
            if (visibility == true)
            {
                uncheckAllImage.Visibility = Visibility.Visible;
                checkAllImage.Visibility = Visibility.Visible;
                foreach (Classes sample in classesList)
                {
                    checkbx[sample.id - 1].Visibility = Visibility.Visible; // because id start's with 1 but checkbx's andise start's from 0
                }
            }
            if (visibility == false)
            {
                uncheckAllImage.Visibility = Visibility.Hidden;
                checkAllImage.Visibility = Visibility.Hidden;
                foreach (Classes sample in classesList)
                {
                    checkbx[sample.id - 1].Visibility = Visibility.Hidden; // because id start's with 1 but checkbx's andise start's from 0
                }
            }
        }
        private void checkAll()
        {
            foreach (CheckBox sample in checkbx)
            {
                if (sample.Visibility == Visibility.Visible)
                    sample.IsChecked = true;
                //else
                //break;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            coursesList = db.GetCourses();
            bool isvalid = db.isCourseNameValid(textBox1.Text);
            if (isvalid == true)
            {
                show_checkBoxes(true);
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Visible;
                warningImage.Visibility = Visibility.Hidden;
                lable3.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Visible;
                changeButton.Visibility = Visibility.Hidden;
                textBox2.Visibility = Visibility.Hidden;
                textBox2.Text = "";
            }
            else
            {
                errorImage.Visibility = Visibility.Visible;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Hidden;
                int courseid = db.GetCourseIdByName(textBox1.Text);
                if (courseid > 0)
                {
                    show_checkBoxes(true);
                    changeButton.Visibility = Visibility.Visible;
                    warningImage.Visibility = Visibility.Visible;
                    lable3.Visibility = Visibility.Visible;
                    errorImage.Visibility = Visibility.Hidden;
                    textBox2.Visibility = Visibility.Visible;
                    textBox2.Text = textBox1.Text;
                    //tik zadan checkbox ha
                    char[] classesOfcourse = coursesList[courseid - 1].classes.ToCharArray();
                    int n = 0;
                    foreach (char sample in classesOfcourse)
                    {
                        if (sample == '1')
                            checkbx[n].IsChecked = true;
                        else
                            checkbx[n].IsChecked = false;
                        n++;
                    }


                }
            }
            if (textBox1.Text == "")
            {
               errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Hidden;
                changeButton.Visibility = Visibility.Hidden;
                textBox2.Visibility = Visibility.Hidden;
                lable3.Visibility = Visibility.Hidden;
                textBox2.Text = "";
                show_checkBoxes(false);
                uncheckAll();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string cla = "";
            string no = "";
            foreach (CheckBox sample in checkbx)
            {
                no += "0";
                if (sample.IsChecked == true)
                    cla += "1";
                else
                    cla += "0";
            }
            //MessageBox.Show(cla);
            if (cla != no)
            {
                DataAccess db = new DataAccess();
                db.insertIntoCourses(textBox1.Text, cla);
                uncheckAll();
                updateDataGrid();
            }
            else
            {
                MessageBox.Show("لطفا حداقل یک کلاس را انتخاب کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            have_We_any_Change = true;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Courses mycourse = (Courses)dataGrid1.SelectedItem;
            if (mycourse != null)
            {
                int selectedId = (mycourse.id)-1;
                if (selectedId >= 0)
                    textBox1.Text = coursesList[selectedId].name;
            } 
        }
        private void checkAllButton_Click(object sender, RoutedEventArgs e)
        {
            checkAll();
        }
        private void uncheckAllButton_Click(object sender, RoutedEventArgs e)
        {
            uncheckAll();
        }
        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            string oldname = textBox1.Text;
            string newname = textBox2.Text;
            string cla = "";
            string no = "";
            if (db.isCourseNameValid(newname) || oldname==newname) 
            { 
                //change checkboxs
                foreach (CheckBox sample in checkbx)
                {
                    no += "0";
                    if (sample.IsChecked == true)
                        cla += "1";
                    else
                        cla += "0";
                }
                //MessageBox.Show(cla);
                if (cla != no)
                {
                    db.updateCourseNameByName(oldname, newname , cla);

                    /////////////---------------------------------------------------------------------------------------
                    ///
                    string takhasos ;
                    char[] newRules = cla.ToCharArray();
                    int courseid_int = db.GetCourseIdByName(newname);
                    int courseid_InTakhasos;
                    int i = 0;
                    
                    teachersList = db.GetTeachers();
                    foreach (Teachers myteacher in teachersList)
                    {
                        takhasos = myteacher.takhasos;
                        char[] tchar = takhasos.ToCharArray();
                        char[][] cchar = { myteacher.classes1.ToCharArray(), myteacher.classes2.ToCharArray(), myteacher.classes3.ToCharArray() };
                        i= 0;
                       
                        foreach (char sample in tchar)
                        {
                            courseid_InTakhasos = find_Symbol_In_S_Array(tchar[i]) + 1;
                            if (courseid_InTakhasos== courseid_int)
                            {
                                //// classes i must be cla
                                ///
                                
                                int rullnumber = 0;
                                foreach(char rule in newRules)
                                {
                                    if (rule=='0' && cchar[i][rullnumber] == '1')
                                    {
                                        cchar[i][rullnumber] = '0';
                                        MessageBox.Show("یکی از کلاس هایی که " + myteacher.name + " تدریس میکند دچار تغییر شد");
                                        if (new string(cchar[i]) == "000000000000000000000")
                                            tchar[i] = '0';
                                    }
                                    rullnumber++;
                                }
                               
                                db.updateTeacherNameByUsername(myteacher.username, myteacher.username, myteacher.password, myteacher.name, new string(tchar), new string(cchar[0]), new string(cchar[1]), new string(cchar[2]));
                            }
                            i++;
                          
                        }
                        
                       /* classesTakhasos1 = myteacher.classes1;
                        classesTakhasos2 = "000000000000000000000";
                        classesTakhasos3 = "000000000000000000000";*/
                    }

                    uncheckAll();
                    updateDataGrid();
                    //MessageBox.Show("اگر معلمی در حال ");
                }
                else
                {
                    MessageBox.Show("لطفا حداقل یک کلاس را انتخاب کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                updateDataGrid();
            }
            else
                MessageBox.Show("ورودی نامعتبر", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);


            have_We_any_Change = true;

        }
        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            if (have_We_any_Change == true)
            {
                waitImage.Visibility = Visibility.Visible;
                AutoClosingMessageBox.Show("لصفا تا آماده سازی لیست کلاس ها منتظر بمانید", "please wait", 60);
                updateListOfClasses();
            }
                
            waitImage.Visibility = Visibility.Hidden;
            this.Closed -= create_courses_Closed;
            this.Close();
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        }
        private void updateListOfClasses()
        {
            //AutoClosingMessageBox.Show("لصفا تا آماده سازی لیست کلاس ها منتظر بمانید", "please wait", 1000);
            int counter;
            coursesList = db.GetCourses();
            List<int> classes_should_addStudents = new List<int>();
            foreach (Courses sample in coursesList)
            {
                char[] classesBinary = sample.classes.ToCharArray();
                counter = 1;
                foreach (char result in classesBinary)
                {
                    if (result == '1')
                    {
                        db.createcourse_a_ofclass_b_Table(sample.id, counter);
                        classes_should_addStudents.Add(counter);
                    }
                    else
                        db.dropcourse_a_ofclass_b_Table(sample.id, counter);
                    counter++;
                }
            }
            foreach (int result in classes_should_addStudents) 
            {
                DataTable dataTable = db.Get_Table("select _id from Students where _classid=" + result.ToString());
                List<int> students_ids = dataTable.AsEnumerable()
                   .Select(r => r.Field<int>("_id"))
                   .ToList();
                foreach (int stuid in students_ids)
                {
                    db.Add_One_Student_Values_In_Classes_List(stuid);
                }
            }
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

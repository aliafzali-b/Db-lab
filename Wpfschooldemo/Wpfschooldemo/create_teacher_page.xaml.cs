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

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for create_teacher_page.xaml
    /// </summary>
    public partial class create_teacher_page : Window
    {
        public create_teacher_page()
        {
            InitializeComponent();
        }
        /*db-lab
        DataAccess db = new DataAccess();
        List<Classes> classesList = new List<Classes>();
        List<Courses> coursesList = new List<Courses>();
        List<Teachers> teachersList = new List<Teachers>();

        string takhasos = "000";
        string classesTakhasos1 = "000000000000000000000";
        string classesTakhasos2 = "000000000000000000000";
        string classesTakhasos3 = "000000000000000000000";
        char[] s = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '(' };
        public create_teacher_page()
        {
            InitializeComponent();
            this.Closed += new EventHandler(create_teacher_Closed);
           // usernameTextBox.Text = "for excute onchange text function";
            db.createTeachersTable();

            coursesList = db.GetCourses();
            classesList = db.GetClasses();
            foreach (Courses sample in coursesList)
                comboBox1.Items.Add(sample.name);
            searchComboBox.Items.Add("نام کاربری");
            searchComboBox.Items.Add("کلمه عبور");
            searchComboBox.Items.Add("نام و نام خانوادگی");

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = "";//for hidden opjects
            Refresh();
            addPanelButton_Click(sender, e);
            try
            {
                teachersDataGrid.Columns.RemoveAt(4);
                teachersDataGrid.Columns.RemoveAt(4);
                teachersDataGrid.Columns.RemoveAt(4);
                teachersDataGrid.Columns.RemoveAt(4);
            }
            catch { }
        }
        void create_teacher_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void addTakhasosButton_Click(object sender, RoutedEventArgs e)
        {
            char[] tchar = takhasos.ToCharArray();
            char[][] cchar = { classesTakhasos1.ToCharArray(), classesTakhasos2.ToCharArray(), classesTakhasos3.ToCharArray() };
            int selectedId1 = comboBox1.SelectedIndex;
            int selectedId2 = comboBox2.SelectedIndex;
            //char[] s = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '(' };
            char selectedId1CharFormat;

            int whichclass = enomim_yek_dar_kodam_khane_ast(selectedId1 + 1, selectedId2 + 1);
            bool tekrar = false;
            int n;
            if (selectedId1 != -1 && selectedId2 != -1)
            {
                selectedId1CharFormat = s[selectedId1];
                n = -1;
                foreach (char sample in tchar)
                {
                    n++;
                    if (sample == selectedId1CharFormat)
                    {
                        tekrar = true;
                        if (cchar[n][whichclass - 1] != '1')
                            //listBox1.Items.Add(coursesList[selectedId1].name + " class " + classesList[whichclass - 1].name);
                            updateListBox();//alaki ast
                        cchar[n][whichclass - 1] = '1';
                        break;
                    }
                }

                n = -1;
                if (tekrar == false)
                {
                    foreach (char sample in tchar)
                    {
                        n++;
                        if (sample == '0')
                        {
                            listBox1.Items.Add(coursesList[selectedId1].name + " class " + classesList[whichclass - 1].name);
                            tchar[n] = selectedId1CharFormat;
                            cchar[n][whichclass - 1] = '1';
                            break;
                        }
                    }
                }

            }
            else
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            
            takhasos = new string(tchar);
            classesTakhasos1 = new string(cchar[0]);
            classesTakhasos2 = new string(cchar[1]);
            classesTakhasos3 = new string(cchar[2]);
            updateListBox();
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
        private void deleteTakhasosButton_Click(object sender, RoutedEventArgs e)
        {
            char[] tchar = takhasos.ToCharArray();
            char[][] cchar = { classesTakhasos1.ToCharArray(), classesTakhasos2.ToCharArray(), classesTakhasos3.ToCharArray() };
            int selectedId1 = comboBox1.SelectedIndex;
            int selectedId2 = comboBox2.SelectedIndex;
            char[] s = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '(' };
            char selectedId1CharFormat;

            int whichclass = enomim_yek_dar_kodam_khane_ast(selectedId1 + 1, selectedId2 + 1);
            int n;
            if (selectedId1 != -1 && selectedId2 != -1)
            {
                selectedId1CharFormat = s[selectedId1];
                n = -1;
                foreach (char sample in tchar)
                {
                    n++;
                    if (sample == selectedId1CharFormat)
                    {
                        cchar[n][whichclass - 1] = '0';
                        bool shouldWeDeleteTakhasos = true;
                        foreach (char result in cchar[n])
                        {
                            if (result == '1')
                            {
                                shouldWeDeleteTakhasos = false;
                                break;
                            }
                        }
                        if (shouldWeDeleteTakhasos)
                            tchar[n] = '0';
                        break;
                    }
                }


            }
            else
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);


            takhasos = new string(tchar);
            classesTakhasos1 = new string(cchar[0]);
            classesTakhasos2 = new string(cchar[1]);
            classesTakhasos3 = new string(cchar[2]);

            updateListBox();
            UpdateDataGrid();
        }


        private int enomim_yek_dar_kodam_khane_ast(int idcourse, int n)
        {
            if (n > 0)
            {
                //DataAccess db = new DataAccess();
                coursesList = db.GetCourses();
                char[] c = coursesList[idcourse - 1].classes.ToCharArray();
                int counter = 0;
                int numberOfOnes = 0;
                foreach (char sample in c)
                {
                    counter++;
                    if (sample == '1')
                        numberOfOnes++;
                    if (numberOfOnes == n)
                        return counter;

                }
            }
            return -1;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            if (i != -1)
            {
                comboBox2.SelectedIndex = -1;
                comboBox2.Items.Clear();
                // comboBox2.SelectedItem = "";
                DataAccess db = new DataAccess();
                coursesList = db.GetCourses();
                classesList = db.GetClasses();
                int n = -1;
                char[] classes = coursesList[i].classes.ToCharArray();
                // MessageBox.Show(coursesList[i].classes);
                if (i != -1)
                {
                    foreach (char sample in classes)
                    {
                        n++;
                        if (sample == '1')
                        {
                            comboBox2.Items.Add(classesList[n].name);

                        }

                    }
                }
            }
        }
        private void newstart()
        {
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            nameTextBox.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            takhasos = "000";
            classesTakhasos1 = "000000000000000000000";
            classesTakhasos2 = "000000000000000000000";
            classesTakhasos3 = "000000000000000000000";
            listBox1.Items.Clear();
            Refresh();
        }

        private void addTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string name = nameTextBox.Text;
            if (username != "" && password != "" && name != "" && takhasos != "000")
            {
                
                //db-lab////db.insertIntoTeachersTable(username, password, name, takhasos, classesTakhasos1, classesTakhasos2, classesTakhasos3);
                UpdateDataGrid();
                newstart();
            }
            else
            {
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void usernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            teachersList = db.GetTeachers();
            bool isvalid = db.isTeacherNameValid(usernameTextBox.Text);
            if (isvalid == true)
            {
                errorImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                if (titleLable.Content == "افزودن معلم جدید")
                    trueImage.Visibility = Visibility.Visible;
                else
                    trueImage.Visibility = Visibility.Hidden;

                if (titleLable.Content == "ویرایش اطلاعات" || titleLable.Content == "حذف معلم")
                {
                    errorImage.Visibility = Visibility.Visible;
                    trueImage.Visibility = Visibility.Hidden;
                    warningImage.Visibility = Visibility.Hidden;
                    changeButton.IsEnabled = false;
                }

                addTeacherButton.IsEnabled = true;
                //changeButton.IsEnabled = true;
                teachersDataGrid.IsEnabled = true;
                nameTextBox.IsEnabled = true;
                passwordTextBox.IsEnabled = true;
                comboBox1.IsEnabled = true;
                comboBox2.IsEnabled = true;
                addTakhasosButton.IsEnabled = true;
                deleteTakhasosButton.IsEnabled = true;
                
            }
            else
            {
                errorImage.Visibility = Visibility.Visible;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addTeacherButton.IsEnabled = false;
                changeButton.IsEnabled = false;
                int teacherid = db.GetTeacherIdByUsername(usernameTextBox.Text);
                if (teacherid > 0)
                {
                    
                    if (titleLable.Content == "ویرایش اطلاعات")
                    {
                        //we are in change panel and have tekrari moalem
                        warningImage.Visibility = Visibility.Hidden;
                        errorImage.Visibility = Visibility.Hidden;
                        trueImage.Visibility = Visibility.Visible;
                        
                        newUserNameTextBox.IsEnabled = true;
                        newPasswordTextBox.IsEnabled = true;
                        newNameTextBox.IsEnabled = true;
                        changeButton.IsEnabled = true;

                        newUserNameTextBox.Text = teachersList[teacherid - 1].username;
                        newPasswordTextBox.Text = teachersList[teacherid - 1].password;
                        newNameTextBox.Text = teachersList[teacherid - 1].name;

                        teachersList = db.GetTeachers();

                        takhasos = teachersList[teacherid - 1].takhasos;
                        classesTakhasos1 = teachersList[teacherid - 1].classes1;
                        classesTakhasos2 = teachersList[teacherid - 1].classes2;
                        classesTakhasos3 = teachersList[teacherid - 1].classes3;
                        updateListBox();
                    }
                    else if (titleLable.Content == "افزودن معلم جدید")
                    {
                        //we are in add panel and have tekrari moalem
                        warningImage.Visibility = Visibility.Visible;
                        errorImage.Visibility = Visibility.Hidden;
                        trueImage.Visibility = Visibility.Hidden;
                        changeButton.IsEnabled = false;
                        teachersDataGrid.IsEnabled = false;
                        nameTextBox.IsEnabled = false;
                        passwordTextBox.IsEnabled = false;
                        comboBox1.IsEnabled = false;
                        comboBox2.IsEnabled = false;
                        addTakhasosButton.IsEnabled = false;
                        deleteTakhasosButton.IsEnabled = false;
                    }else if (titleLable.Content == "حذف معلم")
                    {
                        //we are in add panel and have tekrari moalem
                        warningImage.Visibility = Visibility.Hidden;
                        errorImage.Visibility = Visibility.Hidden;
                        trueImage.Visibility = Visibility.Visible;
                        //changeButton.IsEnabled = false;
                        //teachersDataGrid.IsEnabled = false;
                        //nameTextBox.IsEnabled = false;
                        //passwordTextBox.IsEnabled = false;
                        //comboBox1.IsEnabled = false;
                        //comboBox2.IsEnabled = false;
                        //addTakhasosButton.IsEnabled = false;
                        deleteButton.IsEnabled = true;
                    }
                    //deleteButton.Visibility = Visibility.Visible;
                    // changeButton.Visibility = Visibility.Visible;
                   

                   /* l1.Visibility = Visibility.Visible;
                    l2.Visibility = Visibility.Visible;
                    l3.Visibility = Visibility.Visible;*/
        /*db-lab
                }

              
            }
            if (usernameTextBox.Text == "")
            {
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addTeacherButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
                changeButton.IsEnabled = false;
                newUserNameTextBox.IsEnabled = false;
                newPasswordTextBox.IsEnabled = false;
                newNameTextBox.IsEnabled = false;
                newstart();
                /*newUserNameTextBox.Text = "";
                newPasswordTextBox.Text = "";
                newNameTextBox.Text = "";
                l1.Visibility = Visibility.Hidden;
                l2.Visibility = Visibility.Hidden;
                l3.Visibility = Visibility.Hidden;*/
        /*db-lab
            }
            //updateListBox();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            string newUsername = newUserNameTextBox.Text;
            string newPassword = newPasswordTextBox.Text;
            string newName = newNameTextBox.Text;
            string oldUsername = usernameTextBox.Text;
            if (takhasos == "000")
                MessageBox.Show("هیچ تخصصی برایش باقی نماند");
            
            if (newUsername != "" && newPassword != "" && newName != "" )
            {
                if (db.isTeacherNameValid(newUsername) || oldUsername == newUsername)
                {
                    db.updateTeacherNameByUsername(oldUsername,newUsername,newPassword,newName,takhasos,classesTakhasos1,classesTakhasos2,classesTakhasos3);
                    UpdateDataGrid();
                    newstart();
                }
            }
            else
            {
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateDataGrid()
        {
            teachersList = db.GetTeachers();
            teachersDataGrid.ItemsSource = teachersList;
            teachersDataGrid.IsReadOnly = true;
            try
            {
                teachersDataGrid.Columns.RemoveAt(4);
                teachersDataGrid.Columns.RemoveAt(4);
                teachersDataGrid.Columns.RemoveAt(4);
                teachersDataGrid.Columns.RemoveAt(4);
                teachersDataGrid.Columns[0].Header = " Id ";
                teachersDataGrid.Columns[1].Header = " نام کاربری ";
                teachersDataGrid.Columns[2].Header = " کلمه عبور ";
                teachersDataGrid.Columns[3].Header = " نام و نام خانوادگی  ";
            }
            catch { }
           // usernameTextBox.Text = "";//chon ba ezafe shodan item haye listbox in textbox meghdar migereft
        }
        private void updateListBox()
        {
            coursesList = db.GetCourses();
            classesList = db.GetClasses();
            char[] tchar = takhasos.ToCharArray();
            char[][] cchar = { classesTakhasos1.ToCharArray(), classesTakhasos2.ToCharArray(), classesTakhasos3.ToCharArray() };
            listBox1.Items.Clear();
            int counter;
            int courseid;
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
                            listBox1.Items.Add(coursesList[courseid - 1].name + " کلاس " + classesList[counter].name);
                        }
                        counter++;
                    }
                }
            }
        }
        //-------------------------------panel


        private void addPanelButton_Click(object sender, RoutedEventArgs e)
        {
            addPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E));//#FF3A3D4F
            changePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            viewallPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            deletePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            usernameTextBox.Text = "";

            errorImage.Visibility = Visibility.Hidden;
            trueImage.Visibility = Visibility.Hidden;
            warningImage.Visibility = Visibility.Hidden;
            addTeacherButton.IsEnabled = false;
            deleteButton.Visibility = Visibility.Hidden;
            changeButton.Visibility = Visibility.Hidden;
            newUserNameTextBox.Visibility = Visibility.Hidden;
            newPasswordTextBox.Visibility = Visibility.Hidden;
            newNameTextBox.Visibility = Visibility.Hidden;

            
            newUserNameTextBox.Text = "";
            newPasswordTextBox.Text = "";
            newNameTextBox.Text = "";
            l1.Visibility = Visibility.Hidden;
            l2.Visibility = Visibility.Hidden;
            l3.Visibility = Visibility.Hidden;
            addTeacherButton.Visibility = Visibility.Visible;
            lable3.Visibility = Visibility.Visible;
            lable4.Visibility = Visibility.Visible;
            usernameTextBox.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Visible;
            nameTextBox.Visibility = Visibility.Visible;
            titleLable.Content = "افزودن معلم جدید";
            comebackFromViewAllTeachers();
            
        }
        

        private void changePanelButton_Click(object sender, RoutedEventArgs e)
        {
            addPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));//#FF3A3D4F
            changePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E));
            viewallPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            deletePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            if (titleLable.Content == "افزودن معلم جدید")
                usernameTextBox.Text = "";

            errorImage.Visibility = Visibility.Hidden;
            trueImage.Visibility = Visibility.Hidden;
            warningImage.Visibility = Visibility.Hidden;
            addTeacherButton.Visibility = Visibility.Hidden;
            deleteButton.Visibility = Visibility.Hidden;
            lable3.Visibility = Visibility.Hidden;
            lable4.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Hidden;
            

            changeButton.Visibility = Visibility.Visible;
            newUserNameTextBox.Visibility = Visibility.Visible;
            newPasswordTextBox.Visibility = Visibility.Visible;
            newNameTextBox.Visibility = Visibility.Visible;
            newUserNameTextBox.Text = "";
            newPasswordTextBox.Text = "";
            newNameTextBox.Text = "";
            l1.Visibility = Visibility.Visible;
            l2.Visibility = Visibility.Visible;
            l3.Visibility = Visibility.Visible;
            titleLable.Content = "ویرایش اطلاعات";

            
            comebackFromViewAllTeachers();
        }
        private void changeRightClick(object sender, RoutedEventArgs e)
        {
            changePanelButton_Click(sender, e);
            Teachers myteacher = (Teachers)teachersDataGrid.SelectedItem;
            int selectedId = teachersDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                usernameTextBox.Text = myteacher.username;
            }
        }
        private void deleteRightClick(object sender, RoutedEventArgs e)
        {
            deletePanelButton_Click(sender, e);
            Teachers myteacher = (Teachers)teachersDataGrid.SelectedItem;
            int selectedId = teachersDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                usernameTextBox.Text = myteacher.username;
            }
        }
        private void seeCoursesRightClick(object sender, RoutedEventArgs e)
        {
            teachersList = db.GetTeachers();
            Teachers myteacher = (Teachers)teachersDataGrid.SelectedItem;
            int selectedId = teachersDataGrid.SelectedIndex;
            string result = "";
            if (selectedId >= 0)
            {
                //usernameTextBox.Text = myteacher.username;
                int teacherid = db.GetTeacherIdByUsername(myteacher.username);
                takhasos = teachersList[teacherid - 1].takhasos;
                classesTakhasos1 = teachersList[teacherid - 1].classes1;
                classesTakhasos2 = teachersList[teacherid - 1].classes2;
                classesTakhasos3 = teachersList[teacherid - 1].classes3;

                //update listbox function
                coursesList = db.GetCourses();
                classesList = db.GetClasses();
                char[] tchar = takhasos.ToCharArray();
                char[][] cchar = { classesTakhasos1.ToCharArray(), classesTakhasos2.ToCharArray(), classesTakhasos3.ToCharArray() };
                int counter;
                int courseid;
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
                                result += coursesList[courseid - 1].name + " کلاس " + classesList[counter].name +"\n";
                            }
                            counter++;
                        }
                    }
                }
                MessageBox.Show(result, "لیست کلاس ها", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            


        }
        private void comebackFromViewAllTeachers()
        {
            usernameTextBox.Visibility = Visibility.Visible;
            lable2.Visibility = Visibility.Visible;
            lable5.Visibility = Visibility.Visible;
            listBox1.Visibility = Visibility.Visible;
            comboBox1.Visibility = Visibility.Visible;
            comboBox2.Visibility = Visibility.Visible;
            addTakhasosButton.Visibility = Visibility.Visible;
            deleteTakhasosButton.Visibility = Visibility.Visible;
            teachersDataGrid.Visibility = Visibility.Hidden;
            searchLable.Visibility = Visibility.Hidden;
            searchTextBox.Visibility = Visibility.Hidden;
            searchComboBox.Visibility = Visibility.Hidden;
        }

        private void viewallPanelButton_Click(object sender, RoutedEventArgs e)
        {
            addPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));//#FF3A3D4F
            changePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            viewallPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E));
            deletePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));

            usernameTextBox.Text = "";

            errorImage.Visibility = Visibility.Hidden;
            trueImage.Visibility = Visibility.Hidden;
            warningImage.Visibility = Visibility.Hidden;
            addTeacherButton.Visibility = Visibility.Hidden;
            deleteButton.Visibility = Visibility.Hidden;
            changeButton.Visibility = Visibility.Hidden;
            lable3.Visibility = Visibility.Hidden;
            lable4.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Hidden;
            l1.Visibility = Visibility.Hidden;
            l2.Visibility = Visibility.Hidden;
            l3.Visibility = Visibility.Hidden;
            newUserNameTextBox.Visibility = Visibility.Hidden;
            newPasswordTextBox.Visibility = Visibility.Hidden;
            newNameTextBox.Visibility = Visibility.Hidden;
            teachersDataGrid.Visibility = Visibility.Hidden;

            // in ha bayad dar do menuey ghably visible shavand
            usernameTextBox.Visibility = Visibility.Hidden;
            lable2.Visibility = Visibility.Hidden;
            lable5.Visibility = Visibility.Hidden;
            listBox1.Visibility = Visibility.Hidden;
            comboBox1.Visibility = Visibility.Hidden;
            comboBox2.Visibility = Visibility.Hidden;
            addTakhasosButton.Visibility = Visibility.Hidden;
            deleteTakhasosButton.Visibility = Visibility.Hidden;

            titleLable.Content = "لیست تمامی معلمان"; //mitavanid rahat avaz konid
            teachersDataGrid.Visibility = Visibility.Visible;
            teachersDataGrid.Width = 960;

            searchLable.Visibility = Visibility.Visible;
            searchTextBox.Visibility = Visibility.Visible;
            searchTextBox.Text = "";
            searchComboBox.Visibility = Visibility.Visible;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            UpdateDataGrid();
            updateListBox();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Teachers> searchTeachersList = new List<Teachers>();
            int selectedIndex = searchComboBox.SelectedIndex;
            string[] indextext = {"_username","_password","_name"};
            string searchValue = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                if (selectedIndex != -1)
                {
                    searchTeachersList = db.SearchTeachers(indextext[selectedIndex], searchValue);
                    teachersDataGrid.ItemsSource = searchTeachersList;
                    teachersDataGrid.IsReadOnly = true;
                    try
                    {
                        teachersDataGrid.Columns.RemoveAt(4);
                        teachersDataGrid.Columns.RemoveAt(4);
                        teachersDataGrid.Columns.RemoveAt(4);
                        teachersDataGrid.Columns.RemoveAt(4);
                    }
                    catch { }
                }
            }
            else
            {
                UpdateDataGrid();
            }
            
        }

        private void deletePanelButton_Click(object sender, RoutedEventArgs e)
        {

            addPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));//#FF3A3D4F
            changePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            viewallPanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x42, 0x42));
            deletePanelButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E));
            usernameTextBox.Text = "";

            comebackFromViewAllTeachers();

            errorImage.Visibility = Visibility.Hidden;
            trueImage.Visibility = Visibility.Hidden;
            warningImage.Visibility = Visibility.Hidden;
            addTeacherButton.IsEnabled = false;
            deleteButton.Visibility = Visibility.Hidden;
            changeButton.Visibility = Visibility.Hidden;
            newUserNameTextBox.Visibility = Visibility.Hidden;
            newPasswordTextBox.Visibility = Visibility.Hidden;
            newNameTextBox.Visibility = Visibility.Hidden;


            lable5.Visibility = Visibility.Hidden;
            listBox1.Visibility = Visibility.Hidden;
            comboBox1.Visibility = Visibility.Hidden;
            comboBox2.Visibility = Visibility.Hidden;
            addTakhasosButton.Visibility = Visibility.Hidden;
            deleteTakhasosButton.Visibility = Visibility.Hidden;
            /* newUserNameTextBox.Text = "";
             newPasswordTextBox.Text = "";
             newNameTextBox.Text = "";*/
        /*db-lab
            l1.Visibility = Visibility.Hidden;
            l2.Visibility = Visibility.Hidden;
            l3.Visibility = Visibility.Hidden;
            addTeacherButton.Visibility = Visibility.Hidden;
            lable3.Visibility = Visibility.Hidden;
            lable4.Visibility = Visibility.Hidden;
            usernameTextBox.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Hidden;

            titleLable.Content = "حذف معلم";
            usernameTextBox.Visibility = Visibility.Visible;
            lable2.Visibility = Visibility.Visible;
            deleteButton.Visibility = Visibility.Visible;


        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            if (MessageBox.Show("آیا واقعا میخواهید این معلم حذف شود؟", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                db.deleteTeacherByUserName(username);
            usernameTextBox.Text = "";
        }

        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= create_teacher_Closed;
            this.Close();
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        */
    }
    
}

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

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for change_studentPage.xaml
    /// </summary>
    public partial class change_studentPage : Page
    {
        DataAccess db = new DataAccess();
        List<Classes> classesList = new List<Classes>();
        List<Students> studentsList = new List<Students>();
        public change_studentPage()
        {
            InitializeComponent();
            classesList = db.GetClasses();
            studentsList = db.GetStudents();
            foreach (Classes sample in classesList)
                classesComboBox.Items.Add(sample.name);
            oldusernameTextbox.Text = Globals.cache;
        }
        void change_studentPage_Closed(object sender, EventArgs e)
        {
            Globals.cache = "";
        }
        private void oldusernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isvalid = db.isStudentNameValid(oldusernameTextbox.Text);
            classesList = db.GetClasses();
            studentsList = db.GetStudents();
            errorImage.Visibility = Visibility.Visible;
            trueImage.Visibility = Visibility.Hidden;
            warningImage.Visibility = Visibility.Hidden;
            changeButton.IsEnabled = false;
            //if (isvalid)
            disableAllTextBoxes();
            if (db.GetStudentIdByUsername(oldusernameTextbox.Text) > 0)
            {
                trueImage.Visibility = Visibility.Visible;
                warningImage.Visibility = Visibility.Hidden;
                errorImage.Visibility = Visibility.Hidden;

                int stuid = db.GetStudentIdByUsername(oldusernameTextbox.Text);
                newusernameTextbox.Text = studentsList[stuid - 1].username;
                newpasswordTextbox.Text = studentsList[stuid - 1]._password;
                newnameTextbox.Text = studentsList[stuid - 1]._name;
                newfathernameTextbox.Text = studentsList[stuid - 1].fathername;
                newphonenumberTextbox.Text = (studentsList[stuid - 1].phone).ToString();
                classesComboBox.SelectedIndex = (studentsList[stuid - 1].classid)-1;
                changeButton.IsEnabled = true;
                enableAllTextBoxes();

            }
            
            if (oldusernameTextbox.Text == "")
            {
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
            }
        }
        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            studentsList = db.GetStudents();
            string oldusername = oldusernameTextbox.Text;
            string newusername = newusernameTextbox.Text;
            string newpassword = newpasswordTextbox.Text;
            string newname = newnameTextbox.Text;
            string newfathername = newfathernameTextbox.Text;
            long newphone = -1;
            try { newphone = Int64.Parse(newphonenumberTextbox.Text); } catch { }
            string newmajor = newmajorTextbox.Text;
            int classid = classesComboBox.SelectedIndex;



            if (newusername != "" && newpassword != "" && newname != "" && newfathername != "" && newphone >= 0 && classid >= 0)
            {
                // DataAccess db = new DataAccess();
                //db.insertIntoStudentsTable(username, password, name, fathername, phone, classid + 1);
                if (classid + 1 == studentsList[db.GetStudentIdByUsername(oldusername) - 1].classid)
                {
                    MessageBox.Show("کلاس همان قبلی است");
                    db.updateStudentByUsername(oldusername, newusername, newpassword, newname, newfathername, newphone, newmajor, classid + 1);

                    db.Update_Student_Values_In_Classes_List(db.GetStudentIdByUsername(oldusername));

                    Globals.cache = "";
                    clearAllTextBoxes();
                }
                else
                {
                    if (MessageBox.Show("با جابجا کردن کلاس تمام نمرات این دانش آموز پاک خواهد شد"+"\n"+"آیا مایل به ادامه روند هستید؟", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.updateStudentByUsername(oldusername, newusername, newpassword, newname, newfathername, newphone, newmajor, classid + 1);
                        db.deleteStudentOFClassesList(db.GetStudentIdByUsername(newusername));
                        db.Add_One_Student_Values_In_Classes_List(db.GetStudentIdByUsername(newusername));
                        clearAllTextBoxes();
                        Globals.cache = "";
                    }    
                }
            }
            else
            {
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void clearAllTextBoxes()
        {
            oldusernameTextbox.Text = "";
            newusernameTextbox.Text = "";
            newpasswordTextbox.Text = "";
            newnameTextbox.Text = "";
            newfathernameTextbox.Text = "";
            newphonenumberTextbox.Text = "";
            newmajorTextbox.Text = "";
            classesComboBox.SelectedIndex = -1;
        }
        private void disableAllTextBoxes()
        {
            newusernameTextbox.IsEnabled = false;
            newpasswordTextbox.IsEnabled = false;
            newnameTextbox.IsEnabled = false;
            newfathernameTextbox.IsEnabled = false;
            newphonenumberTextbox.IsEnabled = false;
            newmajorTextbox.IsEnabled = false;
            classesComboBox.SelectedIndex = -1;
        }
        private void enableAllTextBoxes()
        {
            newusernameTextbox.IsEnabled = true;
            newpasswordTextbox.IsEnabled = true;
            newnameTextbox.IsEnabled = true;
            newfathernameTextbox.IsEnabled = true;
            newphonenumberTextbox.IsEnabled = true;
            newmajorTextbox.IsEnabled = true;
        }
    }
}


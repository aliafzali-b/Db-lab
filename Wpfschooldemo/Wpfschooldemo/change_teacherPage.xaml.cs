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
    /// Interaction logic for change_teacherPage.xaml
    /// </summary>
    public partial class change_teacherPage : Page
    {
        DataAccess db = new DataAccess();

        List<Classes> classesList = new List<Classes>();
        List<Teachers> teachersList = new List<Teachers>();

        public change_teacherPage()
        {
            InitializeComponent();

            genderComboBox.Items.Add("آقا");
            genderComboBox.Items.Add("خانم");
        }
        void change_teacherPage_Closed(object sender, EventArgs e)
        {
            Globals.cache = "";
        }

        private void oldusernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isvalid = db.isTeacherNameValid(oldusernameTextbox.Text);
            classesList = db.GetClasses();
            teachersList = db.GetTeachers();
            errorImage.Visibility = Visibility.Visible;
            trueImage.Visibility = Visibility.Hidden;
            warningImage.Visibility = Visibility.Hidden;
            changeButton.IsEnabled = false;
            //if (isvalid)
            disableAllTextBoxes();
            if (db.GetTeacherIdByUsername(oldusernameTextbox.Text) >= 0)
            {
                trueImage.Visibility = Visibility.Visible;
                warningImage.Visibility = Visibility.Hidden;
                errorImage.Visibility = Visibility.Hidden;

                int stuid = db.GetTeacherIdByUsername(oldusernameTextbox.Text);
                newusernameTextbox.Text = teachersList[stuid].username;
                newpasswordTextbox.Text = teachersList[stuid]._password;
                newnameTextbox.Text = teachersList[stuid]._name;
                newlastnameTextbox.Text = teachersList[stuid].lastname;
                newspecialtyTextbox.Text = teachersList[stuid].expert;
                newphonenumberTextbox.Text = (teachersList[stuid].phone).ToString();
                newemailTextbox.Text = teachersList[stuid].email;
                if (teachersList[stuid].gender == 'M')
                    genderComboBox.SelectedIndex = 0;
                else if (teachersList[stuid].gender == 'F')
                    genderComboBox.SelectedIndex = 1;
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
            teachersList = db.GetTeachers();
            string oldusername = oldusernameTextbox.Text;
            string newusername = newusernameTextbox.Text;
            string newpassword = newpasswordTextbox.Text;
            string newname = newnameTextbox.Text;
            string newlastname = newlastnameTextbox.Text;
            string newexpert = newspecialtyTextbox.Text;
            long newphone = -1;
            try { newphone = Int64.Parse(newphonenumberTextbox.Text); } catch { }
            string newemail = newemailTextbox.Text;
            int genderid = genderComboBox.SelectedIndex;
            char gender;
            if (genderid == 0)
            {
                gender = 'M';
            }
            else
            {
                gender = 'F';
            }


            if (newusername != "" && newpassword != "" && newname != "" && newlastname != "" && newphone >= 0 && newemail != "" && genderid>=0)
            {
                // DataAccess db = new DataAccess();
                //db.insertIntoStudentsTable(username, password, name, fathername, phone, classid + 1);


                /*if (classid== teachersList[db.GetStudentIdByUsername(oldusername)].classid)
                {*/
                //MessageBox.Show("کلاس همان قبلی است");
                db.updateTeacherByUsername(oldusername, newusername, newpassword, newname, newlastname, newexpert, newphone, newemail, gender);

                //db.Update_Student_Values_In_Classes_List(db.GetStudentIdByUsername(oldusername));

                Globals.cache = "";
                clearAllTextBoxes();
                /*}
                else
                {
                    if (MessageBox.Show("با جابجا کردن کلاس تمام نمرات این دانش آموز پاک خواهد شد"+"\n"+"آیا مایل به ادامه روند هستید؟", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //db.updateStudentByUsername(oldusername, newusername, newpassword, newname, newfathername, newphone, classid + 1);
                        db.deleteStudentOFClassesList(db.GetStudentIdByUsername(newusername));
                        db.Add_One_Student_Values_In_Classes_List(db.GetStudentIdByUsername(newusername));
                        clearAllTextBoxes();
                        Globals.cache = "";
                    }    
                }*/
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
            newlastnameTextbox.Text = "";
            newspecialtyTextbox.Text = "";
            newphonenumberTextbox.Text = "";
            newemailTextbox.Text = "";
            genderComboBox.SelectedIndex = -1;
        }
        private void disableAllTextBoxes()
        {
            newusernameTextbox.IsEnabled = false;
            newpasswordTextbox.IsEnabled = false;
            newnameTextbox.IsEnabled = false;
            newlastnameTextbox.IsEnabled = false;
            newspecialtyTextbox.IsEnabled = false;
            newphonenumberTextbox.IsEnabled = false;
            newemailTextbox.IsEnabled = false;
            //newmajorTextbox.IsEnabled = false;
            genderComboBox.SelectedIndex = -1;
        }
        private void enableAllTextBoxes()
        {
            newusernameTextbox.IsEnabled = true;
            newpasswordTextbox.IsEnabled = true;
            newnameTextbox.IsEnabled = true;
            newlastnameTextbox.IsEnabled = true;
            newspecialtyTextbox.IsEnabled = true;
            newphonenumberTextbox.IsEnabled = true;
            newemailTextbox.IsEnabled = true;
            //newmajorTextbox.IsEnabled = true;
        }













    }
}

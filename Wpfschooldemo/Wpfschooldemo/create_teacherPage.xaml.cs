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
    /// Interaction logic for create_teacherPage.xaml
    /// </summary>
    public partial class create_teacherPage : Page
    {
        DataAccess db = new DataAccess();
        public create_teacherPage()
        {
            InitializeComponent();
            genderComboBox.Items.Add("آقا");
            genderComboBox.Items.Add("خانم");
        }
        private void usernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isvalid = db.isTeacherNameValid(usernameTextbox.Text);
            if (isvalid == true)
            {
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Visible;
                warningImage.Visibility = Visibility.Hidden;
                addButton.IsEnabled = true;
            }
            else
            {
                errorImage.Visibility = Visibility.Visible;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addButton.IsEnabled = false;
                if (db.GetTeacherIdByUsername(usernameTextbox.Text) > 0)
                {
                    warningImage.Visibility = Visibility.Visible;
                    errorImage.Visibility = Visibility.Hidden;
                }
            }
            if (usernameTextbox.Text == "")
            {
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addButton.IsEnabled = false;
            }
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextbox.Text;
            string password = passwordTextbox.Text;
            string name = nameTextbox.Text;
            string lastname = lastnameTextbox.Text;
            string expert = specialtyTextbox.Text;
            
            long phone = -1;
            try { phone = Int64.Parse(phonenumberTextbox.Text); } catch { }
            string email = emailTextbox.Text;
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


            if (username != "" && password != "" && name != "" && lastname != "" && phone >= 0 && email!="" && genderid >= 0)
            {
                // DataAccess db = new DataAccess();
                db.insertIntoTeachersTable(username, password, name, lastname, expert, phone, email,gender,0);
                //db.Add_One_Student_Values_In_Classes_List(db.GetStudentIdByUsername(username));

                clearAllTextBoxes();
                MessageBox.Show("انجام شد", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void clearAllTextBoxes()
        {
            usernameTextbox.Text = "";
            passwordTextbox.Text = "";
            nameTextbox.Text = "";
            lastnameTextbox.Text = "";
            emailTextbox.Text = "";
            phonenumberTextbox.Text = "";
            specialtyTextbox.Text = "";
            genderComboBox.SelectedIndex = -1;
        }
    }
}

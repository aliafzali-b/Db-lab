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
    /// Interaction logic for create_studentPage.xaml
    /// </summary>
    public partial class create_studentPage : Page
    {
        DataAccess db = new DataAccess();
        List<Classes> classesList = new List<Classes>();
        Random rnd = new Random();
        public create_studentPage()
        {
            InitializeComponent();
            classesList = db.GetClasses();
            foreach (Classes sample in classesList)
                classesComboBox.Items.Add(sample.name);

        }
        private void usernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isvalid = db.isStudentNameValid(usernameTextbox.Text);
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
                if (db.GetStudentIdByUsername(usernameTextbox.Text) > 0)
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
            string fathername = fathernameTextbox.Text;
            long phone=-1;
            try { phone = Int64.Parse(phonenumberTextbox.Text); } catch { }
            string address = addressTextbox.Text;
            string info = infoTextbox.Text;

            int majorid = majorsComboBox.SelectedIndex;
            int classid = classesComboBox.SelectedIndex;
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


            if (username != "" && password != "" && name != "" && lastname != "" && fathername != "" && phone >=0 && classid >=0)
            {
                // DataAccess db = new DataAccess();
                db.insertIntoStudentsTable(username, password, name,lastname, fathername,phone,address,info,majorid,classid,gender);
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
            fathernameTextbox.Text = "";
            phonenumberTextbox.Text = "";
            majorsComboBox.SelectedIndex = -1;
            classesComboBox.SelectedIndex = -1;
        }

        private void FillClassesButton_Click(object sender, RoutedEventArgs e)
        {
            string username;
            string password;
            string name;
            string fathername;
            long phone ;
            string major;

            classesList = db.GetClasses();
            int range = classesList.Count();
            foreach (Classes sample in classesList)
            {
                Random ttt = new Random();
                int number = ttt.Next(20, 30);
                for (int i = 0; i < number; i++)
                {
                    username = randomString(4);
                    password = randomString(2);
                    name = randomName() + " " + randomFamilyName();
                    fathername = randomFatherName();
                    Random phonernd = new Random();
                    phone = ttt.Next(913000000, 915000000);
                    major = randomMajor();
                    //must work db.insertIntoStudentsTable(username, password, name, fathername, phone, major, sample.id);
                }
            }
        }
        private string randomString(int length)
        {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        
        private string randomName()
        {
            string[] name ={"علی","محمد", "کاظم", "رضا", "اصغر", "امین", "حسین", "مرتضی", "احمد", "رسول", "کامران", "کامبیز", "کوروش", "محمدرضا", "مصطفی", "مهدی", "محمدحسین", "بهرام", "شهاب", "رامبد", "مهران", "مسیح", "جلال", "کمال", "اکبر", "یزدان", "امیرحسین", "هادی", "علیرضا", "محمدکاظم", "امین مهدی", "سیاوش", "حسن", "سجاد", "باقر", "جواد", "عباس", "ایمان", "فردین" };
            int number = rnd.Next(1, name.Length);
            return name[number-1];
        }
        private string randomFatherName()
        {
            string[] name = { "علی", "محمد", "کاظم", "رضا", "اصغر", "امین", "حسین", "مرتضی", "احمد", "رسول", "کامران", "کامبیز", "کوروش", "محمدرضا", "مصطفی", "مهدی", "محمدحسین", "بهرام", "شهاب", "رامبد", "مهران", "مسیح", "جلال", "کمال", "اکبر", "یزدان", "امیرحسین", "هادی", "علیرضا", "محمدکاظم", "امین مهدی", "سیاوش", "حسن", "سجاد", "باقر", "جواد", "عباس", "ایمان", "فردین" };
            int number = rnd.Next(1, name.Length);
            return name[number - 1];
        }
        private string randomFamilyName()
        {
            string[] familyName = { "افضلی", "حسام پور", "صفرزاده", "باقری", "عرب", "حیایی", "پورعرب", "حیدری", "مهدوی", "سبحانی", "شکوری", "دهقانی", "اسلامی", "خسروی", "رضوی", "موسوی", "سیدی", "محمدی", "صفری", "محمدی پور", "افضلی پور", "ابراهیمی", "شهابی", "باقری", "بحرینی", "قوام", "امیرحسینی", "باهنر", "رجایی", "بهشتی", "لاریجانی", "احمدی", "رشیدی", "شفیعی", "قاضی زاده", "رستمی", "صفاپور", "کریمی", "کمالی" };
            int number = rnd.Next(1, familyName.Length);
            return familyName[number-1];
        }
        private string randomMajor()
        {
            string[] major = { "ریاضی فیزیک", "علوم تجربی", "ادبیات فارسی"};
            int number = rnd.Next(1, major.Length);
            return major[number-1];
        }
    }   

}

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

namespace WpfTeacherDemo
{
    /// <summary>
    /// Interaction logic for AddExam_Page.xaml
    /// </summary>
    public partial class AddExam_Page : Page
    {
        DataAccess db = new DataAccess();
        List<Classes> ClassesList = new List<Classes>();
        List<Courses> CoursesList = new List<Courses>();
        public AddExam_Page()
        {
            InitializeComponent();
            CoursesList = db.GetCourses();
            ClassesList = db.GetClasses();
            pageTitleLable.Content = "افزودن امتحان به " + CoursesList[Globals.myTeacherSelectedCourseId - 1].name + " کلاس " + ClassesList[Globals.myTeacherSelectedClassId - 1].name;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string _name = nameTextbox.Text;
            string _date = dateTextbox.Text;
            string _time = timeTextbox.Text;
            string _info = infoTextbox.Text;
            float _zarib = -1;
            try { _zarib = float.Parse(zaribTextbox.Text); } catch { }

            if (_name != "" && _date != "" && _time != "" && _info != "" && _zarib > 0 )
            {
                // DataAccess db = new DataAccess();
                db.insertIntoExames(_name, _date, _time, _info, _zarib);
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
            nameTextbox.Text = "";
            dateTextbox.Text = "";
            timeTextbox.Text = "";
            infoTextbox.Text = "";
            zaribTextbox.Text = "";
        }

    }
}

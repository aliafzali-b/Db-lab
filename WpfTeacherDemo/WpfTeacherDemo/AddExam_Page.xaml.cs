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
        string yymmdd= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DateTime examdate = DateTime.Now;
        List <Classes> ClassesList = new List<Classes>();
        List<Courses> CoursesList = new List<Courses>();
        public AddExam_Page()
        {
            InitializeComponent();
            CoursesList = db.GetCourses();
            ClassesList = db.GetClasses();
            //pageTitleLable.Content = "افزودن امتحان به " + CoursesList[Globals.myTeacherSelectedCourseId - 1].name + " کلاس " + ClassesList[Globals.myTeacherSelectedClassId - 1].name;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //string _name = nameTextbox.Text;
            //string _date = dateTextbox.Text;
            string _timeHH = timeHHTextbox.Text;
            string _timeMM = timeMMTextbox.Text;
            float _zarib = -1;
            try { _zarib = float.Parse(zaribTextbox.Text); } catch { }
            string _info = infoTextbox.Text;
            int status = 0;

            if (calender1.SelectedDate.HasValue && !String.IsNullOrEmpty(_timeHH) && !String.IsNullOrEmpty(_timeMM))
            {
                // ... Display SelectedDate in Title.
                DateTime date = calender1.SelectedDate.Value;
                TimeSpan ts = new TimeSpan(Int32.Parse(_timeHH), Int32.Parse(_timeMM), 0);
                date = date.Date + ts;
                examdate = date;

                //this.Title = date.ToShortDateString();
                //this.Title = date.ToString("yyyy-MM-dd");
                //this.Title = date.ToString("yyyy-MM-dd h:mm");
                yymmdd = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //MessageBox.Show(yymmdd);
                if (yymmdd != "" && _info != "" && _zarib > 0)
                {
                    db.insertIntoExames(yymmdd, _zarib, _info, status);
                    clearAllTextBoxes();
                    MessageBox.Show("انجام شد", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }
        private void clearAllTextBoxes()
        {
            //nameTextbox.Text = "";
            //dateTextbox.Text = "";
            timeHHTextbox.Text = "";
            timeMMTextbox.Text = "";
            infoTextbox.Text = "";
            zaribTextbox.Text = "";
        }
        private void Calendar_SelectedDatesChanged(object sender,SelectionChangedEventArgs e)
        {
            // ... Get reference.
            var calendar = sender as Calendar;

            // ... See if a date is selected.
            if (calendar.SelectedDate.HasValue)
            {
                // ... Display SelectedDate in Title.
                DateTime date = calendar.SelectedDate.Value;
                TimeSpan ts = new TimeSpan(22, 30, 0);
                date = date.Date + ts;
                examdate = date;

                //this.Title = date.ToShortDateString();
                //this.Title = date.ToString("yyyy-MM-dd");
                //this.Title = date.ToString("yyyy-MM-dd h:mm");
                yymmdd = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //MessageBox.Show(yymmdd);
            }
        }
    }
}

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
using System.Data;

namespace Wpfschooldemo
{
    /// <summary>
    /// Interaction logic for function_teacherPage.xaml
    /// </summary>
    public partial class function_teacherPage : Page
    {
        DataAccess db = new DataAccess();
        public function_teacherPage()
        {
            InitializeComponent();
        }

        private void f1Button_Click(object sender, RoutedEventArgs e)
        {
            string studentUsername = studentUsernameTextbox.Text;
            if (!String.IsNullOrWhiteSpace(studentUsername))
            {
                string f1Query = "select * from nearstexams('" + studentUsername + "') order by _date";
                DataTable myResult = db.Get_Table(f1Query);
                resultDataGrid.Columns.Clear();
                resultDataGrid.ItemsSource = myResult.DefaultView;
            }
            else
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        private void f2Button_Click(object sender, RoutedEventArgs e)
        {
            string t1Username = t1UsernameTextbox.Text;
            string t2Username = t2UsernameTextbox.Text;
            string t3Username = t3UsernameTextbox.Text;
            if (!String.IsNullOrWhiteSpace(t1Username) && !String.IsNullOrWhiteSpace(t2Username) && !String.IsNullOrWhiteSpace(t3Username))
            {
                string f2Query = "select * from xyzCommonTeachers('" + t1Username + "','" + t2Username + "','" + t3Username + "')";
                DataTable myResult = db.Get_Table(f2Query);
                resultDataGrid.Columns.Clear();
                resultDataGrid.ItemsSource = myResult.DefaultView;
            }
            else
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        private void f3Button_Click(object sender, RoutedEventArgs e)
        {
            string f3Query = "select * from BestTeachers() order by score DESC";
            DataTable myResult = db.Get_Table(f3Query);
            resultDataGrid.Columns.Clear();
            resultDataGrid.ItemsSource = myResult.DefaultView;
        }
    }
}

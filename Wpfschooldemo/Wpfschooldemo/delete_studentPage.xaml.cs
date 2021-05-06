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
    /// Interaction logic for delete_studentPage.xaml
    /// </summary>
    public partial class delete_studentPage : Page
    {
        DataAccess db = new DataAccess();
        List<Classes> classesList = new List<Classes>();
        List<Students> studentsList = new List<Students>();
        public delete_studentPage()
        {
            InitializeComponent();
            usernameTextbox.Text = Globals.cache;
        }
        void delete_studentPage_Closed(object sender, EventArgs e)
        {
            Globals.cache = "";
        }
        private void usernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (usernameTextbox.Text == "")
                errorImage.Visibility = Visibility.Hidden;
            else
                errorImage.Visibility = Visibility.Visible;
            trueImage.Visibility = Visibility.Hidden;
            deleteButton.IsEnabled = false;
            if (db.GetStudentIdByUsername(usernameTextbox.Text) >= 0)
            {
                trueImage.Visibility = Visibility.Visible;
                errorImage.Visibility = Visibility.Hidden;

                int stuid = db.GetStudentIdByUsername(usernameTextbox.Text);
                deleteButton.IsEnabled = true;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextbox.Text;
            if (MessageBox.Show("آیا واقعا میخواهید این دانش آموز حذف شود؟", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ///db.deleteStudentOFClassesList(db.GetStudentIdByUsername(username));
                db.deleteStudentByUserName(username);
            }

            usernameTextbox.Text = "";
            Globals.cache = "";
        }
    }
}

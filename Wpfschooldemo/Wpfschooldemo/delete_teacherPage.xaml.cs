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
    /// Interaction logic for delete_teacherPage.xaml
    /// </summary>
    public partial class delete_teacherPage : Page
    {
        DataAccess db = new DataAccess();
        public delete_teacherPage()
        {
            InitializeComponent();
            usernameTextbox.Text = Globals.cache;
        }
        void delete_teacherPage_Closed(object sender, EventArgs e)
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
            if (db.GetTeacherIdByUsername(usernameTextbox.Text) >= 0)
            {
                trueImage.Visibility = Visibility.Visible;
                errorImage.Visibility = Visibility.Hidden;

                int teacherId = db.GetTeacherIdByUsername(usernameTextbox.Text);
                deleteButton.IsEnabled = true;
            }
        }


        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextbox.Text;
            if (MessageBox.Show("آیا واقعا میخواهید این معلم حذف شود؟", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ///db.deleteStudentOFClassesList(db.GetStudentIdByUsername(username));
                db.deleteTeacherByUserName(username);
            }
            usernameTextbox.Text = "";
            Globals.cache = "";
        }
    }
}

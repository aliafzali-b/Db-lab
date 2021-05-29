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
    /// Interaction logic for create_Boss_Window.xaml
    /// </summary>
    public partial class create_Boss_Window : Window
    {
        DataAccess db = new DataAccess();
        public create_Boss_Window()
        {
            InitializeComponent();
            this.Closed += new EventHandler(create_Boss_Window_Closed);
            if (Globals.LogedIn)
                returnImage.Visibility = Visibility.Visible;
            else
                returnImage.Visibility = Visibility.Hidden;

            genderComboBox.Items.Add("آقا");
            genderComboBox.Items.Add("خانم");
            //db.Create_All_Functions();
        }
        void create_Boss_Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextbox.Text;
            string password = passwordTextbox.Text;
            string name = nameTextbox.Text;
            string lastname = lastnameTextbox.Text;
            string address = addressTextbox.Text;
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
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastname) && !string.IsNullOrWhiteSpace(address) && !string.IsNullOrWhiteSpace(email) && genderid>=0)
            {
                db.initializeBoss(username, password, name, lastname, address, email, gender);
                this.Closed -= create_Boss_Window_Closed;
                this.Close();
                var MainWindow = new MainWindow();
                MainWindow.Show();
            }
            else
                MessageBox.Show("لطفا ورودی را کنترل کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= create_Boss_Window_Closed;
            this.Close();
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        }
    }
}

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
            string email = emailTextbox.Text;
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
            {
                db.initializeBoss(username, password, name, email);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.Create_All_Tables();
        }
    }
}

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {
        public static string cache;
        DataAccess db = new DataAccess();
        List<Classes> classesList = new List<Classes>();
        List<Courses> coursesList = new List<Courses>();
        List<Boss> myBoss = new List<Boss>();
        public MainWindow()
        {
            Globals.cache = "";
            InitializeComponent();
            this.Closed += new EventHandler(MainWindow_Closed);
            try
            {
                db.Create_All_Tables();
                
                if (!db.isThereAnyBoss())
                {
                    this.Hide();
                    var create_Boss_Window = new create_Boss_Window();
                    create_Boss_Window.Show();
                }
            }
            catch 
            {
                if (!db.isThereAnyBoss())
                {
                    this.Hide();
                    var create_Boss_Window = new create_Boss_Window();
                    create_Boss_Window.Show();
                }
            }
            try
            {
                myBoss = db.GetBoss();
                if (myBoss[0].rememberme == 1)
                {
                    usernameTextbox.Text = myBoss[0].username;
                    passwordBox.Password = myBoss[0]._password;
                    rememberCheckbox.IsChecked = true;
                }
                else
                    rememberCheckbox.IsChecked = false;
            }
            catch { }
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        } 

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextbox.Text;
            string password = passwordBox.Password;
            if (db.isBossUsernamePasswordCorrect(username, password))
            {
                
                if (rememberCheckbox.IsChecked == true)
                    db.rememberBoss(1);
                else
                    db.rememberBoss(0);
                this.Hide();
                var Boss_Panel = new Boss_Panel();
                Boss_Panel.Show();
            }
            else
            {
                MessageBox.Show("کلمه عبور یا نام کاربری نادرست است", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExitImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}

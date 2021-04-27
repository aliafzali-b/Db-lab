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
    /// Interaction logic for userGuidePage.xaml
    /// </summary>
    public partial class userGuidePage : Window
    {
        public userGuidePage()
        {
            InitializeComponent();
            this.Closed += new EventHandler(userGuidePage_Closed);
        }
        void userGuidePage_Closed(object sender, EventArgs e)
        {
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        }
        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}

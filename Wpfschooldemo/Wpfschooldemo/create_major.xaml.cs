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
    /// Interaction logic for create_major.xaml
    /// </summary>
    public partial class create_major : Page
    {
        DataAccess db = new DataAccess();
        List<Major> classesList = new List<Major>();
        public create_major()
        {
            
            InitializeComponent();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextbox.Text;
            int code = -1;
            try { code = Int32.Parse(codeTextbox.Text); } catch { }
            

            if(name != "")
            {
                db.insertIntoMajorTable(name, code);
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
            codeTextbox.Text = "";
            
        }


        private void nameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isvalid = db.isMajorNameValid(nameTextbox.Text);
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
              
            }
            if (nameTextbox.Text == "")
            {
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addButton.IsEnabled = false;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        { }

    }
}

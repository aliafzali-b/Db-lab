using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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
    /// Interaction logic for Karname_page.xaml
    /// </summary>
    public partial class Karname_page : Page
    {
        DataAccess db = new DataAccess();
        public Karname_page()
        {
            InitializeComponent();
            usernameTextbox.Text = Globals.cache;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            changeName();
        }
        void Karname_page_Closed(object sender, EventArgs e)
        {
            Globals.cache = "";
        }

        private void usernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
           /*aliali  Globals.karname_List.Clear();*/
            
            if (db.GetStudentIdByUsername(usernameTextbox.Text) > -1)
            {
                UpdateDataGrid();
            }
            else
            {
                karnameDataGrid.Columns.Clear();
            }
        }
        private void UpdateDataGrid()
        {
            string stuUsername = usernameTextbox.Text;
            DataTable myResult = db.Get_Table("select * from karname('"+ stuUsername + "')");
            karnameDataGrid.Columns.Clear();
            karnameDataGrid.ItemsSource = myResult.DefaultView;
            karnameDataGrid.IsReadOnly = true;
            changeName();
        }
        private void changeName()
        {
            try {
                karnameDataGrid.Columns[0].Header = " نام درس ";
                karnameDataGrid.Columns[1].Header = " معدل ";
            } catch { }
        }
    }
}

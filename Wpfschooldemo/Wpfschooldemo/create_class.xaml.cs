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
    /// Interaction logic for create_class.xaml
    /// </summary>
    public partial class create_class : Window
    {
        List<Classes> classlist = new List<Classes>();
        List<Major> classesList = new List<Major>();
        DataAccess db = new DataAccess();
        public create_class()
        {
            InitializeComponent();
            db.createClassesTable();
            classlist = db.GetClasses();
            textBox1.Text = "for excute onchange text function";

            classesList = db.GetMajor();
            foreach (Major sample in classesList)
                MajorComboBox.Items.Add(sample.name);


            updateDataGrid();
            this.Closed += new EventHandler(create_class_Closed);

        }

        void create_class_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        void create_class_Loaded(object sender,EventArgs e)
        {
            try
            {
                dataGrid1.Columns[0].Header = " Id ";
                dataGrid1.Columns[1].Header = " نام ";
            }
            catch { }
        }
        private void updateDataGrid()
        {
            classlist = db.GetClasses();
            dataGrid1.ItemsSource = classlist;
           // dataGrid1.Columns.RemoveAt(2);
            dataGrid1.IsReadOnly = true;
            textBox1.Text = "";//chon ba ezafe shodan item haye listbox in textbox meghdar migereft
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isvalid = db.isClassNameValid(textBox1.Text);
            if (isvalid == true)
            {
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Visible;
                warningImage.Visibility = Visibility.Hidden;
                addButton.IsEnabled = true;
                deleteButton.Visibility = Visibility.Hidden;
                changeButton.Visibility = Visibility.Hidden;
                textBox2.Visibility = Visibility.Hidden;
                nameLable.Visibility = Visibility.Hidden;
                textBox2.Text = "";
            }
            else
            {
                errorImage.Visibility = Visibility.Visible;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addButton.IsEnabled = false;
                if (db.GetClassIdByName(textBox1.Text) > 0)
                {
                    deleteButton.Visibility = Visibility.Visible;
                    changeButton.Visibility = Visibility.Visible;
                    warningImage.Visibility = Visibility.Visible;
                    errorImage.Visibility = Visibility.Hidden;
                    textBox2.Visibility = Visibility.Visible;
                    nameLable.Visibility = Visibility.Visible;
                    textBox2.Text = "";
                }
            }
            if (textBox1.Text == "")
            {
                errorImage.Visibility = Visibility.Hidden;
                trueImage.Visibility = Visibility.Hidden;
                warningImage.Visibility = Visibility.Hidden;
                addButton.IsEnabled = false;
                deleteButton.Visibility = Visibility.Hidden;
                changeButton.Visibility = Visibility.Hidden;
                textBox2.Visibility = Visibility.Hidden;
                nameLable.Visibility = Visibility.Hidden;
                textBox2.Text = "";
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            string ClassName = textBox1.Text;
            int Chairnum = -1;
            try { Chairnum = Int32.Parse(TextBoxChairNum.Text); } catch { }

            int BranchNum = -1;
            try { BranchNum = Int32.Parse(TextBoxBranchNum.Text); } catch { }

            string Year = TextBoxYear.Text;

            int MajorID = MajorComboBox.SelectedIndex;



            if (db.isClassNameValid(textBox1.Text) && ClassName != "" && Year != "" && Chairnum >= 0 && MajorID >=0 && BranchNum >=0)
                db.insertIntoClasses(ClassName,Year,MajorID,Chairnum,BranchNum);
            else
                MessageBox.Show("ورودی نامعتبر", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            updateDataGrid();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            Classes myclass = (Classes)dataGrid1.SelectedItem;
            if (myclass != null)
            {
                int selectedId = myclass.classid - 1;
                if (selectedId >= 0)
                    textBox1.Text = classlist[selectedId]._name;
            } 
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            string oldname = textBox1.Text;
            string newname = textBox2.Text;

            //string ClassName = textBox1.Text;
            int Chairnum = -1;
            try { Chairnum = Int32.Parse(TextBoxChairNum.Text); } catch { }

            int BranchNum = -1;
            try { BranchNum = Int32.Parse(TextBoxBranchNum.Text); } catch { }

            string Year = TextBoxYear.Text;

            int MajorID = MajorComboBox.SelectedIndex;

            if (db.isClassNameValid(newname))
            {
                db.changeClassNameByName(oldname, newname, Chairnum, BranchNum, Year, MajorID);
                updateDataGrid();
            }
            else
                MessageBox.Show("ورودی نامعتبر", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }
        private void returnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Closed -= create_class_Closed;
            this.Close();
            var Boss_Panel = new Boss_Panel();
            Boss_Panel.Show();
        }

        private void AddMajorButton_Click(object sender, RoutedEventArgs e)
        {
            this.Closed -= create_class_Closed;
            this.Close();
            var create_major = new create_major();
            create_major.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string ClassName = textBox1.Text;
            if (MessageBox.Show("آیا واقعا میخواهید این کلاس حذف شود؟", "Are you sure", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                db.removeClassByName(ClassName);
            }
            
        }
    }
}

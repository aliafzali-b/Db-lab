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

namespace WpfTeacherDemo
{
    /// <summary>
    /// Interaction logic for Datagrid_Page.xaml
    /// </summary>
    public partial class Datagrid_Page : Page
    {
        DataAccess db = new DataAccess();
    
        List<StudentsOfClass> StudentsOfClassList = new List<StudentsOfClass>();
        public Datagrid_Page()
        {
            InitializeComponent();
            List<StudentsOfClass> StudentsOfClassList = new List<StudentsOfClass>();
            StudentsOfClassList = db.Get_List_Course_a_Class_b(Globals.myTeacherSelectedCourseId, Globals.myTeacherSelectedClassId);
            classDataGrid.ItemsSource = StudentsOfClassList;
        }
        private void sabtButton(object sender, RoutedEventArgs e)
        {
            /*StudentsOfClass myStudent = (StudentsOfClass)classDataGrid.SelectedItem;
            int selectedId = classDataGrid.SelectedIndex;
            if (selectedId >= 0)
            {
                db.UpdateTaple(myStudent);
            }*/
            StudentsOfClassList = db.Get_List_Course_a_Class_b(Globals.myTeacherSelectedCourseId, Globals.myTeacherSelectedClassId);

            int counter = 0;
            foreach (StudentsOfClass sample in StudentsOfClassList)
            {

                StudentsOfClass myStudent = (StudentsOfClass)classDataGrid.Items[counter];
                db.UpdateTaple(myStudent);
                counter++;
            }
            //MessageBox.Show(StudentsOfClassList[0].g10); 
        }
    }
}

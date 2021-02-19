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

namespace TestCaseGenerator
{
    /// <summary>
    /// Interaction logic for SelectionPagr.xaml
    /// </summary>
    public partial class SelectionPagr : Page
    {
        List<Course> crs = new List<Course>();
        public SelectionPagr()
        {
            InitializeComponent();
        }

        private void AddCourses()
        {
            Course c = new Course();
            c.course = "Programming";
            c.topic = new List<string> { "Array","Operators","Basics","Conditional Statements"};
            crs.Add(c);
            Course c1 = new Course();
            c1.course = "Machine Learning";
            c1.topic = new List<string> { "NLP", "Basics", "Conditional Statements" };
            crs.Add(c1);

            
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.NavigationService.Navigate(new Question());
        }
    }
}

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new SelectionPagr());
        }

        //public MainWindow(string Course, string topic)
        //{
        //    var obj = new Question(Course, topic);
        //    NavigationService.GetNavigationService(this).Navigate(obj);
        //    //MainFrame.NavigationService.Navigate(new Question(Course,topic));
        //}


        private void btnSelectionFrame_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new SelectionPagr());
        }

        private void btnQuesFrame_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Question());
        }
    }
}

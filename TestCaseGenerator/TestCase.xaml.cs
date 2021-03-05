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
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace TestCaseGenerator
{
    
    
    /// <summary>
    /// Interaction logic for TestCase.xaml
    /// </summary>
    public partial class TestCase : Page
    {
        public static string connectionString = @"Data Source=DESKTOP-JI48AUG\SQLEXPRESS;Initial Catalog=Coherence;Integrated Security=True";
        List<string> inputType = new List<string>();
        ObservableCollection<string> par = new ObservableCollection<string>();
        ObservableCollection<string> constraints = new ObservableCollection<string>();
        List<string> mylist = new List<string>();
        
        public TestCase()
        {
            InitializeComponent();
        }

        public TestCase(string s)
        {
            InitializeComponent();
            txtblckDisplayQues.Text = s;
        }

        private void btnAddParam_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(txtboxParameter.Text);
            
            for (int i = 0; i < count; i++)
            {
                par.Add("a");

            }
            lstboxParameter.ItemsSource = par;
            
        }

        private void btnRemovePAram_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string data = b.DataContext as string;
            int index = par.IndexOf(data);

            par.RemoveAt(index);
        }

        private void GetTypes()
        {
            
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select distinct(Type) from InputTypes";
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        inputType.Add(dr.GetString(0));
                    }

                    con.Close();
                }

                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void btnAddTestCase_Click(object sender, RoutedEventArgs e)
        {
            
            int count = Convert.ToInt32(txtboxTestCase.Text);

            for (int i = 0; i < count; i++)
            {
                int j = i + 1;
                mylist.Add("TestCase " + j);
            }
            lstboxTestCase.ItemsSource = mylist;
        }

        private void btnAddConstraint_Click(object sender, RoutedEventArgs e)
        {
            constraints.Add(txtboxConstraint.Text);
            lstboxConstraint.ItemsSource = constraints;
        }

        private void btnConstraintRemove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string s = btn.DataContext as string;
            int index = constraints.IndexOf(s);

            constraints.RemoveAt(index);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
using System.IO;

namespace TestCaseGenerator
{
    /// <summary>
    /// Interaction logic for Question.xaml
    /// </summary>
    public partial class Question : Page
    {
        public static string connectionString = "Properties.Settings.Default.database";

        ObservableCollection<string> alternatelst = new ObservableCollection<string>();

        ObservableCollection<string> challangelst = new ObservableCollection<string>();

        int topicId ;
        ObservableCollection<String> sample = new ObservableCollection<string>();
        public Question()
        {
            InitializeComponent();
            sample.Add("Sample Input");
            string crs = "Foundations of Programming and Problem Solving";
            string topic = "Prime Numbers and Factors";

           topicId= GetTopicIdByTopicAndCourseName(crs, topic);

            lstboxSampleInput.ItemsSource = sample;
        }

       

        private void btnAddSampleInput_Click(object sender, RoutedEventArgs e)
        {
            sample.Add("Sample Input");
        }

        private void btnDeleteSampleInput_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string data = b.DataContext as string;
            int index= sample.IndexOf(data);

            sample.RemoveAt(index);
        }


        private int GetTopicIdByTopicAndCourseName(string courseName, string topic)
        {
            int id=0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
     
                    cmd.CommandText = "select TopicId from CourseTopics where Topic='"+topic+"' and CourseName='"+courseName+"'";
                    var res = cmd.ExecuteScalar();
                    id = int.Parse(res.ToString());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return id;
        }

        private int GetQuestionIdByTopicAndQuestion(string courseName, string topic, string Ques)
        {

            int topicId =GetTopicIdByTopicAndCourseName(courseName,topic);
            int id = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select Qid from Question where TopicId="+topicId+" and QuestionStem='"+Ques+"'";
                    var res = cmd.ExecuteScalar();
                    id = int.Parse(res.ToString());

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return id;
        }

        private void InsertQuestion(string courseName, string topic, string Qstem,Status HasHint=(int)Status.NotAvailable,string hint="",Status HasChallange=(int)Status.NotAvailable,string challStem="",Status HasAlternate=(int)Status.NotAvailable,string AltStem="")
        {
            int id = GetTopicIdByTopicAndCourseName(courseName, topic);
            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into Question(TopicId,QuestionStem) values(" + id + ", '" + Qstem + "')";
                    //var res = cmd.ExecuteScalar();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertChallange(string courseName, string topic, string Ques, string stem)
        {
            int id = GetQuestionIdByTopicAndQuestion(courseName, topic, Ques);

            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into Challange(Qid, ChallangeStem) values(" + id + ", '" + stem + "')";
                    //var res = cmd.ExecuteScalar();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void InsertAlternate(string courseName, string topic, string Ques, string stem)
        {
            int id = GetQuestionIdByTopicAndQuestion(courseName, topic, Ques);

            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into Alternate(Qid, AlternateStem) values(" + id + ", '" + stem + "')";
                    //var res = cmd.ExecuteScalar();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertTestCases(string courseName, string topic, string Ques, string inputpath,string outputpath)
        {
            int id = GetQuestionIdByTopicAndQuestion(courseName, topic, Ques);
            System.IO.FileStream Ipfs = new System.IO.FileStream(inputpath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            BinaryReader br = new BinaryReader(Ipfs);
            byte[] input = br.ReadBytes((Int32)Ipfs.Length);

            System.IO.FileStream Opfs = new System.IO.FileStream(outputpath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            BinaryReader reader = new BinaryReader(Opfs);
            byte[] output = reader.ReadBytes((Int32)Opfs.Length);



            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into TestCases(Qid,Input,ExpectedOutput)values(" + id + ",'" + input + "','" + output + "')";
                    //var res = cmd.ExecuteScalar();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public byte[] FileToByteArray(string fileName)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);
            byte[] fileContent = br.ReadBytes((Int32)fs.Length);

            return fileContent;
        }

        private string ByteArrayToFile(string bPath, string fName, byte[] content)
        {
            //Save the Byte Array as File.
            string filePath = bPath + "\\" + fName;
            File.WriteAllBytes(filePath, content);

            MessageBox.Show("File Generated");

            return filePath;
        }

        private void btnHint_Click(object sender, RoutedEventArgs e)
        {
            txtboxHint.Visibility = Visibility.Visible;
            btnHint.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            gridCursor.Margin = new Thickness((10 + 335 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    stkpnlSample.Visibility = Visibility.Visible;
                    stkpnlAlternate.Visibility = Visibility.Collapsed;
                    stkpnlChallange.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    stkpnlSample.Visibility = Visibility.Collapsed;
                    stkpnlAlternate.Visibility = Visibility.Collapsed;
                    stkpnlChallange.Visibility = Visibility.Visible;
                    break;
                case 2:
                    stkpnlSample.Visibility = Visibility.Collapsed;
                    stkpnlAlternate.Visibility = Visibility.Visible;
                    stkpnlChallange.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnChallangeRemove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string s = btn.DataContext as string;
            int index = challangelst.IndexOf(s);

            challangelst.RemoveAt(index);
        }

        private void btnAddChallange_Click(object sender, RoutedEventArgs e)
        {
            challangelst.Add(txtboxChallangeStem.Text);
            lstboxChallange.ItemsSource = challangelst;
            txtboxChallangeStem.Text = "";
            txtboxChallangeStem.Focus();
        }

        private void btnAddAlternate_Click(object sender, RoutedEventArgs e)
        {
            alternatelst.Add(txtboxAlternateStem.Text);
            lstboxChallange.ItemsSource = alternatelst;
            txtboxAlternateStem.Text = "";
            txtboxAlternateStem.Focus();
        }
    }
}

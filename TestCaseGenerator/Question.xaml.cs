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
        public static string connectionString = @"Data Source=DESKTOP-JI48AUG\SQLEXPRESS;Initial Catalog=Coherence;Integrated Security=True";

        

        

        int topicId ;
        int btnId = 0;

        ObservableCollection<String> sample = new ObservableCollection<string>();
        public Question()
        {
            InitializeComponent();
            sample.Add("Sample Input");
            string crs = "Foundations of Programming and Problem Solving";
            string topic = "Prime Numbers and Factors";

            topicId = GetTopicIdByTopicAndCourseName(crs, topic);

            lstboxSampleInput.ItemsSource = sample;
        }
        public Question(int id)
        {
            InitializeComponent();
            sample.Add("Sample Input");

            topicId = id;

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

                    cmd.CommandText = "select TopicId from CourseTopics where Topic='" + topic + "' and CourseName='" + courseName + "'";
                    var res = cmd.ExecuteScalar();
                    id = int.Parse(res.ToString());

                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return id;
        }

        private int GetQuestionIdByTopicAndQuestion(int topicId, string Ques)
        {

           // int topicId =GetTopicIdByTopicAndCourseName(courseName,topic);
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

        private void InsertQuestion(int Qid,int topicId, string Qstem,int HasHint=(int)Status.NotAvailable,string hint="",int HasChallange=(int)Status.NotAvailable,int ChallangeId=0,int HasAlternate=(int)Status.NotAvailable,int AltId=0)
        {
            //int id = GetTopicIdByTopicAndCourseName(courseName, topic);
            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into Question (Qid,TopicId,QuestionStem,HasHint,Hint,HasChallange,ChallangeId,HasAlternate,AlternateId) values("+Qid+"," + topicId + ", '" + Qstem + "'," + HasHint + ",'" + hint + "'," + HasChallange + "," + ChallangeId + "," + HasAlternate + "," + AltId + ")";
                    //var res = cmd.ExecuteScalar();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           
        }

        private bool HasChallange(int Qid)
        {
            bool flag = false;
            int id = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select HasChallange from Question where Qid=" + Qid + " ";
                    var res = cmd.ExecuteScalar();
                    id = int.Parse(res.ToString());

                    if (id == 1)
                    {
                        flag = true;
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }

        private bool HasAlternate( int Qid)
        {
            bool flag = false;
            int id = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select HasAlternate from Question where Qid=" + Qid + " ";
                    var res = cmd.ExecuteScalar();
                    id = int.Parse(res.ToString());

                    if (id == 1)
                    {
                        flag = true;
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }

       

       

       
        private void btnHint_Click(object sender, RoutedEventArgs e)
        {
            txtboxHint.Visibility = Visibility.Visible;
            btnHint.IsEnabled = false;
        }


        //private void btnAddAlternate_Click(object sender, RoutedEventArgs e)
        //{
        //    alternatelst.Add(txtboxAlternateStem.Text);
        //    //lstboxChallange.ItemsSource = alternatelst;
        //    txtboxAlternateStem.Text = "";
        //    txtboxAlternateStem.Focus();
        //}

        private int HasChallange()
        {
            int has = 0;
            if (btnHint.IsEnabled == false)
            { has = 1;
                return has;
            }

            return has;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            QuestionModel questionModel = new QuestionModel();
            questionModel.QuestionStem = txtboxQuestion.Text;
            questionModel.HasChallange = HasChallange();
            questionModel.Hint = txtboxHint.Text;
           
            InsertQuestion(GetMaxQuestionId()+1,topicId, questionModel.QuestionStem, questionModel.IsHint, questionModel.Hint);

        }

        private int GetMaxQuestionId()
        {
            int id = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select count(*) from Question";
                    var res1 = cmd.ExecuteScalar();
                    if (Convert.ToInt32(res1) > 0)
                    {
                        cmd.CommandText = "select max(Qid) from Question";
                        var res = cmd.ExecuteScalar();
                        id = Convert.ToInt32(res);
                    }


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return id;
        }

       

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtboxQuestion.Text = "";
        }

    }
}

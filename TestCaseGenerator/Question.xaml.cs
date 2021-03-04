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
        public Question(string crs, string topic)
        {
            InitializeComponent();
            sample.Add("Sample Input");

            topicId = GetTopicIdByTopicAndCourseName(crs, topic);

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

        private void UpdateChallange(int Qid,int HasChallange, int ChallangeId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "update Question set HasChallange=" + HasChallange + " , ChallangeId=" + ChallangeId + " where Qid=" + Qid + "";
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

        private void UpdateAlternate(int Qid, int HasAlternate, int AlternateId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "update Question set HasAlternate=" + HasAlternate + " , AlternateId=" + AlternateId + " where Qid=" + Qid + "";
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

        

        private void InsertAlternate(int QuesId, string Ques, string stem)
        {
            //int id = GetQuestionIdByTopicAndQuestion(courseName, topic, Ques);

            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into Alternate(Qid, AlternateStem) values(" + QuesId + ", '" + stem + "')";
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

        private void InsertTestCases(int Qid, string inputpath,string outputpath)
        {
           // int id = GetQuestionIdByTopicAndQuestion(courseName, topic, Ques);
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

                    cmd.CommandText = "insert into TestCases(Qid,Input,ExpectedOutput)values(" + Qid + ",'" + input + "','" + output + "')";
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
            btnId = index;

            gridCursor.Margin = new Thickness((10 + 335 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    
                    stkpnlSample.Visibility = Visibility.Visible;
                    //stkpnlAlternate.Visibility = Visibility.Collapsed;
                    //stkpnlChallange.Visibility = Visibility.Collapsed;

                    stkpnlEnterQues.Visibility = Visibility.Visible;
                    txtblckDisplayQues.Text = txtboxQuestion.Text;
                    //if(hasHint(Qid))
                    txtblckDisplayHint.Text = txtboxHint.Text;
                    stkpnlDisplayQuestion.Visibility = Visibility.Collapsed;

                    break;
                case 1:
                    //var obj = new Challange(GetQuestionIdByTopicAndQuestion(topicId, txtboxQuestion.Text));
                    //NavigationService.GetNavigationService(this).Navigate(obj);
                    QuesFrame.NavigationService.Navigate(new Challange(GetQuestionIdByTopicAndQuestion(topicId, txtboxQuestion.Text)));
                    break;
                case 2:
                    stkpnlSample.Visibility = Visibility.Collapsed;
                    //stkpnlAlternate.Visibility = Visibility.Visible;
                    //stkpnlChallange.Visibility = Visibility.Collapsed;

                    stkpnlEnterQues.Visibility = Visibility.Collapsed;
                    stkpnlDisplayQuestion.Visibility = Visibility.Visible;

                    break;
            }
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

            switch (btnId)
            {
                case 0:
                        InsertQuestion(GetMaxQuestionId()+1,topicId, questionModel.QuestionStem, questionModel.IsHint, questionModel.Hint);
                        //insert sample testcases
                    break;
                case 1:

                    questionModel.Qid = GetQuestionIdByTopicAndQuestion(topicId, txtblckDisplayQues.Text);
                    //for (int i = 0; i < challangelst.Count; i++)
                    //{
                    //    InsertChallange(questionModel.Qid, challangelst[i]);

                    //    if (HasChallange(questionModel.Qid))
                    //    {
                    //        InsertQuestion(questionModel.Qid, questionModel.TopicId, questionModel.QuestionStem, questionModel.IsHint, questionModel.Hint, questionModel.HasChallange, GetMaxChallangeId());
                    //    }
                    //    else
                    //    {
                    //        UpdateChallange(questionModel.Qid, (int)Status.Available, GetMaxChallangeId());
                    //    }
                    //}
                    break;
                case 2:
                    break;
            }
            
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

        private int GetMaxChallangeId()
        {
            int id = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select count(*) from Challange";
                    var res1 = cmd.ExecuteScalar();
                    if (Convert.ToInt32(res1) > 0)
                    {
                        cmd.CommandText = "select max(ChallangeId) from Challange";
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

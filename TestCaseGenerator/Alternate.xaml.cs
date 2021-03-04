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
    /// Interaction logic for Alternate.xaml
    /// </summary>
    public partial class Alternate : Page
    {
        int quesId = 0;
        ObservableCollection<string> alternatelst = new ObservableCollection<string>();
        public Alternate()
        {
            InitializeComponent();
        }

        public Alternate(int qid)
        {
            InitializeComponent();
            quesId = qid;

            txtblckDisplayQues.Text = GetQuestionByQid(qid);

            if (HasHint(qid))
            {
                txtblckDisplayHint.Text = GetHintByQid(qid);
            }
            else
            {
                txtblckDisplayHint.Visibility = Visibility.Collapsed;
            }
        }

        private void btnChallangeRemove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string s = btn.DataContext as string;
            int index =alternatelst.IndexOf(s);

            alternatelst.RemoveAt(index);
        }

        private void btnAddAlternate_Click(object sender, RoutedEventArgs e)
        {
            alternatelst.Add(txtboxAlternateStem.Text);
            lstboxAlternate.ItemsSource = alternatelst;
            txtboxAlternateStem.Text = "";
            txtboxAlternateStem.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            QuestionModel questionModel = new QuestionModel();
            questionModel.Qid = quesId;

            for (int i = 0; i < alternatelst.Count; i++)
            {
                InsertChallange(questionModel.Qid, alternatelst[i]);
            }
        }

        private string GetQuestionByQid(int qid)
        {
            string s = "";

            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select QuestionStem from Question where Qid=" + qid + "";
                    var res = cmd.ExecuteScalar();
                    s = res.ToString();
                    //int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return s;
        }

        private bool HasHint(int qid)
        {
            bool flag = false;

            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select HasHint from Question where Qid=" + qid + "";
                    var res = cmd.ExecuteScalar();
                    int ans = int.Parse(res.ToString());

                    if (ans == 1)
                    { flag = true; }
                    //int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return flag;
        }

        private string GetHintByQid(int qid)
        {
            string s = "";

            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select Hint from Question where Qid=" + qid + " and HasHint=" + 1 + "";
                    var res = cmd.ExecuteScalar();
                    s = res.ToString();
                    //int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return s;
        }

        private void InsertChallange(int Qid, string stem)
        {
            //int id = GetQuestionIdByTopicAndQuestion(courseName, topic, Ques);

            try
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.database))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "insert into Alternate(Qid,AlternateStem) values("+Qid+",'"+stem+"')";
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
    }
}

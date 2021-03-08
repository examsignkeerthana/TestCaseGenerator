﻿using System;
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
using System.Data.SqlClient;
using System.Windows.Navigation;

namespace TestCaseGenerator
{
    /// <summary>
    /// Interaction logic for LandingWindow.xaml
    /// </summary>
    public partial class LandingWindow : Window
    {
        int topicId = 0;

        public static string connectionString = @"Data Source=DESKTOP-JI48AUG\SQLEXPRESS;Initial Catalog=Coherence;Integrated Security=True";
        public LandingWindow()
        {
            InitializeComponent();
        }

        public LandingWindow(string crs,string topic)
        {
            InitializeComponent();

            topicId = GetTopicIdByTopicAndCourseName(crs, topic);
        }

        private int GetTopicIdByTopicAndCourseName(string courseName, string topic)
        {
            int id = 0;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                int index = int.Parse(((Button)e.Source).Uid);
                

                gridCursor.Margin = new Thickness((10 + 335 * index), 0, 0, 0);

                switch (index)
                {
                    case 0:

                    var obj = new Question(topicId);
                    NavigationService.GetNavigationService(this).Navigate(obj);

                    break;
                    case 1:
                    var obj1 = new Challange(topicId);
                    NavigationService.GetNavigationService(this).Navigate(obj1);


                    //var obj = new Challange(GetQuestionIdByTopicAndQuestion(topicId, txtboxQuestion.Text));
                    //NavigationService.GetNavigationService(this).Navigate(obj);
                    //QuesFrame.NavigationService.Navigate(new Challange(GetQuestionIdByTopicAndQuestion(topicId, txtboxQuestion.Text)));
                    break;
                    case 2:

                    var obj2 = new Alternate(topicId);
                    NavigationService.GetNavigationService(this).Navigate(obj2);

                    break;
                
            }
        }
    }
}

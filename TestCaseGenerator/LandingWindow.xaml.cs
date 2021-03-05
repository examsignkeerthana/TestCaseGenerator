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

namespace TestCaseGenerator
{
    /// <summary>
    /// Interaction logic for LandingWindow.xaml
    /// </summary>
    public partial class LandingWindow : Window
    {
        public LandingWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                int index = int.Parse(((Button)e.Source).Uid);
                

                gridCursor.Margin = new Thickness((10 + 335 * index), 0, 0, 0);

                switch (index)
                {
                    case 0:
                        
                        
                        break;
                    case 1:

                    var obj = new Question(cmboboxCourse.SelectedItem.ToString(), cmboboxTopic.SelectedItem.ToString());
                    NavigationService.GetNavigationService(this).Navigate(obj);

                    //var obj = new Challange(GetQuestionIdByTopicAndQuestion(topicId, txtboxQuestion.Text));
                    //NavigationService.GetNavigationService(this).Navigate(obj);
                    //QuesFrame.NavigationService.Navigate(new Challange(GetQuestionIdByTopicAndQuestion(topicId, txtboxQuestion.Text)));
                    break;
                    case 2:
                        
                        break;
                
            }
        }
    }
}

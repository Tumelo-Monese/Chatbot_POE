using System.Windows;

namespace Chatbot_POE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToTasks_Click(object sender, RoutedEventArgs e)
        {
            Tasks tasksWindow = new Tasks();
            tasksWindow.ShowDialog();
        }

        private void GoToQuiz_Click(object sender, RoutedEventArgs e)
        {
            Quiz quizWindow = new Quiz();
            quizWindow.ShowDialog();
        }

        private void GoToLog_Click(object sender, RoutedEventArgs e)
        {
            ActivityLog logWindow = new ActivityLog();
            logWindow.ShowDialog();  
        }




        private void SubmitInputButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

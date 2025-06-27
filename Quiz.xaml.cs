using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Chatbot_POE
{
    public partial class Quiz : Window
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex = 0;
        private int score = 0; // Track score

        public Quiz()
        {
            InitializeComponent();
            ActivityLog.AddEntry("User started the quiz.");
            LoadQuestions();
            DisplayQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion("What does 'phishing' refer to?",
                    new[] { "A cooking method", "A cyber attack via email", "A password strategy", "A hardware tool" }, 1),

                new QuizQuestion("Which of the following is a strong password?",
                    new[] { "123456", "password", "Pa$$w0rd123!", "qwerty" }, 2),

                new QuizQuestion("Which protocol is used to securely browse websites?",
                    new[] { "HTTP", "HTTPS", "FTP", "SMTP" }, 1),

                new QuizQuestion("What should you do if you receive a suspicious email?",
                    new[] { "Click it", "Ignore it", "Forward it", "Report or delete it" }, 3),

                new QuizQuestion("What is two-factor authentication (2FA)?",
                    new[] { "A backup password", "A firewall", "Extra login security", "Antivirus" }, 2),

                new QuizQuestion("Which of these is malware?",
                    new[] { "Firewall", "VPN", "Antivirus", "Ransomware" }, 3),

                new QuizQuestion("Why should you avoid public Wi-Fi for banking?",
                    new[] { "Expensive", "Slow", "Hackable", "Battery saver" }, 2),

                new QuizQuestion("Which is a phishing sign?",
                    new[] { "HTTPS", "Correct grammar", "Weird link & urgency", "Padlock icon" }, 2),

                new QuizQuestion("What is the safest device for online banking?",
                    new[] { "Public PC", "Library PC", "Secured personal device", "strangers tablet" }, 2),

                new QuizQuestion("what is the purpose of antivirus software?",
                    new[] { "Backup", "Speed boost", "Popup blocker", "Detect & remove malware" }, 3)
            };
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                QuestionTextBlock.Text = "Quiz Completed!";
                FeedbackTextBlock.Text = $"Your score: {score}/{questions.Count}";
                
                ActivityLog.AddEntry($"User completed the quiz. Score: {score}/{questions.Count}");
                return;
            }


            QuizQuestion q = questions[currentQuestionIndex];
            QuestionTextBlock.Text = q.Question;
            OptionA.Content = q.Options[0];
            OptionB.Content = q.Options[1];
            OptionC.Content = q.Options[2];
            OptionD.Content = q.Options[3];

            OptionA.IsChecked = false;
            OptionB.IsChecked = false;
            OptionC.IsChecked = false;
            OptionD.IsChecked = false;
            FeedbackTextBlock.Text = "";
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = -1;
            if (OptionA.IsChecked == true) selectedIndex = 0;
            else if (OptionB.IsChecked == true) selectedIndex = 1;
            else if (OptionC.IsChecked == true) selectedIndex = 2;
            else if (OptionD.IsChecked == true) selectedIndex = 3;

            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select an answer.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool correct = selectedIndex == questions[currentQuestionIndex].CorrectAnswerIndex;
            ActivityLog.AddEntry($"Answered Question {currentQuestionIndex + 1}: {(correct ? "Correct" : "Incorrect")}");

            if (selectedIndex == questions[currentQuestionIndex].CorrectAnswerIndex)
            {
                score++;
                FeedbackTextBlock.Text = "Correct!";
                FeedbackTextBlock.Foreground = Brushes.Green;
            }
            else
            {
                FeedbackTextBlock.Text = "Incorrect.";
                FeedbackTextBlock.Foreground = Brushes.Red;
            }

            currentQuestionIndex++;

            Dispatcher.InvokeAsync(async () =>
            {
                await System.Threading.Tasks.Task.Delay(1000);
                DisplayQuestion();
            });
        }
    }


    public class QuizQuestion
    {
        public string Question { get; }
        public string[] Options { get; }
        public int CorrectAnswerIndex { get; }

        public QuizQuestion(string question, string[] options, int correctAnswerIndex)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}

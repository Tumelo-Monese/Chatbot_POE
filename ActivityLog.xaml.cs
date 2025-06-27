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

namespace Chatbot_POE
{
    /// <summary>
    /// Interaction logic for ActivityLog.xaml
    /// </summary>
    public partial class ActivityLog : Window
    {
        public static List<string> ActivityEntries { get; set; } = new List<string>();

        public ActivityLog()
        {
            InitializeComponent();
            LoadActivities();
        }

        private void LoadActivities()
        {
            ActivityListBox.Items.Clear();
            foreach (string activity in ActivityEntries)
            {
                ActivityListBox.Items.Add(activity);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static void AddEntry(string entry)
        {
            ActivityEntries.Add($"{DateTime.Now:G} - {entry}");
        }

    }
}


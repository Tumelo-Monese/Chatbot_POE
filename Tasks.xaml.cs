using System;
using System.Collections.Generic;
using System.Windows;

namespace Chatbot_POE
{
    public partial class Tasks : Window
    {
        private List<TaskItem> taskList = new List<TaskItem>();

        public Tasks()
        {
            InitializeComponent();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string description = TaskDescriptionTextBox.Text.Trim();
            DateTime? reminder = TaskReminderDatePicker.SelectedDate;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TaskItem newTask = new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminder,
                IsCompleted = false
            };

            taskList.Add(newTask);
            ActivityLog.AddEntry($"New task added: {title}");
            RefreshTaskList();

            // Clear inputs
            TaskTitleTextBox.Clear();
            TaskDescriptionTextBox.Clear();
            TaskReminderDatePicker.SelectedDate = null;
        }

        private void RefreshTaskList()
        {
            TasksListBox.Items.Clear();
            foreach (var task in taskList)
            {
                TasksListBox.Items.Add(task.ToString());
            }
        }

        private void MarkCompletedButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = TasksListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < taskList.Count)
            {
                taskList[selectedIndex].IsCompleted = true;
                ActivityLog.AddEntry($"Task marked completed: {taskList[selectedIndex].Title}");
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Please select a task to mark as completed.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = TasksListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < taskList.Count)
            {
                string taskTitle = taskList[selectedIndex].Title;
                taskList.RemoveAt(selectedIndex);
                ActivityLog.AddEntry($"Task deleted: {taskTitle}");
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "[✓] " : "[ ] ";
            string reminderStr = ReminderDate.HasValue ? $" (Remind on {ReminderDate.Value.ToShortDateString()})" : "";
            return $"{status}{Title} - {Description}{reminderStr}";
        }
    }
}

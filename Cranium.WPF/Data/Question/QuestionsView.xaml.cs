using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cranium.WPF.Data.Question
{
    public partial class QuestionsView
    {
        public QuestionsView()
        {
            InitializeComponent();
        }

        private void OnListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is ListBox l))
                return;

            if (!(l.SelectedItem is Answer.Answer selectedItem))
                return;

            switch (e.Key)
            {
                case Key.Delete:
                    (l.ItemsSource as IList<Answer.Answer>)?.Remove(selectedItem);
                    break;
            }
        }

        private void OnAddAnswerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button b))
                return;

            if (!(b.CommandParameter is IList<Answer.Answer> l))
                return;

            l.Add(new Answer.Answer());
        }
    }
}
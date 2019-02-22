using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cranium.Data.RestClient.Models;

namespace Cranium.WPF.Views.Data
{
    public partial class Questions
    {
        public Questions()
        {
            InitializeComponent();
        }

        private void OnListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is ListBox l))
                return;

            if (!(l.SelectedItem is Answer selectedItem))
                return;

            switch (e.Key)
            {
                case Key.Delete:
                    (l.ItemsSource as IList<Answer>)?.Remove(selectedItem);
                    break;
            }
        }

        private void OnAddAnswerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button b))
                return;

            if (!(b.CommandParameter is IList<Answer> l))
                return;

            l.Add(new Answer());
        }
    }
}
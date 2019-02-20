using System.Windows;
using System.Windows.Controls;
using Cranium.Data.RestClient.Models;

namespace Cranium.WPF.Views.Controls
{
    public class AnswerEditingControl : Control
    {
        public static readonly DependencyProperty AnswerProperty = DependencyProperty.Register(
            nameof(Answer),
            typeof(Answer),
            typeof(AnswerEditingControl),
            new PropertyMetadata(default(Answer)));

        public Answer Answer
        {
            get => (Answer) GetValue(AnswerProperty);
            set => SetValue(AnswerProperty, value);
        }
    }
}
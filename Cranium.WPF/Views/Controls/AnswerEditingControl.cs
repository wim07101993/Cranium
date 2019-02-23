using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.Views.Controls
{
    public class AnswerEditingControl : Control
    {
        public static readonly DependencyProperty AnswerProperty = DependencyProperty.Register(
            nameof(Answer),
            typeof(Answer),
            typeof(AnswerEditingControl),
            new PropertyMetadata(default(Answer)));

        public static readonly DependencyProperty PickFileCommandProperty = DependencyProperty.Register(
            nameof(PickFileCommand),
            typeof(ICommand),
            typeof(AnswerEditingControl),
            new PropertyMetadata(default(ICommand)));


        static AnswerEditingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnswerEditingControl),
                new FrameworkPropertyMetadata(typeof(AnswerEditingControl)));
        }


        public Strings Strings => App.Strings;

        public Answer Answer
        {
            get => (Answer) GetValue(AnswerProperty);
            set => SetValue(AnswerProperty, value);
        }

        public ICommand PickFileCommand
        {
            get => (ICommand)GetValue(PickFileCommandProperty);
            set => SetValue(PickFileCommandProperty, value);
        }
    }
}
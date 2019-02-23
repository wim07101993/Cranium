using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.Views.Controls
{
    public class CategoryEditingControl : Control
    {
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(
            nameof(Category),
            typeof(Category),
            typeof(CategoryEditingControl),
            new PropertyMetadata(default(Category)));

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register(
            nameof(DeleteCommand),
            typeof(ICommand),
            typeof(CategoryEditingControl),
            new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty ChangeImageCommandProperty = DependencyProperty.Register(
            nameof(ChangeImageCommand),
            typeof(ICommand),
            typeof(CategoryEditingControl),
            new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            nameof(Image), 
            typeof(ImageSource), 
            typeof(CategoryEditingControl), 
            new PropertyMetadata(default(ImageSource)));

        public static readonly DependencyProperty StringsProperty = DependencyProperty.Register(
            nameof(Strings),
            typeof(Strings),
            typeof(CategoryEditingControl),
            new PropertyMetadata(default(Strings)));


        static CategoryEditingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CategoryEditingControl),
                new FrameworkPropertyMetadata(typeof(CategoryEditingControl)));
        }


        public Category Category
        {
            get => (Category) GetValue(CategoryProperty);
            set => SetValue(CategoryProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand) GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public ICommand ChangeImageCommand
        {
            get => (ICommand)GetValue(ChangeImageCommandProperty);
            set => SetValue(ChangeImageCommandProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public Strings Strings
        {
            get => (Strings) GetValue(StringsProperty);
            set => SetValue(StringsProperty, value);
        }
    }
}
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Cranium.WPF.Models;

namespace Cranium.WPF.Views.Controls
{
    public class CategoryEditingControl : AModelEditingControl<Category>
    {
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


        static CategoryEditingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CategoryEditingControl),
                new FrameworkPropertyMetadata(typeof(CategoryEditingControl)));
        }


        public ICommand ChangeImageCommand
        {
            get => (ICommand) GetValue(ChangeImageCommandProperty);
            set => SetValue(ChangeImageCommandProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
    }
}
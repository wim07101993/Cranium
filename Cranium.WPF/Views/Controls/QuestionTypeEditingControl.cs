using System.Collections.Generic;
using System.Windows;
using Cranium.WPF.Models;

namespace Cranium.WPF.Views.Controls
{
    public class QuestionTypeEditingControl : AModelEditingControl<QuestionType>
    {
        public static readonly DependencyProperty CategoriesProperty = DependencyProperty.Register(
            nameof(Categories), 
            typeof(IList<Category>), 
            typeof(QuestionTypeEditingControl),
            new PropertyMetadata(default(IList<Category>)));


        static QuestionTypeEditingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QuestionTypeEditingControl),
                new FrameworkPropertyMetadata(typeof(QuestionTypeEditingControl)));
        }


        public IList<Category> Categories
        {
            get => (IList<Category>)GetValue(CategoriesProperty);
            set => SetValue(CategoriesProperty, value);
        }
    }
}
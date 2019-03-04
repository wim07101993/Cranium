using Cranium.WPF.Data.Category;
using Cranium.WPF.Helpers.Controls;
using System.Collections.Generic;
using System.Windows;

namespace Cranium.WPF.Game.Player
{
    public class PlayerEditingControl : AModelEditingControl<Player>
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty CategoriesProperty = DependencyProperty.Register(
            nameof(Categories),
            typeof(IList<Category>),
            typeof(PlayerEditingControl),
            new PropertyMetadata(default(IList<Category>)));

        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(
            nameof(Category),
            typeof(Category),
            typeof(PlayerEditingControl),
            new PropertyMetadata(default(Category)));

        public static readonly DependencyProperty MoveBackwardsProperty = DependencyProperty.Register(
           nameof(MoveBackwards),
           typeof(bool),
           typeof(PlayerEditingControl),
           new PropertyMetadata(default(bool)));

        #endregion DEPENDENCY PROPERTIES


        #region FIELDS

        #endregion FIELDS


        #region CONSTRUCTORS

        static PlayerEditingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlayerEditingControl),
                new FrameworkPropertyMetadata(typeof(PlayerEditingControl)));
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public IList<Category> Categories
        {
            get => (IList<Category>)GetValue(CategoriesProperty);
            set => SetValue(CategoriesProperty, value);
        }

        public Category Category
        {
            get => (Category)GetValue(CategoriesProperty);
            set => SetValue(CategoriesProperty, value);
        }

        public bool MoveBackwards
        {
            get => (bool)GetValue(MoveBackwardsProperty);
            set => SetValue(MoveBackwardsProperty, value);
        }

        #endregion PROPERTIES


        #region METHDOS

        #endregion METHODS
    }
}

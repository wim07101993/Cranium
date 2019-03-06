using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Cranium.WPF.Data.Category;

namespace Cranium.WPF.Game.Tile
{
    public class TileControl : Control
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(
            nameof(Category),
            typeof(Category),
            typeof(TileControl),
            new PropertyMetadata(default(Category)));

        public static readonly DependencyProperty PlayersProperty = DependencyProperty.Register(
            nameof(Players),
            typeof(IList<Player.Player>),
            typeof(TileControl),
            new PropertyMetadata(default(IList<Player.Player>)));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(TileControl),
            new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty PlayerSizeProperty = DependencyProperty.Register(
            nameof(PlayerSize),
            typeof(double),
            typeof(TileControl),
            new PropertyMetadata(50.0));

        public static readonly DependencyProperty PlayerMarginProperty = DependencyProperty.Register(
            nameof(PlayerMargin),
            typeof(Thickness),
            typeof(TileControl),
            new PropertyMetadata(new Thickness(8)));

        #endregion DEPENEDENCY PROPERTIES


        #region FIELDS

        private const string ElementPlayers = "PartPlayers";

        private ItemsControl _itemsControl;

        #endregion FIELDS


        #region CONSTRUCTORS

        static TileControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TileControl),
                new FrameworkPropertyMetadata(typeof(TileControl)));
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public Category Category
        {
            get => (Category) GetValue(CategoryProperty);
            set => SetValue(CategoryProperty, value);
        }

        public IList<Player.Player> Players
        {
            get => (IList<Player.Player>) GetValue(PlayersProperty);
            set => SetValue(PlayersProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public double PlayerSize
        {
            get => (double)GetValue(PlayerSizeProperty);
            set => SetValue(PlayerSizeProperty, value);
        }

        public Thickness PlayerMargin
        {
            get => (Thickness)GetValue(PlayerMarginProperty);
            set => SetValue(PlayerMarginProperty, value);
        }

        #endregion PROPERTIES


        #region METHODS

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _itemsControl = GetTemplateChild(ElementPlayers) as ItemsControl;

            if (_itemsControl == null)
                throw new InvalidOperationException($"You have missed to specify {ElementPlayers} in your template");
        }
        
        #endregion METHODS
    }
}
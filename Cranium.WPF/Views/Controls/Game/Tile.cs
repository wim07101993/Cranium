using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Game;

namespace Cranium.WPF.Views.Controls.Game
{
    public class Tile : Control
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(
            nameof(Category),
            typeof(Category),
            typeof(Tile),
            new PropertyMetadata(default(Category)));

        public static readonly DependencyProperty PlayersProperty = DependencyProperty.Register(
            nameof(Players),
            typeof(IList<Player>),
            typeof(Tile),
            new PropertyMetadata(default(IList<Player>)));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(Tile),
            new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty PlayerSizeProperty = DependencyProperty.Register(
            nameof(PlayerSize),
            typeof(double),
            typeof(Tile),
            new PropertyMetadata(50.0));
        
        #endregion DEPENEDENCY PROPERTIES


        #region FIELDS

        private const string ElementPlayers = "PartPlayers";

        private ItemsControl _itemsControl;

        #endregion FIELDS


        #region CONSTRUCTORS

        static Tile()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tile),
                new FrameworkPropertyMetadata(typeof(Tile)));
        }
        
        #endregion CONSTRUCTORS


        #region PROPERTIES

        public Category Category
        {
            get => (Category) GetValue(CategoryProperty);
            set => SetValue(CategoryProperty, value);
        }

        public IList<Player> Players
        {
            get => (IList<Player>) GetValue(PlayersProperty);
            set => SetValue(PlayersProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public double PlayerSize => (double) GetValue(PlayerSizeProperty);
   
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
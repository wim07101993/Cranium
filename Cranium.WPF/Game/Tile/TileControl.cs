﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Cranium.WPF.Data.Category;

namespace Cranium.WPF.Game.Tile
{
    public class TileControl : System.Windows.Controls.Control
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
            new PropertyMetadata(default(IList<Player.Player>), OnPlayersChanged));

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

        public double PlayerSize => (double) GetValue(PlayerSizeProperty);

        public Thickness PlayerMargin => (Thickness) GetValue(PlayerMarginProperty);

        #endregion PROPERTIES


        #region METHODS

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _itemsControl = GetTemplateChild(ElementPlayers) as ItemsControl;

            if (_itemsControl == null)
                throw new InvalidOperationException($"You have missed to specify {ElementPlayers} in your template");

            _itemsControl.SizeChanged += OnSizeChanged;
        }

        private static void OnPlayersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TileControl tile))
                return;

            if (e.OldValue is ObservableCollection<Player.Player> oldPlayers)
                oldPlayers.CollectionChanged -= tile.OnPlayersCollectionChanged;
            if (tile.Players is ObservableCollection<Player.Player> newPlayers)
                newPlayers.CollectionChanged += tile.OnPlayersCollectionChanged;

            tile.RecalculatePlayerSize();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => RecalculatePlayerSize();

        private void OnPlayersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            => RecalculatePlayerSize();

        private void RecalculatePlayerSize()
        {
            if (Math.Abs(ActualHeight) < 0.1 || Math.Abs(ActualWidth) < 0.1 || Players.Count == 0)
                return;

            var width = Math.Max(_itemsControl.ActualWidth, _itemsControl.ActualHeight);
            var height = Math.Min(_itemsControl.ActualWidth, _itemsControl.ActualHeight);
            var columns = Players.Count;
            var rows = 1;

            while (true)
            {
                if (width / columns >= height / rows)
                    break;

                rows++;
                columns = (int) Math.Ceiling((double) Players.Count / rows);
            }

            var playerContainerSize = Math.Min(width / columns, height / rows);
            var playerMargin = playerContainerSize / 10;
            var playerSize = playerContainerSize - playerMargin * 2;

            SetValue(PlayerSizeProperty, playerSize);
            SetValue(PlayerMarginProperty, new Thickness(playerMargin));
        }

        #endregion METHODS
    }
}
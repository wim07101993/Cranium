using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cranium.WPF.Helpers.Controls
{
    public class MusicPlayer : Control
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty PlayCommandProperty = DependencyProperty.Register(
            nameof(PlayCommand),
            typeof(ICommand),
            typeof(MusicPlayer),
            new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty PauseCommandProperty = DependencyProperty.Register(
            nameof(PauseCommand),
            typeof(ICommand),
            typeof(MusicPlayer),
            new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty StopCommandProperty = DependencyProperty.Register(
            nameof(StopCommand),
            typeof(ICommand),
            typeof(MusicPlayer),
            new PropertyMetadata(default(ICommand)));

        #endregion DEPENDENCY PROPERTIES


        #region FIELDS
        
        #endregion FIELDS

        #region CONSTRUCTOR
        
        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ICommand PlayCommand
        {
            get => (ICommand) GetValue(PlayCommandProperty);
            set => SetValue(PlayCommandProperty, value);
        }

        public ICommand PauseCommand
        {
            get => (ICommand) GetValue(PauseCommandProperty);
            set => SetValue(PauseCommandProperty, value);
        }

        public ICommand StopCommand
        {
            get => (ICommand) GetValue(StopCommandProperty);
            set => SetValue(StopCommandProperty, value);
        }
        
        #endregion PROPERTIES

        #region METHODS
        
        #endregion METHODS
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cranium.WPF.Models;

namespace Cranium.WPF.Views.Controls
{
    public class QuestionEditingControl : AModelEditingControl<Question>
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty ChangeAttachmentCommandProperty = DependencyProperty.Register(
            nameof(ChangeAttachmentCommand),
            typeof(ICommand),
            typeof(QuestionEditingControl),
            new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty QuestionTypesProperty = DependencyProperty.Register(
            nameof(QuestionTypes),
            typeof(IList<QuestionType>),
            typeof(QuestionEditingControl),
            new PropertyMetadata(default(IList<QuestionType>), OnCategoriesChanged));

        #endregion DEPENDENCY PROPERTIES


        #region FIELDS

        private const string ElementComboBox = "PartComboBox";

        private ComboBox _comboBox;
        private bool _updatingQuestionType;

        #endregion FIELDS


        #region CONSTRUCTORS

        static QuestionEditingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QuestionEditingControl),
                new FrameworkPropertyMetadata(typeof(QuestionEditingControl)));
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public ICommand ChangeAttachmentCommand
        {
            get => (ICommand) GetValue(ChangeAttachmentCommandProperty);
            set => SetValue(ChangeAttachmentCommandProperty, value);
        }

        public IList<QuestionType> QuestionTypes
        {
            get => (IList<QuestionType>) GetValue(QuestionTypesProperty);
            set => SetValue(QuestionTypesProperty, value);
        }

        #endregion PROPERTIES


        #region METHODS

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _comboBox = GetTemplateChild(ElementComboBox) as ComboBox;

            if (_comboBox == null)
                throw new InvalidOperationException($"You have missed to specify {ElementComboBox} in your template");

            _comboBox.SelectionChanged += OnComboboxSelectedItemChanged;
        }

        private void OnComboboxSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_updatingQuestionType)
                return;

            _updatingQuestionType = true;
            Model.QuestionType = _comboBox.SelectedItem as QuestionType;
            _updatingQuestionType = false;
        }

        protected override void OnModelChanged(Question oldValue, Question newValue)
        {
            if (oldValue != null)
                oldValue.PropertyChanged -= OnModelPropertyChanged;
            if (newValue != null)
                newValue.PropertyChanged += OnModelPropertyChanged;

            OnModelPropertyChanged(newValue, new PropertyChangedEventArgs(null));
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is QuestionType c))
                return;

            switch (e.PropertyName)
            {
                case null:
                case nameof(Question.QuestionType):
                    if (_updatingQuestionType || _comboBox == null)
                        break;

                    _updatingQuestionType = true;
                    _comboBox.SelectedItem = QuestionTypes?.FirstOrDefault(x => x.Id == c.Id);
                    _updatingQuestionType = false;
                    break;
            }
        }

        private static void OnCategoriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is QuestionEditingControl q))
                return;

            q._updatingQuestionType = true;
            q._comboBox.SelectedItem = q.QuestionTypes?.FirstOrDefault(x => x.Id == q.Model.QuestionType.Id);
            q._updatingQuestionType = false;
        }

        #endregion METHODS
    }
}
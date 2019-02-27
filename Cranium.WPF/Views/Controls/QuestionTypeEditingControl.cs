using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cranium.WPF.Models;

namespace Cranium.WPF.Views.Controls
{
    public class QuestionTypeEditingControl : AModelEditingControl<QuestionType>
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty CategoriesProperty = DependencyProperty.Register(
            nameof(Categories),
            typeof(IList<Category>),
            typeof(QuestionTypeEditingControl),
            new PropertyMetadata(default(IList<Category>), OnCategoriesChanged));

        #endregion DEPENDENCY PROPERTIES


        #region FIELDS

        private const string ElementComboBox = "PartComboBox";

        private ComboBox _comboBox;
        private bool _updatingCategory;

        #endregion FIELDS


        #region CONSTRUCTORS

        static QuestionTypeEditingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QuestionTypeEditingControl),
                new FrameworkPropertyMetadata(typeof(QuestionTypeEditingControl)));
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public IList<Category> Categories
        {
            get => (IList<Category>) GetValue(CategoriesProperty);
            set => SetValue(CategoriesProperty, value);
        }

        #endregion PROPERTIES


        #region METHDOS

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
            if (_updatingCategory)
                return;

            _updatingCategory = true;
            Model.Category = _comboBox.SelectedItem as Category;
            _updatingCategory = false;
        }

        protected override void OnModelChanged(QuestionType oldValue, QuestionType newValue)
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
                case nameof(QuestionType.Category):
                    if (_updatingCategory || _comboBox == null)
                        break;

                    _updatingCategory = true;
                    _comboBox.SelectedItem = Categories?.FirstOrDefault(x => x.Id == c.Id);
                    _updatingCategory = false;
                    break;
            }
        }

        private static void OnCategoriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is QuestionTypeEditingControl q))
                return;

            q._updatingCategory = true;
            q._comboBox.SelectedItem = q.Categories?.FirstOrDefault(x => x.Id == q.Model?.Category?.Id);
            q._updatingCategory = false;
        }

        #endregion METHODS
    }
}
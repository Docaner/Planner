using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Planner.Module.Diagram.Models
{
    public class BindingToReadOnlyPropertyBehavior : Behavior<DependencyObject>
    {
        public PropertyPath Property { get => binding.Path; set => binding.Path = value; }

        private readonly Binding binding = new Binding("") { Mode = BindingMode.OneWay };

        protected override void OnAttached()
        {
            binding.Source = AssociatedObject;
            BindingOperations.SetBinding(this, SourceProperty, binding);
        }

        /// <summary>Приватное свойство для получения данных 
        /// по заданной привязке</summary>
        private object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(BindingToReadOnlyPropertyBehavior), new PropertyMetadata(null, propertyChangedCallback));

        /// <summary>Метод обратного вызова для передачи полученного значения 
        /// в целевую привязку</summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((BindingToReadOnlyPropertyBehavior)d).Target = e.NewValue;

        /// <summary>Целевой объект куда должно передаваться значение</summary>
        public object Target
        {
            get { return (object)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(BindingToReadOnlyPropertyBehavior), new PropertyMetadata(null));

    }
}

using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Planner.Module.Diagram.Models
{
    public class Agregator : BindableBase
    {
        /// <summary>
        /// Название агрегатора
        /// </summary>
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        /// <summary>
        /// Высота агрегатора
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                if (value < 0) return;
                SetProperty(ref _height, value);
                UpdateHeightMeltings();
            }
        }
        private double _height;

        public double ActualHeight
        {
            get => _actualHeight;
            set
            {
                SetProperty(ref _actualHeight, value);
                Height = 0.9 * value;
            }
        }
        private double _actualHeight;

        public double Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
                UpdateHeightMeltings();
            }
        }
        private double _width;

        /// <summary>
        /// Коллекция плавок
        /// </summary>
        public ObservableCollection<Melting> Meltings 
        { 
            get => _meltings;
            private set
            {
                if(Meltings != null) Meltings.CollectionChanged -= Meltings_CollectionChanged;
                SetProperty(ref _meltings, value);
                Meltings.CollectionChanged += Meltings_CollectionChanged;
                SubscribeMeltings();
            }
        }
        private ObservableCollection<Melting> _meltings;

        /// <summary>
        /// Обновление высоты плавки
        /// </summary>
        /// <param name="height"></param>
        public void UpdateHeightMeltings()
        {
            double melHeight = Height - 4;
            foreach (var melting in Meltings)
                melting.Height = melHeight;
        }

        void SubscribeMeltings()
        {
            foreach (Melting m in Meltings)
                SubscribeMelting(m);
        }

        void SubscribeMelting(Melting m)
        {
            m.EventMouseLeftButtonDown += Mouse.SetFocus;
            m.EventMouseLeftButtonUp += Mouse.RemoveFocus;
            m.EventMouseLeave += Mouse.RemoveFocus;
        }

        void UnsubscribeMelting(Melting m)
        {
            m.EventMouseLeftButtonDown -= Mouse.SetFocus;
            m.EventMouseLeftButtonUp -= Mouse.RemoveFocus;
            m.EventMouseLeave -= Mouse.RemoveFocus;
        }

        private void Meltings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach (Melting m in e.NewItems)
                    SubscribeMelting(m);
            }

            if(e.OldItems != null)
            {
                foreach (Melting m in e.OldItems)
                    UnsubscribeMelting(m);
            }
        }

        public MouseMelting Mouse
        {
            get => _mouse;
            set => SetProperty(ref _mouse, value);
        }
        private MouseMelting _mouse;


        public Agregator(string name, ObservableCollection<Melting> meltings)
        {
            Mouse = new MouseMelting();

            Name = name;
            Meltings = meltings;
        }
    }
}

using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
        public ObservableCollection<Melting> Meltings { get => _meltings; private set => SetProperty(ref _meltings, value); }
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

        public Agregator(string name, ObservableCollection<Melting> meltings)
        {
            Name = name;
            Meltings = meltings;
        }
    }
}

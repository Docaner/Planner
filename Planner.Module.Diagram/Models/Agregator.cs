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
                SetProperty(ref _height, value);
                UpdateHeightMeltings();
            }
        }
        private double _height;

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
            double melHeight = Height * 0.9;
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

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
        /// Коллекция плавок
        /// </summary>
        public ObservableCollection<Melting> Meltings { get => _meltings; private set => SetProperty(ref _meltings, value); }
        private ObservableCollection<Melting> _meltings;

        public Agregator(string name, ObservableCollection<Melting> meltings)
        {
            Name = name;
            Meltings = meltings;
        }
    }
}

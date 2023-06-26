using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class Agregator:BindableBase
    {
        ObservableCollection<Plavki> plavkis;
        private string _name;

        private int _id;
        public string Name
        {
            get => _name;set => SetProperty(ref _name, value);
        }
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public ObservableCollection<Plavki> Plavkis
        {
            get => plavkis;
            set => SetProperty(ref plavkis, value);
        }
        public Agregator(int id)  
        {
            Name = "";
            Id = id;
            Plavkis = new ObservableCollection<Plavki>();
        }
       

    }
}

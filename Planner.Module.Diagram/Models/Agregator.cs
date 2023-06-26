﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class Agregator : BindableBase
    {
        ObservableCollection<Plavki> plavkis;
        private string _name;

        private int _id = 0;
        public string Name
        {
            get => _name; set => SetProperty(ref _name, value);
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
        public Agregator(string name)
        {
            Name = name;
            Id = _id++;
            Plavkis = new ObservableCollection<Plavki>();
        }
        public void Add(Plavki plavki)
        {
            plavkis.Add(plavki);
            plavki.AgregatorName = Name;
            if (Name.StartsWith("КОНВ"))
            {
                plavki.Time_Start = plavki.Konvminutestart;
                plavki.Time_End = plavki.Konvminuteend;
            }
            else if (Name.StartsWith("УДМ") || Name.StartsWith("ВАКУ"))
            {
                plavki.Time_Start = plavki.Udminutestart;
                plavki.Time_End = plavki.Udminuteend;
            }
            else if (Name.StartsWith("УПК") || Name.StartsWith("УНРС"))
            {
                plavki.Time_Start = plavki.Upkinutestart;
                plavki.Time_End = plavki.Udminuteend;
            }
        }

    }
}

using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Planner.Module.Diagram.Servises;

namespace Planner.Module.Diagram.Models
{
    public class Houre:BindableBase
    {
        int _minuted;
        int _houre;
        MinutedToHoure minutedToHoure = new MinutedToHoure();
        public int Minuted { get { return _minuted; } set =>SetProperty(ref _minuted, value); }
        public int Houres { get => _houre; set => SetProperty(ref _houre, value); }

    public Houre(int minuted) {
           
           
            
       minutedToHoure.minutetohoure(minuted,_minuted,_houre);
        

        }
        public void Houreses(int value)
        {
            minutedToHoure.minutetohoure(value, _minuted, _houre);
        }
    }
}

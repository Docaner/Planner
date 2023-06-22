using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class LineItem : BindableBase
    {

        public double X1 { get => _x1; set => SetProperty(ref _x1, value); }
        private double _x1;

        public double Y1 { get => _y1; set => SetProperty(ref _y1, value); }
        private double _y1;

        public double X2 { get => _x2; set => SetProperty(ref _x2, value); }
        private double _x2;

        public static Double Y2 { get => _y2; set => _y2 = value; }
        private static Double _y2;

        public LineItem(double x1, double y1, double x2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
        }
    }
}

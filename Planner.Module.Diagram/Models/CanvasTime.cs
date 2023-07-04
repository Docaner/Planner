using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class CanvasTime:BindableBase
    {


        public double X1 { get => _x1; set => SetProperty(ref _x1, value); }
        private double _x1;

        public double Y1 { get => _y1; set => SetProperty(ref _y1, value); }
        private double _y1;

        public double X2 { get => _x2; set => SetProperty(ref _x2, value); }
        private double _x2;

        public double Y2 { get => _y2; set => SetProperty(ref _y2, value); }
        private double _y2;

        public double X2Text { get => _x2Text; set => SetProperty(ref _x2Text, value); }
        private double _x2Text;

        public double X1Text { get => _x1Text; set => SetProperty(ref _x1Text, value); }
        private double _x1Text;
   
        private void UpdateX2Text()
        {      
                         
        }

        public double TextWidth
        {
            get => _textWidth;
            set
            {
                SetProperty(ref _textWidth, value);
                
            }
        }

        private double _textWidth;

        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                ChangeTimeStringFormat();
            }
        }
        private DateTime _time;

        public string TimeStringFormat { get => _timeStringFormat; set => SetProperty(ref _timeStringFormat, value); }
        private string _timeStringFormat;

        private void ChangeTimeStringFormat()
        {
 
                TimeStringFormat = Time.ToString("HH:mm");
        

        }

        public CanvasTime(double x1, double y1, double x2, double y2, DateTime time)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Time = time;
        }
    }
}



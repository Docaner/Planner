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

        public double Y2 { get => _y2; set => SetProperty(ref _y2, value); }
        private double  _y2;

        public double X2Text { get => _x2Text; set => SetProperty(ref _x2Text, value); }
        private double _x2Text;

        public double X1Text { get => _x1Text; set => SetProperty(ref _x1Text, value); }
        private double _x1Text;
        public double Z { get => _z; set => SetProperty(ref _z, value); }
        private double _z;
        public string Hex { get => _hex; set => SetProperty(ref _hex, value); }
        private string _hex;
        public string Hex1 { get => _hex1; set => SetProperty(ref _hex1, value); }
        private string _hex1;
        private void UpdateX2Text()
        {
            if (Time.Hour == 0 && Time.Minute == 0)
            {
                X2Text = X2 - TextWidth / 2 + 6;
            }
            else if (Time.Minute == 0)
            {
                X2Text = X2 - TextWidth / 2 + 2;

            }

        }

        public double TextWidth
        {
            get => _textWidth;
            set
            {
                SetProperty(ref _textWidth, value);
                UpdateX2Text();
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
            if (Time.Hour == 0 && Time.Minute == 0)
            {
                TimeStringFormat = Time.ToString("dd MMM");
                Z = 1;
                Hex = "#D3D3D3";
                Hex1 = "Blue";
                X2Text += 10;
            }
            else if (Time.Minute == 0)
            {
                TimeStringFormat = Time.ToString("HH:mm");
                Z = 0;
                Hex = "#FFFFFF";
                Hex1 = "Black";

            }


        }

        public LineItem(double x1, double y1, double x2, double y2, DateTime time)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Z = 0;
            Hex = "#FFFFFF";
            Hex1 = Hex;
            Time = time;
        }
    }
}

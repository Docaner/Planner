using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class CanvasFiller : BindableBase
    {
        /// <summary>
        /// Ширина поля Canvas
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
            }
        }
        private double _width;


        /// <summary>
        /// Высота поля Canvas
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
                
                UpdateHourLines();
                UpdateLineNow();
                //UpdateMaxHeightAgregators();
            }
        }
        private double _height;



        /*
        /// <summary>
        /// Высота поля с агрегаторами
        /// </summary>
        public double MaxHeightAgregators
        {
            get => _maxHeightAgregators;
            set
            {
                SetProperty(ref _maxHeightAgregators, value);
                UpdateHeightAgregator();
            }
        }
        private double _maxHeightAgregators;

        private void UpdateMaxHeightAgregators() => MaxHeightAgregators = Height - 30;

        /// <summary>
        /// Высота одного агрегатора в канвасе
        /// </summary>
        public double HeightAgregator { get => _heightAgregator; set => SetProperty(ref _heightAgregator, value); }
        private double _heightAgregator;

        private void UpdateHeightAgregator()
        {
            if (Agregators == null) return;
            HeightAgregator = MaxHeightAgregators / Agregators.Count;
        }*/





        /// <summary>
        /// Линия, показывающая текущее время
        /// </summary>
        public LineItem LineNow { 
            get => _lineNow; 
            set =>SetProperty(ref _lineNow,value); 
        }
        private LineItem _lineNow;

        /// <summary>
        /// Коллекция с вертикальными (часовыми)
        /// </summary>
        public ObservableCollection<LineItem> HourLines
        {
            get => _hourLines;
            set => SetProperty(ref _hourLines, value);
        }

        private ObservableCollection<LineItem> _hourLines;

        /// <summary>
        /// Обновление координат часовых линий
        /// </summary>
        public void UpdateHourLines()
        {
            if (HourLines == null) return;

            double y2 = Height - 20;
            foreach (LineItem line in HourLines)
                line.Y2 = y2;
        }
        /// <summary>
        /// Обновление координат линии реального времени
        /// </summary>
        public void UpdateLineNow()
        {
            
            double y2 = Height - 20;
            LineNow.Y2 = y2;
        }

        /// <summary>
        /// Время, с которого начинается канвас
        /// </summary>
        public DateTime TimeStart { get; private set; }

        /// <summary>
        /// Время, до которого заканчивается рисоваться канвас
        /// </summary>
        public DateTime TimeEnd { get; private set; }

        /// <summary>
        /// Чему равно расстояние между двумя часовыми линиями в пикселях
        /// </summary>
        public double PixelsInHour { get; private set; }


        /// <summary>
        /// Метод создающий линии в заданом диапазоне
        /// </summary>
        void LinesDraw(DateTime start, DateTime end)
        {
            TimeStart = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
            TimeEnd = end;

            HourLines = new ObservableCollection<LineItem>();

            DateTime timeIterator = TimeStart;
            double width = PixelsInHour;

            while (timeIterator < end)
            {
                timeIterator = timeIterator.AddHours(1);
                HourLines.Add(new LineItem(width, 0.0, width, Height, timeIterator));
                width += PixelsInHour;
            }

            Width = width;
        }

        /// <summary>
        /// Метод, создающий линию,показывающую реальное время
        /// </summary>
        void LineNowDraw(DateTime start,DateTime nowTime)
        {
            TimeStart = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
            DateTime timeIterator = TimeStart;
            double width = PixelsInHour;

            //TimeSpan diff = nowTime.Subtract(TimeStart);
            //width+= diff.TotalMinutes;

            TimeSpan span = nowTime - TimeStart;
            width= PixelsInHour * span.TotalSeconds / 3600;
            LineNow = new LineItem(width, 0.0, width, Height,nowTime);

        }
        /// <summary>
        /// Агрегаторы
        /// </summary>
        public ObservableCollection<Agregator> Agregators
        {
            get => _agregators;
            set
            {
                SetProperty(ref _agregators, value);
                //UpdateHeightAgregator();
            }
        }

        private ObservableCollection<Agregator> _agregators;

        public CanvasFiller()
        {
            PixelsInHour = 60;
            LineNowDraw(DateTime.Now.AddDays(-2),DateTime.Now);
            Height = 0;
            Width = 0;


            LinesDraw(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));
            
        }
    }
}

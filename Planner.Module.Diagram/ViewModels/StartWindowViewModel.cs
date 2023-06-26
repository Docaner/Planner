using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Planner.Module.Diagram.Models;
using Planner.Module.Diagram.Components;

namespace Planner.Module.Diagram.ViewModels
{
    public class StartWindowViewModel : BindableBase
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }
        private string _header;

        void RefreshHeader() => Header = "Всем привет! Это ПЛАНИРОВЩИК!" + CanvasHeight + " and " + CanvasWidth;

        /// <summary>
        /// Высота поля Canvas
        /// </summary>
        public Double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                SetProperty(ref _canvasHeight, value);
                RefreshHeader();
                UpdateHeight();
                LineY = value - 20.0;
            }
        }
        private Double _canvasHeight = 100;

        public void UpdateHeight()
        {
            UpdateHourLine();
        }

        /// <summary>
        /// Ширина поля Canvas
        /// </summary>
        public Double CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                SetProperty(ref _canvasWidth, value);
                RefreshHeader();
            }
        }
        private Double _canvasWidth;

        /// <summary>
        /// Кордината Y таймлайна
        /// </summary>
        public Double LineY
        {
            get => _lineY;
            set => SetProperty(ref _lineY, value);
        }
        private Double _lineY;


        public ObservableCollection<LineItem> CanvasHourLines
        {
            get => _canvasHourLines;
            set => SetProperty(ref _canvasHourLines, value);
        }

        private ObservableCollection<LineItem> _canvasHourLines;

        public void UpdateHourLine()
        {
            double y2 = CanvasHeight - 20;
            foreach (LineItem line in CanvasHourLines)
                line.Y2 = y2;
        }

        void LinesDraw()
        {
            CanvasHourLines = new ObservableCollection<LineItem>();

            LineItem line;

            DateTime timeNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            DateTime timeStart = timeNow.AddDays(-2);
            DateTime timeEnd = timeNow.AddDays(2);

            DateTime timeIterator = timeStart;

            double step = 60;
            double width = step;

            while (timeIterator < timeEnd)
            {
                timeIterator = timeIterator.AddHours(1);
                line = new LineItem(width, 0.0, width, CanvasHeight, timeIterator);
                CanvasHourLines.Add(line);
                width += step;
            }

            CanvasWidth = width;
        }

        public ObservableCollection<Agregator> Agregators
        {
            get => _agregators;
            set => SetProperty(ref _agregators, value);
        }

        private ObservableCollection<Agregator> _agregators;

        public StartWindowViewModel()
        {
            Agregators=new ObservableCollection<Agregator>();
            //RefreshHeader();
            CanvasWidth = 2000;
            LinesDraw();

            ObservableCollection<Agregator> agregators = new ObservableCollection<Agregator>();
            agregators.Add(new Agregator("КОНВ 1"));
            agregators[0].Add(new Plavki("das", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
        
           
    
            Agregators = agregators;
        }



    }
}

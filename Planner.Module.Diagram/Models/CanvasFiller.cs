using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
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
                UpdateAgragatorsWidth();
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
                HeightUniformGrid = _height - 20;
                //UpdateMaxHeightAgregators();
            }
        }
        private double _height;

        public double HeightUniformGrid
        {
            get => _heightUniformGrid;
            set
            {
                if (value < 0) return;
                SetProperty(ref _heightUniformGrid, value);
            }
        }
        private double _heightUniformGrid;
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
            double y1 = Height + 440;
            int z = 1;
            foreach (LineItem line in HourLines)
            { if (z % 6 != 0)
                {
                    line.Y2 = y2+10;
                    line.Y1 = y1;
                }
            else {  line.Y2 = y2;
                    line.Y1 = y1;
                }
                z++;
            }
                
        }
        
        /// <summary>
        /// Обновление координат линии реального времени
        /// </summary>
        public void UpdateLineNow()
        {
            if (LineNow == null) return;
            double y2 = Height - 20;
            LineNow.LineNow.Y2 = y2;
        }


        /// <summary>
        /// Метод создающий линии в заданом диапазоне
        /// </summary>
        void LinesDraw(DateTime start, DateTime end)
        {
            _settings.TimeStart = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
            _settings.TimeEnd = end;

            LineNowInit();

            HourLines = new ObservableCollection<LineItem>();
         

            DateTime timeIterator = _settings.TimeStart;
            double width = _settings.PixelsInHour/6;
           

            while (timeIterator < end)
            {
               
                timeIterator = timeIterator.AddMinutes(10);
               
                HourLines.Add(new LineItem(width, Height+440 , width, Height, timeIterator));
                width += _settings.PixelsInHour/6;
                
            }

            Width = width;
           
           

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
                InitMeltingLines();
                SubscribrMeltings();
                ReDrawLines();
                UpdateMeltings(); 
                UpdateAgragatorsWidth();
            }
        }

        private void UpdateAgragatorsWidth()
        {
            if (Agregators == null) return;
            foreach (Agregator agr in Agregators)
            {
                agr.Width = Width;
            }
        }

        private void ReDrawLines()
        {
            DateTime min, max;
            FindMinMaxTimeMeltings(out min, out max);
            LinesDraw(min.AddHours(-1), max.AddHours(1));
        }

        private void UpdateMeltings()
        {
            foreach (Agregator agregator in Agregators)
                foreach (Melting melting in agregator.Meltings)
                    melting.UpdateCanvasLeft();
        }


        private ObservableCollection<Agregator> _agregators;

        /// <summary>
        /// Подписка на события Meltings
        /// </summary>
        private void SubscribrMeltings()
        {
            foreach (Agregator agr in Agregators)
            {
                foreach (Melting mel in agr.Meltings)
                {
                    mel.EventMouseEnter += DrawLinesToMeltings;
                    mel.EventMouseLeave += LeaveMeltingsLines;
                }
            }
        }
        /// <summary>
        /// Поиск минимального и максимального времени плавок
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void FindMinMaxTimeMeltings(out DateTime min, out DateTime max)
        {
            min = Agregators[0].Meltings[0].Start;
            max = Agregators[0].Meltings[0].Start;
            foreach (Agregator agr in Agregators)
            {
                foreach (Melting mel in agr.Meltings)
                {
                    if (mel.Start < min)
                    {
                        min = mel.Start;
                    }
                    if (mel.End > max)
                    {
                        max = mel.End;
                    }
                }
            }
        }

        /// <summary>
        /// Коллекция линий, которые рисуются к сфокусированным плавкам
        /// </summary>
        public ObservableCollection<LineItem> MeltingLines { get => _meltingLines; set => SetProperty(ref _meltingLines, value); }
        private ObservableCollection<LineItem> _meltingLines;

        /// <summary>
        /// Инициализация коллекции MeltingLines
        /// </summary>
        private void InitMeltingLines()
        {
            ObservableCollection<LineItem> lines = new ObservableCollection<LineItem>();
            //Необходимо линий на 1 меньше, чем агрегаторов.
            for (int i = 0; i < Agregators.Count - 1; i++) lines.Add(new LineItem(0, 0, 0, 0, DateTime.Now));
            MeltingLines = lines;
        }

        /// <summary>
        /// Рисует линии к указанной плавке в разных агрегаторах
        /// </summary>
        /// <param name="target">Melting, на который наведен курсор</param>
        private void DrawLinesToMeltings(Melting target)
        {
            LeaveMeltingsLines(null);

            Melting startDraw = Agregators[0].Meltings.FirstOrDefault(x => x.Id == target.Id);
            Melting endDraw;

            int indexLine = 0;
            double y = Agregators[0].Height / 2;

            for (int i = 1; i < Agregators.Count; i++)
            {
                endDraw = Agregators[i].Meltings.FirstOrDefault(x => x.Id == target.Id);

                if (endDraw == null) continue;

                if (startDraw != null)
                {
                    MeltingLines[indexLine].X1 = startDraw.CanvasLeft;
                    MeltingLines[indexLine].Y1 = y;

                    y += Agregators[i].Height;

                    MeltingLines[indexLine].X2 = endDraw.CanvasLeft;
                    MeltingLines[indexLine].Y2 = y;

                    indexLine++;
                }

                startDraw = endDraw;
            }
        }

        /// <summary>
        /// Убирает линии проведённые к плавкам
        /// </summary>
        /// <param name="target">Плавка</param>
        private void LeaveMeltingsLines(Melting target)
        {
            foreach (LineItem line in MeltingLines)
            {
                line.X1 = 0;
                line.Y1 = 0;
                line.X2 = 0;
                line.Y2 = 0;
            }
        }
        /// <summary>
        /// Линия реального времени
        /// </summary>
        public RealTimeLine LineNow { get; private set; }

        private void LineNowInit() => LineNow = new RealTimeLine(_settings, Height);

        /// <summary>
        /// Настройки канваса
        /// </summary>
        private CanvasSettings _settings;

        public CanvasFiller(CanvasSettings settings)
        {
            _settings = settings;
            Height = 0;
            Width = 0;

            LinesDraw(_settings.TimeStart, _settings.TimeEnd);
            LineNowInit();
        }
    }
}

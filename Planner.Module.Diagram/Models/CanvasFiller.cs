using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

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

                UpdateLines();
                UpdateLineNow();
                if (Lines != null)
                {
                    Lines.Y1 = Height - 64;
                    Lines.Y2 = Height - 64;
                }
                HeightUniformGrid = _height - 90;
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
        public void UpdateLines()
        {
            if (HourLines == null) return;

            double y2 = Height - 74;
            double y1 = Height - 56;
            int z = 1;
            foreach (LineItem line in HourLines)
            {
                if (z % 6 != 0)
                {
                    line.Y2 = y2 + 3;
                    line.Y1 = y1;

                }
                else
                {
                    line.Y2 = y2;
                    line.Y1 = y1 + 3;

                }

                line.X1Text = Height - 50;
                z++;
            }

        }

        /// <summary>
        /// Обновление координат линии реального времени
        /// </summary>
        public void UpdateLineNow()
        {
            if (LineNow == null) return;
            double y2 = Height - 64;
            LineNow.LineNow.Y2 = y2;
        }

        private CanvasLine lines;
        public CanvasLine Lines
        {
            get => lines; set => SetProperty(ref lines, value);

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
            double width = _settings.PixelsInHour / 6;


            while (timeIterator < end)
            {

                timeIterator = timeIterator.AddMinutes(10);

                HourLines.Add(new LineItem(width, Height + 440, width, Height, timeIterator));
                width += _settings.PixelsInHour / 6;

            }
            Lines = new CanvasLine(0.0, Height - 70, width, Height - 70);

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
            if (Agregators == null || Agregators.Count <= 0) return;
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
        /// Рисовка линий к плавкам
        /// </summary>
        /// <param name="target">Плавка, к которой нужно рисовать</param>
        private void ShowMeltingLines(Melting target)
        {
            Melting startDraw = Agregators[0].Meltings.FirstOrDefault(x => x.Id == target.Id);
            Melting endDraw;

            int indexLine = 0;
            double y1 = Agregators[0].ActualHeight / 2;
            double y2 = y1 + Agregators[0].ActualHeight;

            for (int i = 1; i < Agregators.Count; i++)
            {
                endDraw = Agregators[i].Meltings.FirstOrDefault(x => x.Id == target.Id);

                if (startDraw != null && endDraw != null)
                {
                    MeltingLines[indexLine].X1 = startDraw.CanvasLeft + startDraw.Width / 2;
                    MeltingLines[indexLine].Y1 = indexLine <= 0 ? y1 : MeltingLines[indexLine - 1].Y2;

                    MeltingLines[indexLine].X2 = endDraw.CanvasLeft + endDraw.Width / 2;
                    MeltingLines[indexLine].Y2 = y2;
                    
                    startDraw = endDraw;

                    indexLine++;
                }

                y2 += Agregators[i].ActualHeight;

            }
        }

        /// <summary>
        /// Скрывает линии, проведенные к плавкам
        /// </summary>
        private void HideMeltingLines()
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
        /// Рисует линии к указанной плавке в разных агрегаторах
        /// </summary>
        /// <param name="target">Melting, на который наведен курсор</param>
        private void DrawLinesToMeltings(Melting target)
        {
            ShowMeltingLines(target);
            ShowStartEndLines(target);

        }

        /// <summary>
        /// Убирает линии проведённые к плавкам
        /// </summary>
        /// <param name="target">Плавка</param>
        private void LeaveMeltingsLines(Melting target)
        {
            HideMeltingLines();
            HideStartEndLines();
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

        /// <summary>
        /// Линии начала и конца плавки
        /// </summary>
        public ObservableCollection<CanvasTime> StartEndLines
        {
            get => _startEndLines;
            set => SetProperty(ref _startEndLines, value);
        }

        private ObservableCollection<CanvasTime> _startEndLines;

        private void InitStartEndLines()
        {
            StartEndLines = new ObservableCollection<CanvasTime>();
            for(int i = 0; i < 2; i++)
                StartEndLines.Add(new CanvasTime(0, 0, 0, 0, DateTime.Now));
        }

        /// <summary>
        /// Показывает линии начала и конца
        /// </summary>
        /// <param name="target">Плавка, к которой рисуются линии</param>
        private void ShowStartEndLines(Melting target)
        {
            int indexAgr = 0;
            Agregator agr = null;

            for (int i = 0; i < Agregators.Count; i++)
            {
                if (Agregators[i].Meltings.FirstOrDefault(x => x == target) != null)
                {
                    agr = Agregators[i];
                    indexAgr = i;
                    break;
                }
            }

            double y1 = agr.ActualHeight * (indexAgr + 1) - (agr.ActualHeight - agr.Height) / 2;


            DateTime start = target.Start;
            DateTime end = target.End;

            double canvasLeft = _settings.ConvertTimeToCanvasLeft(start) + 1;
            StartEndLines[0].X1 = canvasLeft;
            StartEndLines[0].Y1 = y1;
            StartEndLines[0].X2 = canvasLeft;
            StartEndLines[0].Y2 = Height - 64;
            StartEndLines[0].Time = target.Start;

            StartEndLines[0].X2Text = StartEndLines[0].X2 - 8;
            StartEndLines[0].X1Text = Height - 50;
            canvasLeft = _settings.ConvertTimeToCanvasLeft(end) + 1;
            StartEndLines[1].X1 = canvasLeft;
            StartEndLines[1].Y1 = y1;
            StartEndLines[1].X2 = canvasLeft;
            StartEndLines[1].Y2 = Height - 64;
            StartEndLines[1].Time = target.End;
            StartEndLines[1].X2Text = StartEndLines[1].X2 - 8;
            StartEndLines[1].X1Text = Height - 50;
        }

            private void HideStartEndLines()
        {
            foreach(CanvasTime line in StartEndLines)
            {
                line.X1 = 0;
                line.Y1 = 0;
                line.X2 = 0;
                line.Y2 = 0;
                line.TimeStringFormat = "";
            }
        }

        public CanvasFiller(CanvasSettings settings)
        {
            _settings = settings;
            Height = 0;
            Width = 0;

            LinesDraw(_settings.TimeStart, _settings.TimeEnd);
            LineNowInit();
            InitStartEndLines();
        }
    }
}

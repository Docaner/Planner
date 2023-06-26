using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class Melting : BindableBase
    {
        /// <summary>
        /// Номер плавки
        /// </summary>
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        private int _id;

        /// <summary>
        /// Время старта плавки
        /// </summary>
        public DateTime Start
        {
            get => _start;
            set
            {
                _start = value;
                UpdateStartStringFormat();
                UpdateCanvasLeft();
            }
        }
        private DateTime _start;

        public string StartStringFormat { get => _startStringFormat; set => SetProperty(ref _startStringFormat, value); }
        private string _startStringFormat;

        private void UpdateStartStringFormat() => StartStringFormat = Start.ToString("HH:mm");

        /// <summary>
        /// Время конца плавки
        /// </summary>
        public DateTime End
        {
            get => _end;
            set
            {
                _end = value;
                UpdateEndStringFormat();
                UpdateWidth();
            }
        }
        private DateTime _end;

        public string EndStringFormat { get => _endStringFormat; set => SetProperty(ref _endStringFormat, value); }
        private string _endStringFormat;

        private void UpdateEndStringFormat() => EndStringFormat = End.ToString("HH:mm");

        /// <summary>
        /// Отступ от левого конца канваса
        /// </summary>
        public double CanvasLeft { get => _canvasLeft; set => SetProperty(ref _canvasLeft, value); }
        private double _canvasLeft;

        private void UpdateCanvasLeft() => CanvasLeft = ConvertTimeToCanvasLeft(Start);

        /// <summary>
        /// Ширина блока
        /// </summary>
        public double Width { get => _width; set => SetProperty(ref _width, value); }
        private double _width;

        private void UpdateWidth() => Width = ConvertTimeToCanvasLeft(End) - CanvasLeft;

        /// <summary>
        /// Конвертация времени в CanvasLeft
        /// </summary>
        /// <param name="time">Время</param>
        /// <returns>Отступ CanvasLeft</returns>
        private double ConvertTimeToCanvasLeft(DateTime time)
        {
            TimeSpan span = time - ContextViewModel.StartWindow.Filler.TimeStart; 
            return ContextViewModel.StartWindow.Filler.PixelsInHour * span.TotalSeconds / 3600;
        }

        public Melting(int id, DateTime start, DateTime end)
        {
            Id = id;
            Start = start;
            End = end;
        }
    }
}

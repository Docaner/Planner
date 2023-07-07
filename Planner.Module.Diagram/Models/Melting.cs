using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace Planner.Module.Diagram.Models
{
    public class Melting : BindableBase
    {
        private string _hex;
        public string Hex
        {
            get => _hex; set => SetProperty(ref _hex, value);
        }
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
                
            }
        }
        private DateTime _end;

        public string EndStringFormat { get => _endStringFormat; set => SetProperty(ref _endStringFormat, value); }
        private string _endStringFormat;

        private void UpdateEndStringFormat() => EndStringFormat = End.ToString("HH:mm");

        /// <summary>
        /// Отступ от левого конца канваса
        /// </summary>
        public double CanvasLeft { get => _canvasLeft; set {
                SetProperty(ref _canvasLeft, value);
                  Start=_settings.ConvertCanvasToTime(value);
                  End=_settings.ConvertCanvasToTime(Width);
                    } 
        
        
        }
        private double _canvasLeft;

        public void UpdateCanvasLeft() => CanvasLeft = _settings.ConvertTimeToCanvasLeft(Start);

        /// <summary>
        /// Ширина блока
        /// </summary>
        public double Width { get => _width; set => SetProperty(ref _width, value); }
        private double _width;

        private void UpdateWidth() => Width = _settings.ConvertTimeToCanvasLeft(End) - CanvasLeft;

        /// <summary>
        /// Высота плавки 
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                if (value < 0) return;
                SetProperty(ref _height, value);
            }
        }
        private double _height;

        /// <summary>
        /// Событие MouseEnter
        /// </summary>
        public ICommand MouseEnter { get; }

        public event Action<Melting> EventMouseEnter;

        private void MouseEnterAct()
        {
            EventMouseEnter?.Invoke(this);
        }
        /// <summary>
        /// Событие MouseLeave
        /// </summary>
        public ICommand MouseLeave { get; }

        public event Action<Melting> EventMouseLeave;

        private void MouseLeaveAct()
        {
            EventMouseLeave?.Invoke(this);
        }

        /// <summary>
        /// Действие нажатия на левую кнопку
        /// </summary>
        public ICommand MouseLeftButtonDown { get;}
        /// <summary>
        /// Событие нажатия на ЛКМ
        /// </summary>
        public event Action<Melting> EventMouseLeftButtonDown;
        
        /// <summary>
        /// Действие отжатия левой кнопки
        /// </summary>
        public ICommand MouseLeftButtonUp { get; }
        /// <summary>
        /// Событие нажатия на ЛКМ
        /// </summary>
        public event Action<Melting> EventMouseLeftButtonUp;

        /// <summary>
        /// Данные о канвас
        /// </summary>
        private CanvasSettings _settings;

        private string ColorHash(int id)
        {
            int iResult = (987 * id + 999) % 0xFFF;
            return $"#{iResult:X3}";
        }

        public Melting(int id, DateTime start, DateTime end, string hex, CanvasSettings settings)
        {
            _settings = settings;
            Id = id;
            Start = start;
            End = end;
            Hex = hex;

            MouseEnter = new DelegateCommand(MouseEnterAct);
            MouseLeave = new DelegateCommand(MouseLeaveAct);
            MouseLeftButtonDown = new DelegateCommand(() => EventMouseLeftButtonDown?.Invoke(this) );
            MouseLeftButtonUp = new DelegateCommand(() => EventMouseLeftButtonUp?.Invoke(this));
        }

        public Melting(int id, DateTime start, DateTime end, CanvasSettings settings)
        {
            _settings = settings;
            Id = id;
            Start = start;
            End = end;
            UpdateCanvasLeft();
            UpdateWidth();
            Hex = ColorHash(id);

            MouseEnter = new DelegateCommand(MouseEnterAct);
            MouseLeave = new DelegateCommand(MouseLeaveAct);
            MouseLeftButtonDown = new DelegateCommand(() => EventMouseLeftButtonDown?.Invoke(this));
            MouseLeftButtonUp = new DelegateCommand(() => EventMouseLeftButtonUp?.Invoke(this));
        }
    }
}

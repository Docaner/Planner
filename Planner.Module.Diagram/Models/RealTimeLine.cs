using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Planner.Module.Diagram.Models
{
    public class RealTimeLine : BindableBase
    {
        /// <summary>
        /// Линия, показывающая реальное время
        /// </summary>
        public LineItem LineNow { get; set; }

        /// <summary>
        /// Таймер
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// Настройка канваса
        /// </summary>
        private CanvasSettings _settings;

        public RealTimeLine(CanvasSettings settings, double height)
        {
            _settings = settings;

            LineNowInit(height);
            startTimer();
        }
        /// <summary>
        /// Метод, определяющий координаты линии, показывающей реальное время
        /// </summary>
        public void LineNowInit(double height)
        {
            double width = _settings.ConvertTimeToCanvasLeft(DateTime.Now);
            LineNow = new LineItem(width, 0.0, width, height, DateTime.Now);
        }

        public void startTimer()
        {
            timer = new DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Start();
        }

        void timerTick(object sender, EventArgs e)
        {
            double width = _settings.ConvertTimeToCanvasLeft(DateTime.Now);
            LineNow.X1 = width;
            LineNow.X2 = width;
        }
    }
}

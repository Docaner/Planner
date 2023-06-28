using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
        private LineItem _lineNow;
        /// <summary>
        /// Таймер
        /// </summary>
        private DispatcherTimer timer;
        public RealTimeLine()
        {
            LineNowDraw1(DateTime.Now.AddDays(-2), DateTime.Now);
            startTimer();
        }
        /// <summary>
        /// Метод, определяющий координаты линии, показывающей реальное время
        /// </summary>
        /// <param name="start">Время, с которого начинается canvas</param>
        /// <param name="nowTime">Текущее время</param>
        public void LineNowDraw1(DateTime start, DateTime nowTime)
        {
            TimeSpan span = nowTime - ContextViewModel.StartWindow.Filler.TimeStart;
            double width = ContextViewModel.StartWindow.Filler.PixelsInHour * span.TotalSeconds / 3600;

            LineNow = new LineItem(width, 0.0, width, ContextViewModel.StartWindow.Filler.Height, nowTime);
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
            TimeSpan span = DateTime.Now - ContextViewModel.StartWindow.Filler.TimeStart;
            double width = ContextViewModel.StartWindow.Filler.PixelsInHour * span.TotalSeconds / 3600;
            LineNow.X1 = width;
            LineNow.X2 = width;
        }
    }
}

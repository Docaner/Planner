using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class CanvasSettings
    {
        /// <summary>
        /// Время, с которого начинается канвас
        /// </summary>
        public DateTime TimeStart { get; set; }

        /// <summary>
        /// Время, до которого заканчивается рисоваться канвас
        /// </summary>
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// Чему равно расстояние между двумя часовыми линиями в пикселях
        /// </summary>
        public double PixelsInHour { get; set; }

        /// <summary>
        /// Конвертация времени в CanvasLeft
        /// </summary>
        /// <param name="time">Время</param>
        /// <returns>Отступ CanvasLeft</returns>
        public double ConvertTimeToCanvasLeft(DateTime time)
        {
            TimeSpan span = time - TimeStart;
            return PixelsInHour * span.TotalSeconds / 3600;
        }

        public CanvasSettings(DateTime timeStart, DateTime timeEnd, double pixelsInHour)
        {
            TimeStart = timeStart;
            TimeEnd = timeEnd;
            PixelsInHour = pixelsInHour;
        }

        public CanvasSettings(double pixelsInHour)
        {
            TimeStart = DateTime.Now;
            TimeEnd = DateTime.Now;
            PixelsInHour = pixelsInHour;
        }
    }
}

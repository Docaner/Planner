using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class MouseMelting : IMouseCaptureProxy
    {
        public event EventHandler Capture;
        public event EventHandler Release;

        /// <summary>
        /// Мышь нажата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseDown(object sender, MouseCaptureArgs e)
        {
            _oldX = e.X;
        }

        /// <summary>
        /// Движение мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseMove(object sender, MouseCaptureArgs e)
        {
            if (Focus == null) return;

            double newX = e.X;
            double diffrence = newX - _oldX;
            
            Focus.CanvasLeft += diffrence;

            _oldX = newX;
        }

        /// <summary>
        /// Мышь отжата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseUp(object sender, MouseCaptureArgs e) { }

        /// <summary>
        /// Плавка, которая передвигается
        /// </summary>
        public Melting Focus { get; private set; }

        /// <summary>
        /// Установить фокус на плавку
        /// </summary>
        /// <param name="m"></param>
        public void SetFocus(Melting m) => Focus = m;
        /// <summary>
        /// Удалить фокус с плавки
        /// </summary>
        /// <param name="m"></param>
        public void RemoveFocus(Melting m) => Focus = null;

        /// <summary>
        /// Старое положение курсора
        /// </summary>
        double _oldX;
    }
}

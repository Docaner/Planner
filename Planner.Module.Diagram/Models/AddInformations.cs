using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace Planner.Module.Diagram.Models
{
    public class AddInformations : BindableBase
    {
        /// <summary>
        /// Свойство для связи данных плавки и окна доп.информации. Хранить ссылку на текущую плавку. 
        /// </summary>
        public Melting FocusMelting { get => _focusMelting; set => SetProperty(ref _focusMelting, value); }
        private Melting _focusMelting;

        /// <summary>
        /// Свойство для настройки видимости. Хранит ссылку на плавку, на которую нажали ПКМ в прошлый раз.
        /// </summary>
        public Melting OldFocusMelting { get => _oldfocusMelting; set => SetProperty(ref _oldfocusMelting, value); }
        private Melting _oldfocusMelting;

        /// <summary>
        /// Отступ окна информации от начала canvas
        /// </summary>
        public double WidthWinAdd { get => _widthWinAdd; set => SetProperty(ref _widthWinAdd, value); }
        private double _widthWinAdd;

        /// <summary>
        /// Высота окна доп.информации
        /// </summary>
        public double HeighthWinAdd { get => _heightWinAdd; set => SetProperty(ref _heightWinAdd, value); }
        private double _heightWinAdd;

        /// <summary>
        /// Отображаемость окна доп.информации
        /// </summary>
        public Visibility Visibility { get => _visibility; set => SetProperty(ref _visibility, value); }
        private Visibility _visibility = Visibility.Hidden;

        /// <summary>
        /// Метод, меняющих видимость окна с доп.информацией 
        /// </summary>
        public void VisibilityWinAddInf(Melting target)
        {
            if (OldFocusMelting == null) { OldFocusMelting = target; }
            if (OldFocusMelting == target)
            {
                if (Visibility == Visibility.Visible) { Visibility = Visibility.Hidden; return; }
                if (Visibility == Visibility.Hidden) { Visibility = Visibility.Visible; return; }
            }
            else
            {
                OldFocusMelting = target;
                Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Метод, определяющий в последнем ли агрегаторе плавка.
        /// </summary>
        /// <param name="targe"></param>
        public int NumberAddInform(int numberTargetAgr, int colAgr)
        {
            if (numberTargetAgr + 1 == colAgr) { return 1; }//последний
            return 0;//между первым и последним
        }
        public AddInformations() { }

    }
}

using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Planner.Module.Diagram.Models;

namespace Planner.Module.Diagram.ViewModels
{
    public class StartWindowViewModel : BindableBase
    {
        public CanvasFiller Filler { get; private set; }

        public StartWindowViewModel()
        {
            ContextViewModel.StartWindow = this;
            Filler = new CanvasFiller();

            ObservableCollection<Agregator> agregators = new ObservableCollection<Agregator>()
            {
                new Agregator
                (
                    "Agr1",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(1, DateTime.Now.AddDays(-2).AddHours(1), DateTime.Now.AddDays(-2).AddHours(3)),
                        new Melting(2, DateTime.Now.AddDays(-2).AddHours(4), DateTime.Now.AddDays(-2).AddHours(6)),
                        new Melting(3, DateTime.Now.AddDays(-2).AddHours(7), DateTime.Now.AddDays(-2).AddHours(9))
                    }
                ),
                new Agregator
                (
                    "Agr2",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(123, DateTime.Now.AddDays(-2).AddHours(0), DateTime.Now.AddDays(-2).AddHours(2)),
                        new Melting(124, DateTime.Now.AddDays(-2).AddHours(3), DateTime.Now.AddDays(-2).AddHours(5)),
                        new Melting(125, DateTime.Now.AddDays(-2).AddHours(8), DateTime.Now.AddDays(-2).AddHours(10))
                    }
                ),
                new Agregator
                (
                    "Agr3",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(556, DateTime.Now.AddDays(-2).AddHours(3), DateTime.Now.AddDays(-2).AddHours(6)),
                        new Melting(665, DateTime.Now.AddDays(-2).AddHours(7), DateTime.Now.AddDays(-2).AddHours(10)),
                        new Melting(777, DateTime.Now.AddDays(-2).AddHours(11), DateTime.Now.AddDays(-2).AddHours(13))
                    }
                ),
            };

            Filler.Agregators = agregators;
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }
        private string _header;

        public void RefreshHeader() => Header = "Всем привет! Это ПЛАНИРОВЩИК!" + Filler?.Height + " and " + Filler?.Width;
    }
}

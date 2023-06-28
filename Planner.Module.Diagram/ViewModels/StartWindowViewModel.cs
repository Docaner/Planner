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
                        new Melting(1, DateTime.Now.AddDays(-2).AddHours(1), DateTime.Now.AddDays(-2).AddHours(3),"#ADFF2F"),
                        new Melting(2, DateTime.Now.AddDays(-2).AddHours(4), DateTime.Now.AddDays(-2).AddHours(6),"#8B0000"),
                        new Melting(3, DateTime.Now.AddDays(-2).AddHours(7), DateTime.Now.AddDays(-2).AddHours(9),"#ADFF2F")
                    }
                ),
                new Agregator
                (
                    "Agr2",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(1, DateTime.Now.AddDays(-2).AddHours(0), DateTime.Now.AddDays(-2).AddHours(2), "#ADFF2F"),
                        new Melting(2, DateTime.Now.AddDays(-2).AddHours(3), DateTime.Now.AddDays(-2).AddHours(5), "#1E90FF"),
                        new Melting(3, DateTime.Now.AddDays(-2).AddHours(8), DateTime.Now.AddDays(-2).AddHours(10), "#ADFF2F")
                    }
                ),
                new Agregator
                (
                    "Agr3",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(1, DateTime.Now.AddDays(-2).AddHours(3), DateTime.Now.AddDays(-2).AddHours(6), "#ADFF2F"),
                        new Melting(2, DateTime.Now.AddDays(-2).AddHours(7), DateTime.Now.AddDays(-2).AddHours(10), "#FFFF00"),
                        new Melting(3, DateTime.Now.AddDays(-2).AddHours(11), DateTime.Now.AddDays(-2).AddHours(13), "#ADFF2F")
                    }
                ),
            };

            Filler.Agregators = agregators;
            Filler.LineNow = new RealTimeLine();
        }

        
    }
}

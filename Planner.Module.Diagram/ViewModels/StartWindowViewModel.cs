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
            CanvasSettings settings = new CanvasSettings(60);

            Filler = new CanvasFiller(settings);

            ObservableCollection<Agregator> agregators = new ObservableCollection<Agregator>()
            {
                new Agregator
                (
                    "Agr1",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(1, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(3),"#ADFF2F", settings),
                        new Melting(2, DateTime.Now.AddHours(4), DateTime.Now.AddHours(6),"#8B0000", settings),
                        new Melting(3, DateTime.Now.AddHours(7), DateTime.Now.AddHours(9),"#ADFF2F", settings)
                    }
                ),
                new Agregator
                (
                    "Agr2",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(1, DateTime.Now.AddHours(0), DateTime.Now.AddHours(2), "#ADFF2F", settings),
                        new Melting(2, DateTime.Now.AddHours(3), DateTime.Now.AddHours(5), "#1E90FF", settings),
                        new Melting(3, DateTime.Now.AddHours(8), DateTime.Now.AddHours(10), "#ADFF2F", settings)
                    }
                ),
                new Agregator
                (
                    "Agr3",
                    new ObservableCollection<Melting>()
                    {
                        new Melting(1, DateTime.Now.AddHours(3), DateTime.Now.AddHours(6), "#ADFF2F", settings),
                        new Melting(2, DateTime.Now.AddHours(7), DateTime.Now.AddHours(10), "#FFFF00", settings),
                        new Melting(3, DateTime.Now.AddHours(11), DateTime.Now.AddHours(13), "#ADFF2F", settings)
                    }
                ),
            };

            Filler.Agregators = agregators;
        }

        
    }
}

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
            CanvasSettings settings = new CanvasSettings(300);

            Filler = new CanvasFiller(settings);

            ObservableCollection<Agregator> agregators = new AgregatorsCollection(settings);

            Filler.Agregators = agregators;
        }

        
    }
}

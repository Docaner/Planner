using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.ViewModels
{
    public class StartWindowViewModel : BindableBase
    {
        private String _header;

        public String Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        public StartWindowViewModel()
        {
            Header = "Всем привет! Это ПЛАНИРОВЩИК!";
        }
    }
}

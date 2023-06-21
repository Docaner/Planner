using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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

        private int _windowHeight;
        public int WindowHeight
        {
            get => _windowHeight;
            set => SetProperty(ref _windowHeight, value);
        }

        private int _windowWidth;
        public int WindowWidth
        {
            get => _windowWidth;
            set => SetProperty(ref _windowWidth, value);
        }

        public StartWindowViewModel()
        {
            WindowHeight = 600;
            WindowWidth = 800;
            Header = "Всем привет! Это ПЛАНИРОВЩИК!";
        }
    }
}

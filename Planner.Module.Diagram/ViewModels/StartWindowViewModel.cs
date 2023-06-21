using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Planner.Module.Diagram.ViewModels
{
    public class StartWindowViewModel : BindableBase
    {
        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        void RefreshHeader() => Header = "Всем привет! Это ПЛАНИРОВЩИК!" + CanvasHeight + " and " + CanvasWidth;

        private Double _canvasHeight;
        public Double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                SetProperty(ref _canvasHeight, value);
                RefreshHeader();
                LineY = value - 20.0;
            }
        }

        private Double _canvasWidth;
        public Double CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                SetProperty(ref _canvasWidth, value);
                RefreshHeader();
            }
        }

        private Double _lineY;

        public Double LineY
        {
            get => _lineY;
            set => SetProperty(ref _lineY, value);
        }

        public StartWindowViewModel()
        {
            //RefreshHeader();
            CanvasWidth = 2000;
        }
    }
}

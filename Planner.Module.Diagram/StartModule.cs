using Planner.Module.Diagram.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram
{
    public class StartModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //throw new NotImplementedException();
            var region = containerProvider.Resolve<IRegionManager>();
            region.RegisterViewWithRegion("Diagram", typeof(StartWindow));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.Servises
{
   public class MinutedToHoure
    {
       public void  minutetohoure(int minuted,int min,int houre) {

            houre=minuted/60;
            min = minuted % 60;
        
        }
    }
}

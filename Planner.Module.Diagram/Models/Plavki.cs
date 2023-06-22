using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class Plavki : BindableBase


    {
        string _name;
        string _agregatorname;
        int _konvminutestart;
        int _konvminuteend;
        int _uvdminutestart;
        int _uvdminuteend;
        int _upkinutestart;
        int _upkinuteend;
        Houre time_Start;
        Houre time_End;
        Houre start1, start2, start3, end1, end2, end3;

        public Houre Time_Start {
            get => time_Start; set => SetProperty(ref time_Start, value);
        }

        public Houre Time_End
        {
            get => time_End;set=>SetProperty(ref time_End, value);
}
        public string AgregatorName
        {
            get => _agregatorname;set=>SetProperty(ref _agregatorname, value);
        }
        public Houre Start1
        {
            get => start1; set => SetProperty(ref start1, value);
        }
        public Houre End1
        {
            get => end1; set => SetProperty(ref end1, value);
        }
        public Houre Start2
        {
            get => start2; set => SetProperty(ref start2, value);
        }
        public Houre End3 {
            get => end3; set => SetProperty(ref end3, value);
        }
        public Houre Start3
        {
            get => start3; set => SetProperty(ref start3, value);
        }
        public Houre End2
        {
            get => end2; set => SetProperty(ref end2, value);
        }

    public  string Name { get { return _name; }
            set => SetProperty(ref _name, value);
        }
  public  int Konvminutestart
        {
            get => _konvminutestart;
            set { SetProperty(ref _konvminutestart, value);
                Start1.Houreses(value);
            }

        }
      public  int Konvminuteend
        {
            get => _konvminuteend;
            set { SetProperty(ref _konvminuteend, value);
                End1.Houreses(value);
            }

        }
    public    int Udminutestart
        {
            get => _uvdminutestart; set
            {
                SetProperty(ref _uvdminutestart, value);
                Start2.Houreses(value);
            }
            }
     public   int Udminuteend
        {
            get => _uvdminuteend; set { SetProperty(ref _uvdminuteend, value);

                End2.Houreses(value);
            }
}
public int Upkinutestart
        {
            get => _upkinutestart; set { SetProperty(ref _upkinutestart, value);
                Start3.Houreses(value);
            }
            
        }
      public  int Upkinuminuteend
        {
            get => _upkinuteend; set { SetProperty(ref _upkinuteend, value);
                End3.Houreses(value);
            }
        }
        public Plavki(string name, int konvminutestart, int konvminuteend,  int upkinutestart,  int udminutestart, int udminuteend, int upkinuminuteend)
        {
     
        
            Name = name;
            Konvminutestart = konvminutestart;
            Konvminuteend = konvminuteend;
            Udminutestart = udminutestart;
            Udminuteend = udminuteend;
            Upkinutestart = upkinutestart;
            Upkinuminuteend = upkinuminuteend;
            AgregatorName = "";
            Time_Start = new Houre(100);
            Time_End = new Houre(100);
        }
    }
}

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
        DateTime _konvminutestart;
        DateTime _konvminuteend;
        DateTime _uvdminutestart;
        DateTime _uvdminuteend;
        DateTime _upkinutestart;
        DateTime _upkinuteend;
        DateTime time_Start;
        DateTime time_End;
        string time_Start1;
        string time_End1;

        public DateTime Time_Start
        {
            get => time_Start; set
            {
                SetProperty(ref time_Start, value);
                ChangeTimeStringFormat(time_Start, Time_Start1);
            }

        }

        public DateTime Time_End
        {
            get => time_End; set
            {
                SetProperty(ref time_End, value);
                ChangeTimeStringFormat(time_End, Time_End1);
            }

        }
        public string Time_Start1
        {
            get => time_Start1; set => SetProperty(ref time_Start1, value);
        }

        public string Time_End1
        {
            get => time_End1; set => SetProperty(ref time_End1, value);
        }
        public string AgregatorName
        {
            get => _agregatorname; set => SetProperty(ref _agregatorname, value);
        }


        public string Name
        {
            get { return _name; }
            set => SetProperty(ref _name, value);
        }
        public DateTime Konvminutestart
        {
            get => _konvminutestart;
            set
            {
                SetProperty(ref _konvminutestart, value);
            }

        }
        public DateTime Konvminuteend
        {
            get => _konvminuteend;
            set
            {
                SetProperty(ref _konvminuteend, value);
            }

        }
        public DateTime Udminutestart
        {
            get => _uvdminutestart; set
            {
                SetProperty(ref _uvdminutestart, value);
            }
        }
        public DateTime Udminuteend
        {
            get => _uvdminuteend; set
            {
                SetProperty(ref _uvdminuteend, value);
            }
        }
        public DateTime Upkinutestart
        {
            get => _upkinutestart; set
            {
                SetProperty(ref _upkinutestart, value);

            }

        }
        public DateTime Upkinuminuteend
        {
            get => _upkinuteend; set
            {
                SetProperty(ref _upkinuteend, value);

            }
        }
        public Plavki(string name, int ksh, int ksm, int keh, int kem, int udmsh, int udmsm, int udmeh, int udesm, int upksh, int upksm, int upkeh, int upkesm)
        {



            Name = "";
            Konvminutestart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ksh, ksm, 0);
            Konvminuteend = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ksh, ksm, 0);
            Udminutestart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ksh, ksm, 0);
            Udminuteend = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ksh, ksm, 0);
            Upkinutestart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ksh, ksm, 0);
            Upkinuminuteend = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ksh, ksm, 0);
            AgregatorName = "КОНВ";
            Time_Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            Time_End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 50, 0);
            Time_Start1 = "";
            Time_End1 = "";
            ChangeTimeStringFormat(Time_Start, Time_Start1);
            ChangeTimeStringFormat(Time_End, Time_End1);
        }
        private void ChangeTimeStringFormat(DateTime a, string b)
        {

            b = a.ToString("HH:mm");

        }
    }
}

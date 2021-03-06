﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breaker
{
    class Settings
    {
        //Load and Save Interval
        public void SaveInterval(int intervalMiliseconds)
        {
            Properties.Settings.Default["Interval"] = intervalMiliseconds;
            Properties.Settings.Default.Save();
        }

        public int LoadInterval()
        {
            return Properties.Settings.Default.Interval;
        }

        //Load and Save MainMessage
        public void SaveMainMessage(string mainMessage)
        {
            Properties.Settings.Default["MainMessage"] = mainMessage;
            Properties.Settings.Default.Save();
        }

        public string LoadMainMessage()
        {
            return Properties.Settings.Default["MainMessage"].ToString();
        }

        public void SaveActiveDay(string day)
        {
            Properties.Settings.Default.ActiveDays.Add(day);
            Properties.Settings.Default.Save();
        }

        public void RemoveActiveDay(string day)
        {
            Properties.Settings.Default.ActiveDays.Remove(day);
            Properties.Settings.Default.Save();
        }

        public System.Collections.Specialized.StringCollection LoadActiveDays()
        {
            return Properties.Settings.Default.ActiveDays;
        }
    }
}

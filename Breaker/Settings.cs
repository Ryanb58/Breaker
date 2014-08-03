using System;
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
            return Convert.ToInt32(Properties.Settings.Default["Interval"]);
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
    }
}

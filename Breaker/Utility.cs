using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breaker
{
    class Utility
    {
        public int miliSecondsToMinutes(int miliSeconds)
        {
            int minutes = ( miliSeconds / 1000 ) / 60;
            if (minutes < 1)
            {
                minutes = 1;
            }
            else if (minutes > 59)
            {
                minutes = 59;
            }
            return minutes;
        }

        public int? minutesToHours(int minutes)
        {
            if(minutes >= 60)
            {
                int hours = (minutes / 60);
                return hours;
            }
            else
            {
                return null;
            }
        }

        public int minutesToMiliseconds(int minutes)
        {
            return minutes * 60 * 1000;
        }
    }
}

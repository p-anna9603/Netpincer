using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    class workingSchedule
    {
        int workingHoursID;
        int fromHour, fromMinute;
        int toHour, toMinute;
        string workingDays;

        public workingSchedule(int ID, int fromH, int toH, int fromM, int toM, string workinDays)
        {
            workingHoursID = ID;
            fromHour = fromH;
            toHour = toH;
            fromMinute = fromM;
            toMinute = toM;
            workingDays = workinDays;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class workingSchedule
    {
        int workingHoursID;
        int fromHour, fromMinute;
        int toHour, toMinute;
        string workingDays;
        List<int> workingDaysInInt = new List<int>();

        public workingSchedule(int ID, int fromH, int toH, int fromM, int toM, string workinDays)
        {
            workingHoursID = ID;
            fromHour = fromH;
            toHour = toH;
            fromMinute = fromM;
            toMinute = toM;
            workingDays = workinDays;
            string[] wDays = workingDays.Split(',');
            for (int i = 0; i < wDays.Length; ++i)
            {
                workingDaysInInt.Add(Int32.Parse(wDays[i]));
            }
        }

        public int WorkingHoursID { get => workingHoursID; set => workingHoursID = value; }
        public int FromHour { get => fromHour; set => fromHour = value; }
        public int FromMinute { get => fromMinute; set => fromMinute = value; }
        public int ToHour { get => toHour; set => toHour = value; }
        public int ToMinute { get => toMinute; set => toMinute = value; }
        public string WorkingDays { get => workingDays; set => workingDays = value; }
        //public List<int> WorkingDaysInInt { get => workingDaysInInt; set => workingDaysInInt = value; }
    }
}

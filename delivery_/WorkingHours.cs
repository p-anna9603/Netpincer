using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delivery_
{
	public class WorkingHours
	{
		string fromHour;
		string fromMinute;
		string toHour;
		string toMinute;
		string workingDays;
		string type;
		string username;

		public WorkingHours(string fromH, string fromM, string toH, string toM, string workD, string type_, string username_)
		{
			fromHour = fromH;
			fromMinute = fromM;
			toHour = toH;
			toMinute = toM;
			workingDays = workD;
			type = type_;
			username = username_;
		}

        public string FromHour { get => fromHour; set => fromHour = value; }
        public string FromMinute { get => fromMinute; set => fromMinute = value; }
        public string ToHour { get => toHour; set => toHour = value; }
        public string ToMinute { get => toMinute; set => toMinute = value; }
        public string WorkingDays { get => workingDays; set => workingDays = value; }
        public string Type { get => type; set => type = value; }
        public string Username { get => username; set => username = value; }
    }
}

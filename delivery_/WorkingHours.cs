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

	}
}

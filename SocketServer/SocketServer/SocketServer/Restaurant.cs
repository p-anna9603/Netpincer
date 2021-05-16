using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
	public class Restaurant
	{
		public int Type;
		public int restaurantID { get; set; }
		public string city { get; set; }
		public string zipcode { get; set; }
		public string line1 { get; set; }
		public string line2 { get; set; }
		public int fromHour { get; set; }
		public int fromMinute { get; set; }
		public int toHour { get; set; }
		public int toMinute { get; set; }
		public string name { get; set; }
		public string restaurantDescription { get; set; }
		public string style { get; set; }
		public string owner { get; set; }
		public string pass { get; set; }
		public string email { get; set; }
		public string phoneNumber { get; set; }
		public string lastName { get; set; }
		public string firstName { get; set; }
		public int approximateTime { get; set; }


		public Restaurant() { }

		public Restaurant(string city_, string zipcode_, string line1_, string line2_,
			int fromHour_, int fromMinute_, int toHour_, int toMinute_,
			string name_, string restaurantDescription_, string style_,
			string username_, string phoneNumber_,int approxTime, int restID = 0,
			string lastName = "", string firstName = "", string pass_ = "", string email_ = "")
		{
			this.restaurantID = restID;
			this.city = city_;
			this.zipcode = zipcode_;
			this.line1 = line1_;
			this.line2 = line2_;
			this.fromHour = fromHour_;
			this.fromMinute = fromMinute_;
			this.toHour = toHour_;
			this.toMinute = toMinute_;
			this.name = name_;
			this.restaurantDescription = restaurantDescription_;
			this.style = style_;
			this.owner = username_;
			this.pass = pass_;
			this.phoneNumber = phoneNumber_;
			this.email = email_;
			this.lastName = lastName;
			this.firstName = firstName;
			approximateTime = approxTime;
			Type = 11;
		}

		public string toString()
		{
			return "Owner: " + owner +
				"\nRestaurant Name: " + name + "\nDescription: " + restaurantDescription +
				"\nStyle: " + style + "\nOpening Hours: " + fromHour
				+ ":" + fromMinute + " - " + toHour + ":" + toMinute
				+ "\nAddress\nCity:" + city + "(" + zipcode + ")" + "\n" + line1 + "\n" + line2
				+ "\nPass: " + pass + "\nPhoneNumber: " + phoneNumber + "\nEmail: " + email;
		}
	}
}

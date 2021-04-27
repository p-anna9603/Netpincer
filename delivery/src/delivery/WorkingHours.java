package delivery;

public class WorkingHours {
	public String fromHour;
	public String fromMinute;
	public String toHour;
	public String toMinute;
	public String workingDays;
	public String type;
	public String username;


	public WorkingHours() {}
	
	public WorkingHours(String fromH, String fromM, String toH, String toM, String workD, String type_, String username_) {
		fromHour = fromH;
		fromMinute = fromM;
		toHour = toH;
		toMinute = toM;
		workingDays = workD;
		type=type_;
		username=username_;
	}
}

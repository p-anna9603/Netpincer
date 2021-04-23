
let User = class
{
    constructor(type,clientID,username, password, firstName, phoneNumber, city, zipcode, line1, line2, userType, email) {
        this.type = type;
        this.clientID = clientID;
        this.username = username;
        this.password = password;
        this.firstName = firstName;
        this.phoneNumber = phoneNumber;
        this.city = city;
        this.zipcode = zipcode;
        this.line1 = line1;
        this.line2 = line2;
        this.userType = userType;
        this.email = email;
      }   
}
let Restaurant = class {
    constructor(restaurantID, name, restaurantDesc, style, owner, phonenumber, city, zipcode, line1, line2, fromHour,toHour, toMinute)
    {
        this.RestaurantID=restaurantID;
        this.Name = name;
        this.RestaurantDesc = restaurantDesc;
        this.Stlye = style;
        this.Owner = owner;
        this.PhoneNumber = phonenumber;
        this.City = city;
        this.Zipcode = zipcode;
        this.Line_1 = line1;
        this.Line_2 = line2;
        this.FromHour = fromHour;
        this.ToHour = toHour;
        this.ToMinute = toMinute;
    }
}
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
    static loggedin()
    {
        return true;
    }
        
}

class Orders
{

}
package delivery;


    public class User
    {
        String username;
        String password;
        String lastName;
        String firstName;
        String phoneNumber;
        String city;
        String zipcode;
        String line1;
        String line2;
        public int userType;
        public String email;
        public String type;

        public User() { }

        public User(String _username, String _password, String _lastName, String _firstName, String _phoneNumber,
             String _city, String _zipcode, String _line1, String _line2, int _userTypeId, String email_, String type_)
        {
            username = _username;
            password = _password;
            lastName = _lastName;
            firstName = _firstName;
            phoneNumber = _phoneNumber;
            city = _city;
            zipcode = _zipcode;
            line1 = _line1;
            email = email_;
            type = type_;
            userType = 2;
            if (_line2 == "null")
                line2 = "";
            else
                line2 = _line2;
            /*switch (_userTypeId)
            {
                case 0:
                    userType = UserType.Customer;
                    break;
                case 1:
                    userType = UserType.RestaurantOwner;
                    break;
                case 2:
                    userType = UserType.DeliveryPerson;
                    break;
                default:
                    System.out.println("Undefined User type");
                    break;
            }
            */

        }

        public String toString()
        {
            return "username: " + username + " \npassword: " + password + " \nlastName: " + lastName
            + "\nfirstName: " + firstName + "\nphoneNumber: " + phoneNumber + "\ncity: " + city +
            "\nzipcode: " + zipcode + "\nline1: " + line1 + "\nline2: " + line2 + "\nuserType:" + userType;
        }

        public String getUsername() { return username; }
        public String getPassword() { return password; }
        public String getLastName() { return lastName; }
        public String getFirstName() { return firstName; }
        public String getPhoneNumber() { return phoneNumber; }
        public String getCity() { return city; }
        public String getZipcode() { return zipcode; }
        public String getLine1() { return line1; }
        public String getLine2() { return line2; }
        //String
        //"I'm not gonna code in C++" they said.
        //"C# is way better" they said.
        //"I Don't understand C++" I said
        //"Orosz Ákos laughed" 

    }




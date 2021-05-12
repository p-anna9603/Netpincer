using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public enum UserType { Customer, RestaurantOwner, DeliveryPerson };
    public class User
    {
        [Newtonsoft.Json.JsonProperty]
        private string username;
        [Newtonsoft.Json.JsonProperty]
        private string password;
        [Newtonsoft.Json.JsonProperty]
        private string lastName;
        [Newtonsoft.Json.JsonProperty]
        private string firstName;
        [Newtonsoft.Json.JsonProperty]
        private string phoneNumber;
        [Newtonsoft.Json.JsonProperty]
        private string city;
        [Newtonsoft.Json.JsonProperty]
        private string zipcode;
        [Newtonsoft.Json.JsonProperty]
        private string line1;
        [Newtonsoft.Json.JsonProperty]
        private string line2;
        [Newtonsoft.Json.JsonProperty]
        UserType userType;
        [Newtonsoft.Json.JsonProperty]
        private string email;

        public UserType GetUserType { get => userType; set => userType = value; }
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }

        public User() { }

        public User(string _username, string _password, string _lastName, string _firstName, string _phoneNumber,
             string _city, string _zipcode, string _line1, string _line2, int _userTypeId, string email_)
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
            if (_line2 == "null")
                line2 = "";
            else
                line2 = _line2;
            switch (_userTypeId)
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
                    Console.WriteLine("Undefined User type");
                    break;
            }

        }

        public string toString()
        {
            return "username: " + username + " \npassword: " + password + " \nlastName: " + lastName
            + "\nfirstName: " + firstName + "\nphoneNumber: " + phoneNumber + "\ncity: " + city +
            "\nzipcode: " + zipcode + "\nline1: " + line1 + "\nline2: " + line2 + "\nuserType:" + userType.ToString();
        }

        public string getUsername() { return username; }
        public string getPassword() { return password; }
        public string getLastName() { return lastName; }
        public string getFirstName() { return firstName; }
        public string getPhoneNumber() { return phoneNumber; }
        public string getCity() { return city; }
        public string getZipcode() { return zipcode; }
        public string getLine1() { return line1; }
        public string getLine2() { return line2; }
    }
}



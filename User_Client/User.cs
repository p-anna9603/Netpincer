using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
        public class User
        {
            private string username;
            private string password;
            private string lastName;
            private string firstName;
            private string phoneNumber;
            private string city;
            private string zipcode;
            private string line1;
            private string line2;

            public User() { }

            public User(string _username, string _password, string _lastName, string _firstName, string _phoneNumber,
                 string _city, string _zipcode, string _line1, string _line2)
            {
                username = _username;
                password = _password;
                lastName = _lastName;
                firstName = _firstName;
                phoneNumber = _phoneNumber;
                city = _city;
                zipcode = _zipcode;
                line1 = _line1;
                line2 = _line2;
            }

            public string toString() 
            {
            return "username: " + username + " \npassword: " + password + " \nlastName: " + lastName
                + "\nfirstName: " + firstName + "\nphoneNumber: " + phoneNumber + "\ncity: " + city+
                "\nzipcode: " + zipcode+ "\nline1: " + line1+ "\nline2: " + line2;
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
            //"I'm not gonna code in C++" they said.
            //"C# is way better" they said.
            //"I Don't understand C++" I said
            //"Orosz Ákos laughed" 

        }
 }


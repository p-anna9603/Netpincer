﻿using System;
using RestaurantClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodOrderClient
{
    /// <summary>
    /// Interaction logic for RestaurantRegister.xaml
    /// </summary>
    public partial class RestaurantRegister : Window
    {
        int fromHour = 0;
        int toHour = 0;
        int fromMin = 0;
        int toMin = 0;
        Regex regex = new Regex("[^0-9]+");
        Regex regexChar = new Regex("[^a-zA-ZÀ-ÿ-ő-ű]+");

        string restName; // restaurant name
        string userName;
        string email;
        string password;
        string phone;
        string zipCode;
        string citiName;
        string street;
        string streetNum;
        string leiras;
        string style;
        public string line2 { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public ConnectToServer ServerConnection;
        public RestaurantRegister()
        {
            InitializeComponent();
            // CONNECTING TO SERVER--Not Gonna work without the database!
            ServerConnection = new ConnectToServer();
            Console.WriteLine(ServerConnection.getUser("testUser", "t3stpassword", UserType.Customer).toString());           //OK
           //  ServerConnection.registerUser(new User("userFromClient", "ass", "Flex", "Elek", "+3699145825", "Veszprem", "8200", "Ass utca 6", "2/A", 1, "imel@gmail.com"));
           //   Console.WriteLine(ServerConnection.getUser("testUser", "t3stpassword", UserType.RestaurantOwner).toString());   //NOT FOUND RETURNS ERROR MESSAGE
            Console.WriteLine(ServerConnection.getUser("testRestaurantOwner", "r3staurant", UserType.RestaurantOwner).toString());  //OK
          //   ServerConnection.registerRestaurant(new Restaurant("Veszprem", "8200", "Faradt vagyok utca v2.0", "3/A", 10, 00, 23, 00, "Utalom a C capat", "Hosszabb leiras arrol, mennyire utlaom a Csharpot", "C capa", "Hiiiii", "Jelszoo", "aasd@gmail.com", "+36214563217", "Pistavok", "Tscoo"));

        }
        public void registerRestaurant()
        {
            ServerConnection.registerRestaurant(new Restaurant(citiName, zipCode, street, streetNum, fromHour, fromMin, toHour, toMin, restName, leiras,
               style, userName, password, email, phone, lastName, firstName));
            ServerConnection.StopClient();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
        //    Login login = new Login();
       //     login.Show();
       //     Close();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public void Reset()
        {
            textBoxRestName.Text = "";
            textBoxUserName.Text = "";
            textBoxEmail.Text = "";
          //  textBoxAddress.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
            textBoxCity.Text = "Város";
            textBoxStreet.Text = "Utca";
            textBoxStreetNum.Text = "Hsz";
            textBoxZip.Text = "Irsz.";
            textBoxPhone.Text = "36-";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void fromHour_TextChanged(object sender, TextCompositionEventArgs e)
        {
            TextBox fromHour = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
        }
        int zipKeyDone = 0;
        int cityKeyDone = 0;
        int streetKeyDone = 0;
        int streetNumKeyDone = 0;
        private void zipCode_KeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
            if(textBoxZip.Text.Length == 4)
            {
                e.Handled = true;
            }
            zipKeyDone = 1;
        }
        private void city_KeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
          
            e.Handled = regexChar.IsMatch(e.Text);
            cityKeyDone = 1;
        }
        private void street_KeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            e.Handled = regexChar.IsMatch(e.Text);
            streetKeyDone = 1;
        }
        private void houseNum_KeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
            streetNumKeyDone = 1;
        }
        private void addressGotFocus(object sender, EventArgs e)
        {
            TextBox ad = sender as TextBox;         
            if(ad.Name.Equals(textBoxZip.Name) && zipKeyDone == 0 ||
                ad.Name.Equals(textBoxCity.Name) && cityKeyDone == 0 ||
                 ad.Name.Equals(textBoxStreet.Name) && streetKeyDone == 0 ||
                  ad.Name.Equals(textBoxStreetNum.Name) && streetNumKeyDone == 0)
            {
                ad.Text = "";
            }
        }
        private void addressLostFocus(object sender, EventArgs e)
        {
            TextBox ad = sender as TextBox;
            if (ad.Name.Equals(textBoxZip.Name) && ad.Text.Length == 0)
            {
                ad.Text = "Irsz.";
                zipKeyDone = 0;
            }
            if (ad.Name.Equals(textBoxCity.Name) && ad.Text.Length == 0)
            {
                ad.Text = "Város";
                cityKeyDone = 0;
            }
            if (ad.Name.Equals(textBoxStreet.Name) && ad.Text.Length == 0)
            {
                ad.Text = "Utca";
                cityKeyDone = 0;
            }
            if (ad.Name.Equals(textBoxStreetNum.Name) && ad.Text.Length == 0)
            {
                ad.Text = "Hsz.";
                cityKeyDone = 0;
            }
        }
        private void fromMinutes_TextChanged(object sender, TextCompositionEventArgs e)
        {
            TextBox fromMin = sender as TextBox;
       
            //if (!char.IsControl(Char.Parse(e.KeyCha)) && !char.IsDigit(Char.Parse(e.ToString())) &&
            //  Char.Parse(e.ToString()) != '.' && Char.Parse(e.ToString()) != ',')
            //{
            //    e.Handled = true;
            //    Console.WriteLine("itt");
            //}

            e.Handled = regex.IsMatch(e.Text);
        }
        private void toHours_TextChanged(object sender, TextCompositionEventArgs e)
        {
            TextBox toHour = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);

        }
        private void toMinutes_TextChanged(object sender, TextCompositionEventArgs e)
        {
            TextBox toMinute = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Adja meg az e-mail címét!";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Az email-cím helytelen!";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                restName = textBoxRestName.Text; // restaurant name
                userName = textBoxUserName.Text;
                email = textBoxEmail.Text;
                password = passwordBox1.Password;
                phone = textBoxPhone.Text;
                zipCode = textBoxZip.Text;
                citiName = textBoxCity.Text;
                street = textBoxStreet.Text;
                streetNum = textBoxStreetNum.Text;
                leiras = textBoxDesc.Text;
                style = textBoxStyle.Text;
                lastName = lastNameText.Text;
                firstName = firstNameText.Text;
                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Adja meg a jelszót!";
                    passwordBox1.Focus();
                }
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage.Text = "Adja meg a megerősítő jelszót!";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "A két jelszó nem egyezik!";
                    passwordBoxConfirm.Focus();
                }
                else if (isPhoneNumCorrect() != 1)
                {
                    errormessage.Text = "Helytelen telefonszám!";
                    textBoxPhone.Focus();
                }
                else if (isAddressValid() != 1)
                {
                    if(isAddressValid() == -1)
                    {
                        errormessage.Text = "Helytelen irányítószám!";
                        textBoxZip.Focus();
                    }
                }
                else if (isHourValid() != 1)
                {
                    errormessage.Text = "A nyitvatartás helytelen!";
                    fromWork.Focus();
                }
                else
                {
                    errormessage.Text = "";
                    // string address = textBoxAddress.Text;
                    //SqlConnection con = new SqlConnection("Data Source=TESTPURU;Initial Catalog=Data;User ID=sa;Password=wintellect");
                    //con.Open();
                    //SqlCommand cmd = new SqlCommand("Insert into Registration (FirstName,LastName,Email,Password,Address) values('" + firstname + "','" + lastname + "','" + email + "','" + password + "','" + address + "')", con);
                    //cmd.CommandType = CommandType.Text;
                    //cmd.ExecuteNonQuery();
                    //con.Close();
                    registerRestaurant();
                    errormessage.Text = "Regisztrácó sikeres";
                    Reset();
                }
            }
        }

        private int isPhoneNumCorrect()
        {
            int retVal = 0;
            if (textBoxPhone.Text.Length != 3)
            {
                string phone = textBoxPhone.Text;
                char[] phoneArr = phone.ToCharArray(3, phone.Length - 3);
                string elohivo = (phoneArr.ElementAt(0).ToString() + phoneArr.ElementAt(1).ToString());
                Console.WriteLine(string.Join("",phoneArr));
                Console.WriteLine(elohivo);
                Console.WriteLine(phoneArr.ElementAt(0));
                if (elohivo.Equals("20") || elohivo.Equals("30") || elohivo.Equals("70"))
                {
                    Console.WriteLine("iott");
                    var r = new Regex(@"^\d{2}\d{7}$");
                    if (r.IsMatch(string.Join("", phoneArr)))
                    {
                        retVal = 1;
                        Console.WriteLine("helyes magán");
                    }

                }
                else if(phoneArr.ElementAt(0).Equals('1')) // vezetékes pesti
                {
                    var r = new Regex(@"^\d{1}\d{7}$");
                    if (r.IsMatch(string.Join("", phoneArr)))
                    {
                        retVal = 1;
                        Console.WriteLine("helyes pesti");
                    }
                }
                else // vezetékes vidéki
                {
                    var r = new Regex(@"^\d{2}\d{6}$");
                    if (r.IsMatch(string.Join("", phoneArr)))
                    {
                        retVal = 1;
                        Console.WriteLine("helyes vidéki");
                    }
                }
            }
            return retVal;
        }

        private int isAddressValid()
        {
            int retval = 0;
            if(textBoxZip.Text.Length != 4 && textBoxZip.Text.StartsWith("I"))
            {
                retval = -1;
            }
            else
            {
                retval = 1;
            }
            return retval;
        }
        private int isHourValid()
        {
            int retval = 0;
            char[] from = fromWork.Value.ToString().ToCharArray(14, toWork.Value.ToString().Length - 15);
            string[] allFrom = string.Join("", from).Split(':');
            for (int i = 0; i < allFrom.Length; i++)
            {
                allFrom[i] = allFrom[i].Trim();
            }
            char[] to = toWork.Value.ToString().ToCharArray(14, toWork.Value.ToString().Length - 15);
            string[] allTo = string.Join("", to).Split(':');
            for (int i = 0; i < allTo.Length; i++)
            {
                allTo[i] = allTo[i].Trim();
            }

            fromHour = Int32.Parse(allFrom[0]);
            fromMin = Int32.Parse(allFrom[1]);
            toHour = Int32.Parse(allTo[0]);
            toMin = Int32.Parse(allTo[1]);

            //Console.WriteLine(fromHour);          
            //Console.WriteLine(fromMin);
            //Console.WriteLine(toHour);
            //Console.WriteLine(toMin);
            if((fromHour < toHour) || (fromHour <= toHour && fromMin < toMin))
            {
                retval = 1;
            }
            return retval;
        }
    }
}

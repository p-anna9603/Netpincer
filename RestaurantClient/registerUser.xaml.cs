using System;
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
using System.Windows.Shapes;

namespace RestaurantClient
{
    /// <summary>
    /// Interaction logic for registerUser.xaml
    /// </summary>
    public partial class registerUser : Window
    {
        Regex regex = new Regex("[^0-9]+");
        Regex regexChar = new Regex("[^a-zA-ZÀ-ÿ-ő-ű]+");

        public ConnectToServer ServerConnection;
        startupWindow parent;

        string restName; // restaurant name
        string userName;
        string email;
        string password;
        string phone;
        string zipCode;
        string citiName;
        string street;
        string streetNum;
        string floor;
        string lastName;
        string firstName;
        int retval;

        public registerUser(ConnectToServer ServerCon, startupWindow startupWin)
        {
            InitializeComponent();
            ServerConnection = ServerCon;
            parent = startupWin;
        }

        public void registerUserToServer()
        {
            //ServerConnection.registerRestaurant(new Restaurant(citiName, zipCode, street, streetNum, fromHour, fromMin, toHour, toMin, restName, leiras,
            //   style, userName, password, email, phone, lastName, firstName));
            //ServerConnection.StopClient();
         //   ServerConnection = new ConnectToServer();
            User usr = new User(userName, password, lastName, firstName, phone, citiName, zipCode, street + " " + streetNum, floor, 0, email);
            retval = ServerConnection.registerUser(usr);
            Console.WriteLine("retvaaal:" + retval);
        }

        public void Reset()
        {
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
        int zipKeyDone = 0;
        int cityKeyDone = 0;
        int streetKeyDone = 0;
        int streetNumKeyDone = 0;
        int floorNumKeyDone = 0;

        private void phone_KeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            string phoneUntil = ad.Text;
            ad.Text = phoneUntil;
            e.Handled = regex.IsMatch(e.Text);
        }
        private void zipCode_KeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
            if (textBoxZip.Text.Length == 4)
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
        private void floorNum_KeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            floorNumKeyDone = 1;
        }
        private void addressGotFocus(object sender, EventArgs e)
        {
            TextBox ad = sender as TextBox;
            if (ad.Name.Equals(textBoxZip.Name) && zipKeyDone == 0 ||
                ad.Name.Equals(textBoxCity.Name) && cityKeyDone == 0 ||
                 ad.Name.Equals(textBoxStreet.Name) && streetKeyDone == 0 ||
                  ad.Name.Equals(textBoxStreetNum.Name) && streetNumKeyDone == 0 ||
                  ad.Name.Equals(floorText.Name) && floorNumKeyDone == 0)
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
            if (ad.Name.Equals(floorText.Name) && ad.Text.Length == 0)
            {
                ad.Text = "Emelet / Ajtó";
                floorNumKeyDone = 0;
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
                Console.WriteLine(string.Join("", phoneArr));
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
                else if (phoneArr.ElementAt(0).Equals('1')) // vezetékes pesti
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
                userName = textBoxUserName.Text;
                email = textBoxEmail.Text;
                password = passwordBox1.Password;
                phone = textBoxPhone.Text;
                zipCode = textBoxZip.Text;
                citiName = textBoxCity.Text;
                street = textBoxStreet.Text;
                streetNum = textBoxStreetNum.Text;
                lastName = lastNameText.Text;
                firstName = firstNameText.Text;
                floor = floorText.Text;
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
                    if (isAddressValid() == -1)
                    {
                        errormessage.Text = "Helytelen irányítószám!";
                        textBoxZip.Focus();
                    }
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
                    registerUserToServer();
                    if(retval == 1)
                    {
                        Console.WriteLine("sikeres regisztracio");
                        errormessage.Text = "Regisztrácó sikeres";
                        Reset();
                        parent.Show();
                        this.Close();
                    }
                    else if(retval == 0)
                    {
                        Console.WriteLine("felhasználóó foglaaaaaalt");
                        errormessage.Text = "Felhasználó név foglalt";
                        textBoxUserName.Focus();
                    }
                }
            }
        }
        private int isAddressValid()
        {
            int retval = 0;
            if (textBoxZip.Text.Length != 4 && textBoxZip.Text.StartsWith("I"))
            {
                retval = -1;
            }
            else
            {
                retval = 1;
            }
            return retval;
        }

        private void window_sizeChanged(object sender, SizeChangedEventArgs e)
        {

            Window w = sender as Window;
            backgroundBorder.Width = e.NewSize.Width;
            backgroundBorder.Height = e.NewSize.Height;
            //LinearGradientBrush myVerticalGradient = new LinearGradientBrush();
            //myVerticalGradient.GradientStops.Add(
            //     new GradientStop(Colors.Yellow, 0.0));
            //myVerticalGradient.GradientStops.Add(
            //    new GradientStop(Colors.Red, 0.25));
            //backgroundBorder.Child = myVerticalGradient;
            Console.WriteLine("resiize: " + e.NewSize.Width + ", " + e.NewSize.Height);
            Console.WriteLine("back: " + backgroundBorder.Width + ", " + backgroundBorder.Height);
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            string message = "Biztosan kiszeretne lépni a regisztrációból?";
            string caption = "Kilépés";
            var result = MessageBox.Show(message, caption,
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                parent.Show();
                this.Close();
            }
        }
    }
}

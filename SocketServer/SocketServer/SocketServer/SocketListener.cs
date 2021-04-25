using System;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

/** JSON FORMAT
 *  {type: 0,      -> 0 - Basic Server Info
 *  msgID: 0,
 *  clientID: 0 }
 */

namespace SocketServer
{
    class SocketListener
    {
        private SqlConnection DatabaseConnection;
        private int clientID;
        private List<int> listOfConnectedClients;
        private Socket listener;
        Socket handler;
        public SocketListener()
        {
            DatabaseConnection = ConnectToDatabase();
            Console.WriteLine("Connection State: {0}", DatabaseConnection.State);
            clientID = 0;
            listOfConnectedClients = new List<int>();
            StartServer();
            DatabaseConnection.Close();
        }
        public static void Connect_Try(string host, int port)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(host);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Establishing Connection to {0}",
                host);
            s.Connect(IPs[0], port);
            Console.WriteLine("Connection established");
        }
        private bool Test_Connection()
        {
            
            try
            {
                Connect_Try("localhost", 11000);
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
            return true;
        }

        public void StartServer()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            #region Klau
            //// Get Host IP Address that is used to establish a connection  
            //// In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            //// If a host has multiple addresses, you will get a list of addresses  
            //IPHostEntry host = Dns.GetHostEntry("localhost");
            //IPAddress ipAddress = host.AddressList[0];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            #endregion



            try
            {
                #region Klau
                //// Create a Socket that will use Tcp protocol      
                //listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                //// A Socket must be associated with an endpoint using the Bind method  
                //listener.Bind(localEndPoint);
                //// Specify how many requests a Socket can listen before it gives Server busy response.  
                //// We will listen 10 requests at a time  
                //listener.Listen(10);

                //// getFoods("7", "1");
                ////getRestaurantsList();
                ///*JObject test = new JObject();
                //test.Add("name","Pizza pizza");
                //test.Add("price", "600");
                //test.Add("rating", "3.4");
                //test.Add("categoryID", "3");
                //test.Add("restaurantID", "7");
                //test.Add("availableFrom", "");
                //test.Add("availableTo", "");
                //addFood(test);*/
                //waitingForConnection();

                //// Incoming data from the client.    
                //string data = null;
                //byte[] bytes = null;
                //bool shutdown = false;
                #endregion

                #region Marci
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000));
                    socket.Listen(10);
                    waitingForConnection_teszt(socket);

                    string data = null;
                    byte[] bytes = null;
                    bool shutdown = false;
                #endregion

                while (!shutdown)
                {
                    #region Klau
                    //while (true)
                    //{
                    //    //if (handler.Poll(1000,SelectMode.SelectRead))
                    //    {
                    //        //    waitingForConnection();
                    //    }
                    //    bytes = new byte[4096];
                    //    int bytesRec = handler.Receive(bytes);
                    //    //Console.WriteLine("bytes recieved {0}", bytesRec);
                    //    if (bytesRec != 0)
                    //    {
                    //        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //        //data = Encoding.GetEncoding("windows-1250").GetString(bytes);
                    //        break;
                    //    }
                    //}
                    #endregion
                    #region Marci

                    while (true)
                    {
                        bytes = new byte[4096];
                        int bytesRec = handler.Receive(bytes);
                        if (bytesRec != 0)
                        {
                            data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            break;
                        }
                    }

                    #endregion



                    //RECEIVED NEW BYTES
                    try
                    {
                        JObject receivedJSONObject = new JObject();
                        receivedJSONObject = JObject.Parse(data);
                        Console.WriteLine("Received JSON: {0}", receivedJSONObject.ToString());
                        if (receivedJSONObject["type"].ToString() == "0")  //First Connection From Client
                        {
                            //Console.WriteLine("First message");
                            JObject sendJason = new JObject();
                            sendJason = sendFirstConnectionInfo();
                            handler.Send(Encoding.ASCII.GetBytes(sendJason.ToString()));
                            // handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(sendJason.ToString()));  //Sending Client ID
                            // listOfConnectedClients.Add(clientID - 1); //Adding client to connected list
                        }
                        else if (receivedJSONObject["type"].ToString() == "1") //Get User Information
                        {
                            JObject header = getClientHeader(receivedJSONObject);  //1st JObj  --Header
                            string userInfo = getUserInfo(receivedJSONObject["username"].ToString(), receivedJSONObject["password"].ToString(), receivedJSONObject["userType"].ToString());
                            userInfo = userInfo.Replace("[", "");
                            userInfo = userInfo.Replace("]", "");
                            JObject userJason = JObject.Parse(userInfo);    //2nd JObj  --Body
                            Console.WriteLine("BODY: {0}", userJason);
                            //JsonConvert.DeserializeObject(userInfo);
                            header.Merge(userJason);
                            Console.WriteLine("MERGED JSON: {0}", header.ToString());
                            handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(header.ToString()));
                        }
                        else if (receivedJSONObject["type"].ToString() == "2") //Get Restaurant Information
                        {
                            JObject header = getClientHeader(receivedJSONObject);  //1st JObj  --Header
                            string userInfo = getRestaurant(receivedJSONObject["username"].ToString());
                            userInfo = userInfo.Replace("[", "");
                            userInfo = userInfo.Replace("]", "");
                            JObject userJason = JObject.Parse(userInfo);    //2nd JObj  --Body
                            Console.WriteLine("BODY: {0}", userJason);
                            //JsonConvert.DeserializeObject(userInfo);
                            header.Merge(userJason);
                            Console.WriteLine("MERGED JSON: {0}", header.ToString());
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(header.ToString()));
                            handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
                        }
                        else if (receivedJSONObject["type"].ToString() == "4") // Register User
                        {
                            string register = registerUser(receivedJSONObject);
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                            handler.Send(Encoding.ASCII.GetBytes(register));
                        }
                        else if (receivedJSONObject["type"].ToString() == "5") //Register Restaurant
                        {
                            string register = registerRestaurant(receivedJSONObject);
                            handler.Send(Encoding.ASCII.GetBytes(register));
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                        }
                        else if (receivedJSONObject["type"].ToString() == "7") //Get list of Categories
                        {
                            string register = getCategories(receivedJSONObject["restaurantID"].ToString());
                            handler.Send(Encoding.ASCII.GetBytes(register));
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                        }
                        else if (receivedJSONObject["type"].ToString() == "8") //Add category
                        {
                            string register = addCategory(receivedJSONObject);
                            handler.Send(Encoding.ASCII.GetBytes(register));
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                        }
                        else if (receivedJSONObject["type"].ToString() == "9") //9 - Get list of Food
                        {
                            string register = getFoods(receivedJSONObject["restaurantID"].ToString(), receivedJSONObject["categoryID"].ToString());
                            handler.Send(Encoding.ASCII.GetBytes(register));
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                        }
                        else if (receivedJSONObject["type"].ToString() == "10") //10 - Add Food
                        {
                            string register = addFood(receivedJSONObject);
                            handler.Send(Encoding.ASCII.GetBytes(register));
                        }
                        else if (receivedJSONObject["type"].ToString() == "11") //11 - Get list of Restaurants
                        {
                            string register = getRestaurantsList();
                            handler.Send(Encoding.ASCII.GetBytes(register));
                            //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                        }




                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());

                    }


                    //Console.WriteLine("Text received : {0}", data);



                    //byte[] msg = Encoding.ASCII.GetBytes(data);
                    //byte[] msg = Encoding.ASCII.GetBytes(data);
                    //handler.Send(msg);
                    //if (jasonObject["type"].ToString()=="-1")
                    //{
                    //    shutdown = true;
                    //    handler.Shutdown(SocketShutdown.Both);
                    //    handler.Close();
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        public void Start_Teszt()
        {
                    //    try
                    //    {
               
                    //        while (!shutdown)
                    //        {
                   
                    //            try
                    //            {
                    //                JObject receivedJSONObject = new JObject();
                    //                receivedJSONObject = JObject.Parse(data);
                    //                Console.WriteLine("Received JSON: {0}", receivedJSONObject.ToString());
                    //                if (receivedJSONObject["type"].ToString() == "0")  //First Connection From Client
                    //                {
                    //                    JObject sendJason = new JObject();
                    //                    sendJason = sendFirstConnectionInfo();
                    //                    handler.Send(Encoding.ASCII.GetBytes(sendJason.ToString()));
                    //                }
                    //            }
                    //            catch
                    //            {
                    //            }

                    //        }

                    //    }
                    //    catch
                    //    {
                    //    }
                    //Console.WriteLine("\n Press any key to continue...");
                    //Console.ReadKey();
        }

        private void waitingForConnection_teszt(Socket listener)
        {
            Console.WriteLine("Waiting for a connection...");
            handler = listener.Accept();
        }

        private string getRestaurantsList()
        {
            string query = "SELECT restaurantID,name,restaurantDescription,style,owner,phoneNumber, city,zipcode,line1,line2, fromHour,fromMinute,toHour,toMinute FROM Restaurant.Restaurant JOIN Restaurant.RestaurantAddress ON Restaurant.RestaurantAddress.addressID = Restaurant.addressID JOIN Restaurant.OpeningHours ON Restaurant.OpeningHours.openingHoursID = Restaurant.openingHoursID";
            List<Restaurant> restListLocalVariable= new List<Restaurant>();
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                    return getErrorMessage(92);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    restListLocalVariable.Add(
                        new Restaurant(
                        dataTable.Rows[i]["city"].ToString(),
                        dataTable.Rows[i]["zipcode"].ToString(),
                        dataTable.Rows[i]["line1"].ToString(),
                        dataTable.Rows[i]["line2"].ToString(),
                        Int32.Parse(dataTable.Rows[i]["fromHour"].ToString()),
                        Int32.Parse(dataTable.Rows[i]["fromMinute"].ToString()),
                        Int32.Parse(dataTable.Rows[i]["toHour"].ToString()),
                        Int32.Parse(dataTable.Rows[i]["toMinute"].ToString()),
                        dataTable.Rows[i]["name"].ToString(),
                        dataTable.Rows[i]["restaurantDescription"].ToString(),
                        dataTable.Rows[i]["style"].ToString(),
                        dataTable.Rows[i]["owner"].ToString(),
                        dataTable.Rows[i]["phoneNumber"].ToString(),
                        Int32.Parse(dataTable.Rows[i]["restaurantID"].ToString()))); 
                }
                da.Dispose();
                dataTable.Clear();
                dataTable.Dispose();
                for (int k = 0; k < restListLocalVariable.Count; ++k)
                    Console.WriteLine("{0} étterem: {1}", k, restListLocalVariable[k].toString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(92);
            }
            ListOfRestaurants fl = new ListOfRestaurants(restListLocalVariable);
            return Newtonsoft.Json.JsonConvert.SerializeObject(fl);
        }


        private string getFoods(string restID, string categoryID)
        {
            string query = "SELECT foodID,name,price,rating,pictureID,Restaurant.Food.categoryID,Restaurant.Food.restaurantID,availableFrom,availableTo FROM Restaurant.Food JOIN Restaurant.CategoryName ON Restaurant.CategoryName.categoryID = Restaurant.Food.categoryID WHERE Restaurant.Food.restaurantID = @restaurantID AND Restaurant.Food.categoryID = @categoryID";
            DataTable dataTable = new DataTable();
            List<Food> listOfFood = new List<Food>();
            try
            {
                Console.WriteLine(1);
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@restaurantID", restID);
                command.Parameters.AddWithValue("@categoryID", categoryID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                    return getErrorMessage(70);
                Console.WriteLine(2);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Console.WriteLine(3);
                    //GETTING ALLERGENES
                    DataTable dataTable2 = new DataTable();
                    query = "SELECT Restaurant.AllergenNames.name FROM Restaurant.AllergenNames JOIN Restaurant.Allergens ON Restaurant.Allergens.allergenID = Restaurant.AllergenNames.allergenID JOIN Restaurant.Food ON Restaurant.Food.foodID = Restaurant.Allergens.foodID WHERE Restaurant.Allergens.foodID = @foodID";
                    SqlCommand command2 = new SqlCommand(query, DatabaseConnection);
                    Console.WriteLine(4);
                    command2.Parameters.AddWithValue("@foodID", dataTable.Rows[i]["foodID"].ToString());
                    Console.WriteLine(5);
                    SqlDataAdapter da2 = new SqlDataAdapter(command2);
                    Console.WriteLine(6);
                    da2.Fill(dataTable2);
                    Console.WriteLine(7);
                    List<string> allergens = new List<string>();
                    if (dataTable2.Rows.Count != 0)
                    {
                        Console.WriteLine(8);
                        for (int k=0;k< dataTable2.Rows.Count;++k)
                            allergens.Add(dataTable2.Rows[k]["name"].ToString());
                        Console.WriteLine(9);
                    }

                    Console.WriteLine(10);
                    int picID = 0;
                    Console.WriteLine(dataTable.Rows[i]["pictureID"].ToString());
                    if (dataTable.Rows[i]["pictureID"].ToString() != "")
                    {
                        Console.WriteLine(11);
                        picID = Int32.Parse(dataTable.Rows[i]["pictureID"].ToString());
                        
                    }
                        
                    Console.WriteLine(12);
                    listOfFood.Add(
                        new Food(
                        Int32.Parse(dataTable.Rows[i]["foodID"].ToString()),
                        dataTable.Rows[i]["name"].ToString(),
                        Int32.Parse(dataTable.Rows[i]["price"].ToString()),
                        Double.Parse(dataTable.Rows[i]["rating"].ToString()),
                        picID,
                        allergens,
                        Int32.Parse(dataTable.Rows[i]["categoryID"].ToString()),
                        Int32.Parse(dataTable.Rows[i]["restaurantID"].ToString()),
                        dataTable.Rows[i]["availableFrom"].ToString(),
                        dataTable.Rows[i]["availableTo"].ToString()));

                    da2.Dispose();
                    dataTable2.Clear();
                    dataTable2.Dispose();
                }
                da.Dispose();
                dataTable.Clear();
                dataTable.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(71);
            }
            for (int i = 0; i < listOfFood.Count; ++i)
            {
                Console.WriteLine("FoodID: {0}", listOfFood[i].FoodID);
                Console.WriteLine("Name: {0}", listOfFood[i].Name);
                Console.WriteLine("Price: {0}", listOfFood[i].Price);
                Console.WriteLine("Rating: {0}", listOfFood[i].Rating);
                Console.WriteLine("PictureID: {0}", listOfFood[i].PictureID);
                Console.WriteLine("CatID: {0}", listOfFood[i].CategoryID);
                Console.WriteLine("RestID: {0}", listOfFood[i].RestaurantID);
                Console.WriteLine("From: {0}", listOfFood[i].AvailableFrom);
                Console.WriteLine("To: {0}", listOfFood[i].AvailableTo);
                Console.WriteLine("Allergens:");
                for (int j = 0; j < listOfFood[i].Allergenes.Count; ++j)
                {
                    Console.WriteLine(listOfFood[i].Allergenes[j]);
                }
            }
            FoodList fl = new FoodList(listOfFood);
            return Newtonsoft.Json.JsonConvert.SerializeObject(fl);
        }


        private string addCategory(JObject o)
        {
            //CHECK IF CATEGORY ALREADY ADDED
            string query = "SELECT categoryID FROM Restaurant.CategoryName WHERE categoryName = @_categoryName";
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand command1 = new SqlCommand(query, DatabaseConnection);
                command1.Parameters.AddWithValue("@_categoryName", o["categoryName"].ToString());
                Console.WriteLine(o["categoryName"].ToString());
                string categoryID = "";
                SqlDataAdapter da = new SqlDataAdapter(command1);
                da.Fill(dataTable);
                if (dataTable.Rows.Count != 0)  //IF CATEGORY ALREADY ADDED, ADD TO REST MENU
                {
                    categoryID = dataTable.Rows[0][0].ToString();
                    dataTable.Clear();
                    dataTable.Dispose();
                    da.Dispose();
                    //GETTING REST ID
                    query = "SELECT restaurantID FROM Restaurant.Restaurant WHERE owner = @username";
                    SqlCommand command2 = new SqlCommand(query, DatabaseConnection);
                    command2.Parameters.AddWithValue("@username", o["owner"].ToString());
                    Console.WriteLine(o["owner"].ToString());
                    da = new SqlDataAdapter(command2);
                    DataTable dataTable2 = new DataTable();
                    da.Fill(dataTable2);
                    if (dataTable2.Rows.Count == 0)
                    {
                        da.Dispose();
                        dataTable2.Clear();
                        dataTable2.Dispose();
                        return getErrorMessage(92);
                    }
                    string restaurantID = "";
                    restaurantID = dataTable2.Rows[0][0].ToString();
                    Console.WriteLine("restaurantID: {0}", restaurantID);
                    da.Dispose();
                    dataTable2.Clear();
                    dataTable2.Dispose();
                    JObject ok = new JObject();
                    //CHECKING IF MENU ALREADY CONTAINS THIS CATEGORY
                    query = "SELECT menuID FROM Restaurant.Menu WHERE restaurantID=@restID AND categoryID=@catID";
                    SqlCommand command3 = new SqlCommand(query, DatabaseConnection);
                    command3.Parameters.AddWithValue("@restID", restaurantID);
                    Console.WriteLine("restID: {0}", restaurantID);
                    command3.Parameters.AddWithValue("@catID", categoryID);
                    Console.WriteLine("categoryID: {0}", categoryID);
                    da = new SqlDataAdapter(command3);
                    DataTable dataTable3 = new DataTable();
                    da.Fill(dataTable3);
                    if (dataTable3.Rows.Count != 0)
                    {
                        da.Dispose();
                        dataTable3.Clear();
                        dataTable3.Dispose();
                        ok.Add("type", 8);
                        ok.Add("status", "Category already exists and is part of menu");
                        ok.Add("categoryID", categoryID);
                        return ok.ToString();
                    }
                    da.Dispose();
                    dataTable3.Clear();
                    dataTable3.Dispose();
                    //NOW ADDING CATEGORY TO MENU
                    query = "INSERT INTO Restaurant.Menu(restaurantID,categoryID) VALUES(@restID,@catID)";
                    SqlCommand command4 = new SqlCommand(query, DatabaseConnection);
                    command4.Parameters.AddWithValue("@restID", restaurantID);
                    Console.WriteLine("restID: {0}", restaurantID);
                    command4.Parameters.AddWithValue("@catID", categoryID);
                    Console.WriteLine("categoryID: {0}", categoryID);
                    int affectedRows = command4.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        return getErrorMessage(93);
                    }
                    ok.Add("type", 8);
                    ok.Add("status", "Category already exists but added to menu");
                    ok.Add("categoryID", categoryID);
                    return ok.ToString();
                }
                else
                {
                    //IF CATEGORY NOT ADDED THEN WE ADD IT
                    query = "DECLARE @returnID INT EXEC @returnID = addCategoryToMenu @categoryName SELECT  'categoryID' = @returnID";
                    SqlCommand command5 = new SqlCommand(query, DatabaseConnection);
                    command5.Parameters.AddWithValue("@categoryName", o["categoryName"].ToString());
                    Console.WriteLine(o["categoryName"].ToString());
                    da.Dispose();
                    da = new SqlDataAdapter(command5);
                    DataTable dataTable4 = new DataTable();
                    da.Fill(dataTable4);
                    if (dataTable4.Rows.Count == 0)
                    {
                        da.Dispose();
                        dataTable4.Dispose();
                        return getErrorMessage(97);
                    }
                    categoryID = dataTable4.Rows[0][0].ToString();   //NOW WE HAVE THE CATEG ID
                    da.Dispose();
                    dataTable4.Clear();
                    dataTable4.Dispose();
                    //GETTING REST ID
                    query = "SELECT restaurantID FROM Restaurant.Restaurant WHERE owner = @username";
                    SqlCommand command6 = new SqlCommand(query, DatabaseConnection);
                    command6.Parameters.AddWithValue("@username", o["owner"].ToString());
                    Console.WriteLine(o["owner"].ToString());
                    da = new SqlDataAdapter(command6);
                    DataTable dataTable5 = new DataTable();
                    da.Fill(dataTable5);
                    if (dataTable5.Rows.Count == 0)
                    {
                        da.Dispose();
                        dataTable5.Dispose();
                        return getErrorMessage(92);
                    }
                    string restaurantID2 = "";
                    restaurantID2 = dataTable5.Rows[0][0].ToString();
                    Console.WriteLine("restaurantID2: {0}", restaurantID2);
                    da.Dispose();
                    dataTable5.Clear();
                    dataTable5.Dispose();
                    //NOW ADDING CATEGORY TO MENU
                    query = "INSERT INTO Restaurant.Menu(restaurantID,categoryID) VALUES(@restID,@catID)";
                    SqlCommand command7 = new SqlCommand(query, DatabaseConnection);
                    command7.Parameters.AddWithValue("@restID", restaurantID2);
                    Console.WriteLine("restID: {0}", restaurantID2);
                    command7.Parameters.AddWithValue("@catID", categoryID);
                    Console.WriteLine("categoryID: {0}", categoryID);
                    int affectedRows = command7.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        return getErrorMessage(93);
                    }
                    JObject ok2 = new JObject();
                    ok2.Add("type", 8);
                    ok2.Add("status", "Category didn't exist, now added to menu");
                    ok2.Add("categoryID", categoryID);
                    return ok2.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            //List<string> catList= getCategories(o["restaurantName"].ToString());
            //if (catList.Count == 0)
            //{
            //    return getErrorMessage(99);
            //}
            
            //Categories sendCat = new Categories(Int32.Parse(o["clientID"].ToString()),catList);
            //string sendCatJSON = JsonConvert.SerializeObject(sendCat);
            //Console.WriteLine("SendCatJSON: \n {0}", sendCatJSON);
            //return sendCatJSON;
        }

        private string getCategories(string restaurantID)
        {
            string query = "SELECT Restaurant.Menu.categoryID,categoryName FROM Restaurant.CategoryName JOIN Restaurant.Menu ON Restaurant.Menu.categoryID = Restaurant.CategoryName.categoryID WHERE Restaurant.Menu.restaurantID = @restaurantID";
            DataTable dataTable = new DataTable();
            List<string> catList = new List<string>();
            List<string> idList = new List<string>();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@restaurantID", restaurantID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                    return getErrorMessage(70);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    idList.Add(dataTable.Rows[i]["categoryID"].ToString());
                    catList.Add(dataTable.Rows[i]["categoryName"].ToString());
                }
                da.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(71);
            }
            Categories cat = new Categories(idList, catList);            
            return Newtonsoft.Json.JsonConvert.SerializeObject(cat);
        }

        private string checkUsernameAvailable(string username, int userType)
        {
            //CHECK TO SEE IF USERNAME IS AVAILABLE
            string query = "SELECT username FROM Users.Users WHERE username = @username AND userType=@userType";
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@userType", userType);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count != 0)
                {
                    da.Dispose();
                    return getErrorMessage(90);
                }
                da.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            return "1";
        }

        private string registerRestaurant(JObject o)
        {
            //CHECK TO SEE IF USER IS AVAILABLE
            string response = checkUsernameAvailable(o["owner"].ToString(), 1);
            if (response[0] != '1')
                return response;
           
            string query = "EXEC registerRestaurant @username, @pass, @lastName, @firstName, @phone, @email, @name, @restaurantDescription, @style, @city, @zipcode, @line1, @line2, @fromHour ,@fromMinute, @toHour, @toMinute";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", o["owner"].ToString());
                Console.WriteLine(o["owner"].ToString());
                command.Parameters.AddWithValue("@pass", o["pass"].ToString());
                Console.WriteLine(o["pass"].ToString());
                command.Parameters.AddWithValue("@lastName", o["lastName"].ToString());
                Console.WriteLine(o["lastName"].ToString());
                command.Parameters.AddWithValue("@firstName", o["firstName"].ToString());
                Console.WriteLine(o["firstName"].ToString());
                command.Parameters.AddWithValue("@phone", o["phoneNumber"].ToString());
                Console.WriteLine(o["phoneNumber"].ToString());
                command.Parameters.AddWithValue("@email", o["email"].ToString());
                Console.WriteLine(o["email"].ToString());
                command.Parameters.AddWithValue("@name", o["name"].ToString());
                Console.WriteLine(o["name"].ToString());
                command.Parameters.AddWithValue("@restaurantDescription", o["restaurantDescription"].ToString());
                Console.WriteLine(o["restaurantDescription"].ToString());
                command.Parameters.AddWithValue("@style", o["style"].ToString());
                Console.WriteLine(o["style"].ToString());
                command.Parameters.AddWithValue("@city", o["city"].ToString());
                Console.WriteLine(o["city"].ToString());
                command.Parameters.AddWithValue("@zipcode", o["zipcode"].ToString());
                Console.WriteLine(o["zipcode"].ToString());
                command.Parameters.AddWithValue("@line1", o["line1"].ToString());
                Console.WriteLine(o["line1"].ToString());
                command.Parameters.AddWithValue("@line2", o["line2"].ToString());
                Console.WriteLine(o["line2"].ToString());
                command.Parameters.AddWithValue("@fromHour", Int32.Parse(o["fromHour"].ToString()));
                Console.WriteLine(o["fromHour"].ToString());
                command.Parameters.AddWithValue("@fromMinute", Int32.Parse(o["fromMinute"].ToString()));
                Console.WriteLine(o["fromMinute"].ToString());
                command.Parameters.AddWithValue("@toHour", Int32.Parse(o["toHour"].ToString()));
                Console.WriteLine(o["toHour"].ToString());
                command.Parameters.AddWithValue("@toMinute", Int32.Parse(o["toMinute"].ToString()));
                Console.WriteLine(o["toMinute"].ToString());
                //SqlDataAdapter da = new SqlDataAdapter(command);
                // Console.WriteLine("SQL COMMAND: {0}", command.Parameters.ToString());
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(91);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type", 5);
            ok.Add("status", "Successful");
            return ok.ToString();
        }



        private string registerUser(JObject o)
        {
            //CHECK TO SEE IF USERNAME IS AVAILABLE
            string response = checkUsernameAvailable(o["username"].ToString(), Int32.Parse(o["userType"].ToString()));
            if (response[0] != '1')
                return response;

            //IF USERNAME AVAILABLE
            string query = "EXEC registerUser @username, @pass, @email, @city, @zip, @line1, @line2, @lastName, @firstName, @phone, @userType";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", o["username"].ToString());
                Console.WriteLine(o["username"].ToString());
                command.Parameters.AddWithValue("@pass", o["password"].ToString());
                Console.WriteLine(o["password"].ToString());
                command.Parameters.AddWithValue("@city", o["city"].ToString());
                Console.WriteLine(o["city"].ToString());
                command.Parameters.AddWithValue("@email", o["email"].ToString());
                Console.WriteLine(o["email"].ToString());
                command.Parameters.AddWithValue("@zip", o["zipcode"].ToString());
                Console.WriteLine(o["zipcode"].ToString());
                command.Parameters.AddWithValue("@line1", o["line1"].ToString());
                Console.WriteLine(o["line1"].ToString());
                command.Parameters.AddWithValue("@line2", o["line2"].ToString());
                Console.WriteLine(o["line2"].ToString());
                command.Parameters.AddWithValue("@lastName", o["lastName"].ToString());
                Console.WriteLine(o["lastName"].ToString());
                command.Parameters.AddWithValue("@firstName", o["firstName"].ToString());
                Console.WriteLine(o["firstName"].ToString());
                command.Parameters.AddWithValue("@phone", o["phoneNumber"].ToString());
                Console.WriteLine(o["phoneNumber"].ToString());
                command.Parameters.AddWithValue("@userType", Int32.Parse(o["userType"].ToString()));
                Console.WriteLine(o["userType"].ToString());
                //SqlDataAdapter da = new SqlDataAdapter(command);
                // Console.WriteLine("SQL COMMAND: {0}", command.Parameters.ToString());
                int affectedRows=command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(96);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type",4);
            ok.Add("status","Successful");
            return ok.ToString();
        }

        public static SqlConnection ConnectToDatabase()
        {
            //                      localhost             DatabaseName     UserName     UserPassw
            String cnn = "Data Source=localhost;Initial Catalog=Netpincer;User ID=sa;Password=passw0rd";
            SqlConnection db = new SqlConnection(cnn);
            db.Open();
            return db;
        }

        private JObject sendFirstConnectionInfo() 
        {
            JObject jobc = new JObject();
            //HEADER
            jobc.Add("type", 0);
            jobc.Add("clientID", clientID);
            clientID++;
            return jobc;
        }

        private JObject getClientHeader(JObject jsonObject)
        {
            JObject obj = new JObject();
            obj.Add("type", jsonObject["type"].ToString());
            obj.Add("clientID", jsonObject["clientID"].ToString());
            return obj;
        }


        private string getUserInfo(string username, string pass, string userType)
        {
            string query = "SELECT* FROM getUser(@username, @pass, @userType)";
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@userType", userType);
                SqlDataAdapter da = new SqlDataAdapter(command);
                Console.WriteLine("USER type int: {0}", userType);
               // Console.WriteLine("SQL COMMAND: {0}", command.Parameters.ToString());
                da.Fill(dataTable);
                if (dataTable.Rows.Count==0) 
                {
                    da.Dispose();
                    return getErrorMessage(91);
                }
                //Console.WriteLine("rows: {0}", dataTable.ToString());
                da.Dispose();
                Console.WriteLine("TABLE: {0}", JsonConvert.SerializeObject(dataTable));
                return JsonConvert.SerializeObject(dataTable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }   
        }

        private string getRestaurant(string username)
        {
            string query = "SELECT* FROM getRestaurant(@username)";
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", username);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                {
                    da.Dispose();
                    return getErrorMessage(92);
                }
                da.Dispose();
                Console.WriteLine("TABLE: {0}", JsonConvert.SerializeObject(dataTable));
                return JsonConvert.SerializeObject(dataTable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
        }

         private string addFood(JObject o)
        {
            string query = "DECLARE @returnID INT EXEC @returnID = addFood @foodName,@price,@rating,@categoryID,@restaurantID,@availableFrom,@availableTo SELECT  'foodID' = @returnID";
            SqlCommand command5 = new SqlCommand(query, DatabaseConnection);
            command5.Parameters.AddWithValue("@foodName", o["Name"].ToString());
            Console.WriteLine(o["Name"].ToString());
            command5.Parameters.AddWithValue("@price", o["Price"].ToString());
            Console.WriteLine(o["Price"].ToString());
            command5.Parameters.AddWithValue("@rating", o["Rating"].ToString());
            Console.WriteLine(o["Rating"].ToString());
            command5.Parameters.AddWithValue("@categoryID", o["CategoryID"].ToString());
            Console.WriteLine(o["CategoryID"].ToString());
            command5.Parameters.AddWithValue("@restaurantID", o["RestaurantID"].ToString());
            Console.WriteLine(o["RestaurantID"].ToString());
            command5.Parameters.AddWithValue("@availableFrom", o["AvailableFrom"].ToString());
            Console.WriteLine(o["AvailableFrom"].ToString());
            command5.Parameters.AddWithValue("@availableTo", o["AvailableTo"].ToString());
            Console.WriteLine(o["AvailableTo"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(command5);
            DataTable dataTable4 = new DataTable();
            da.Fill(dataTable4);
            if (dataTable4.Rows.Count == 0)
            {
                da.Dispose();
                dataTable4.Dispose();
                return getErrorMessage(98);
            }
            string foodID = dataTable4.Rows[0][0].ToString();   //NOW WE HAVE THE CATEG ID
            da.Dispose();
            dataTable4.Clear();
            dataTable4.Dispose();
            //FOOD ADDED, NOT LET'S SEE IF ALLERGEN EXISTS OR NOT
            List<string> allergens = new List<string>();
            allergens = JsonConvert.DeserializeObject<List<string>>(o["Allergenes"].ToString());
            for (int i = 0; i < allergens.Count; ++i)
            {
                query = "SELECT allergenID FROM Restaurant.AllergenNames WHERE name = @allergenName";
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@allergenName", allergens[i]);
                SqlDataAdapter da2 = new SqlDataAdapter(command);
                DataTable dataTable2 = new DataTable();
                da2.Fill(dataTable2);
                if (dataTable2.Rows.Count == 0)
                {
                    da2.Dispose();
                    dataTable2.Dispose();
                    return getErrorMessage(69);
                }
                string allergenID = dataTable2.Rows[0][0].ToString();   //NOW WE HAVE THE CATEG ID
                da2.Dispose();
                dataTable2.Clear();
                dataTable2.Dispose();
                //ADDING ALLERGENS TO FOOD
                query = "INSERT INTO Restaurant.Allergens(allergenID,foodID) VALUES(@allergenID,@foodID)";
                SqlCommand command3 = new SqlCommand(query, DatabaseConnection);
                command3.Parameters.AddWithValue("@allergenID", Int32.Parse(allergenID));
                Console.WriteLine("allergenID: {0}", allergenID);
                command3.Parameters.AddWithValue("@foodID", Int32.Parse(foodID));
                Console.WriteLine("foodID: {0}", foodID);
                int affectedRows = command3.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(68);
                }
            }
            

            JObject ok2 = new JObject();
            ok2.Add("type", 10);
            ok2.Add("status", "Food added succesfully");
            ok2.Add("foodID", foodID);
            return ok2.ToString();
        }


        private string getErrorMessage(int errorNumber)
        {
            JObject errorObject = new JObject();
            errorObject.Add("type", 99);
            switch (errorNumber) 
            {
                case 68:
                    errorObject.Add("error", "attempt to add Allergens failed");
                    break;
                case 69:
                    errorObject.Add("error", "Allergen doesn't exist");
                    break;
                case 70:
                    errorObject.Add("error", "Category table empty");
                    break;
                case 71:
                    errorObject.Add("error", "RestaurantID not found");
                    break;
                case 90:
                    errorObject.Add("error", "Username not available found");
                    break;
                case 91:
                    errorObject.Add("error", "User not found");
                    break;
                case 92:
                    errorObject.Add("error", "Restaurant not found");
                    break;
                case 93:
                    errorObject.Add("error", "Menu not found");
                    break;
                case 94:
                    errorObject.Add("error", "Category not found");
                    break;
                case 95:
                    errorObject.Add("error", "attempt to register user failed");
                    break;
                case 96:
                    errorObject.Add("error", "attempt to register restaurant failed");
                    break;
                case 97:
                    errorObject.Add("error", "attempt to add category failed");
                    break;
                case 98:
                    errorObject.Add("error", "attempt to add Food failed");
                    break;
                default:
                    errorObject.Add("error", "Unexpected error");
                    break;

            }
            return errorObject.ToString();
        }


        private void waitingForConnection()
        {
            Console.WriteLine("Waiting for a connection...");
            handler = listener.Accept();
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Text.Json;
using System.Text.Json.Serialization;

public class ConnectToServer
{

    // Client app is the one sending messages to a Server/listener.   
    // Both listener and client can send messages back and forth once a   
    // communication is established.  
    private int clientID;
    private int msgID;
    private IPHostEntry host;
    private IPAddress ipAddress;
    private IPEndPoint remoteEP;
    private Socket sender;
    byte[] bytes = new byte[4096];

    public ConnectToServer()
    {
        clientID = -1;
        msgID = 0;
        StartClient();
        
    }

    public void StopClient()
    {
        clientID = -1;
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
    }

    public void StartClient()
    {
        

        try
        {
            // Connect to a Remote server  
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            host = Dns.GetHostEntry("localhost");
            ipAddress = host.AddressList[1];
            remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.    
            sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.    
            try
            {
                // Connect to Remote EndPoint  
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());               

                    if (clientID == -1)
                    {
                        //Sending "Hello" to Server
                        string receivedMsg =sendJSON(sendFirstConnectionInfo());
                    
                        try
                        {
                            JObject receivedJSonObject = new JObject();
                            receivedJSonObject = JObject.Parse(receivedMsg);
                            if (receivedJSonObject["type"].ToString() == "0")
                            {
                                clientID = Int32.Parse(receivedJSonObject["clientID"].ToString());
                                Console.WriteLine("ClientID : {0}", clientID);
                        }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                }

                    

                    //String recievedMsg = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public Restaurant getRestaurant(string username) 
    {
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 2);    // 2 - Get Restaurant login
            jobc.Add("clientID", clientID);
            jobc.Add("username", username);
            string recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);

            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "2")
            {
                return new Restaurant(
                    receivedJSonObject["city"].ToString(),
                    receivedJSonObject["zipcode"].ToString(),
                    receivedJSonObject["line1"].ToString(),
                    receivedJSonObject["line2"].ToString(),
                    Int32.Parse(receivedJSonObject["fromHour"].ToString()),
                    Int32.Parse(receivedJSonObject["fromMinute"].ToString()),
                    Int32.Parse(receivedJSonObject["toHour"].ToString()),
                    Int32.Parse(receivedJSonObject["toMinute"].ToString()),
                    receivedJSonObject["name"].ToString(),
                    receivedJSonObject["restaurantDescription"].ToString(),
                    receivedJSonObject["style"].ToString(),
                    receivedJSonObject["owner"].ToString(),
                    receivedJSonObject["phoneNumber"].ToString(),
                    Int32.Parse(receivedJSonObject["restaurantID"].ToString())
                    );
            }
            else if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        return new Restaurant();
    }

    public int addCategory(string categoryName, string owner)
    {
        string recievedMsg = "";
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 8);    // 8 - Add category
            jobc.Add("clientID", clientID);
            jobc.Add("categoryName", categoryName);
            jobc.Add("owner", owner);
            recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);
            

            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "8")
                return Int32.Parse(receivedJSonObject["categoryID"].ToString());
            else if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
            }
            return -1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return -1;
        }
    }

    private string sendJSON(JObject JsonSendObject)     //Returns received string
    {
        try
        {
            //Console.WriteLine("JSONOBJECT:\n {0}", JsonSendObject);
            //string msgJSON = JsonConvert.SerializeObject(JsonSendObject.ToString());
           byte[] msg = Encoding.ASCII.GetBytes(JsonSendObject.ToString());
           //byte [] msg = Encoding.GetEncoding("windows-1250").GetBytes(JsonSendObject.ToString());
            // Send the data through the socket.    
            int bytesSent = sender.Send(msg);
            Console.WriteLine("Bytes sent {0}", bytesSent);
            Console.WriteLine("Sent:\n {0}", JsonSendObject.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        Console.WriteLine("Waiting...");
        // Receive the response from the remote device.    
        int bytesRec = sender.Receive(bytes);
        Console.WriteLine("Recieved bytes {0}", bytesRec);
        //return Encoding.GetEncoding("windows-1250").GetString(bytes);
        return Encoding.ASCII.GetString(bytes, 0, bytesRec);

    }
    int userSignIn;

    public int UserSignIn { get => userSignIn; set => userSignIn = value; }

    public User getUser(string username, string password, UserType userType)   //Returns the User object for the given username ands password (if the parameters are wrong the server dies rip so don't do that)
    {
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 1);    // 1 - User login
            jobc.Add("clientID", clientID);
            jobc.Add("username", username);
            jobc.Add("password", password);
            int userTypeNumber=4;
            switch (userType)
            {
                case UserType.Customer:
                    userTypeNumber = 0;
                    break;
                case UserType.RestaurantOwner:
                    userTypeNumber = 1;
                    break;
                case UserType.DeliveryPerson:
                    userTypeNumber = 2;
                    break;
                default:
                    Console.WriteLine("Undefined User type");
                    break;
            }
            jobc.Add("userType", userTypeNumber);
            string recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);
        
                JObject receivedJSonObject = new JObject();
                receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "1")
            {
                //var root = JObject.Parse(jsonString);
                //return receivedJSonObject["User"].ToObject<User>();   //WIP
                userSignIn = 1;
                return new User(
                    receivedJSonObject["username"].ToString(),
                    receivedJSonObject["password"].ToString(),
                    receivedJSonObject["lastName"].ToString(),
                    receivedJSonObject["firstName"].ToString(),
                    receivedJSonObject["phoneNumber"].ToString(),
                    receivedJSonObject["city"].ToString(),
                    receivedJSonObject["zipcode"].ToString(),
                    receivedJSonObject["line1"].ToString(),
                    receivedJSonObject["line2"].ToString(),
                    Int32.Parse(receivedJSonObject["userType"].ToString()),
                    receivedJSonObject["email"].ToString()
                    );
            }
            else if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                userSignIn = 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        return new User();

    }


    public int registerRestaurant(Restaurant rest)
    {
        int retval = 1;
        try
        {
            Console.WriteLine("Basic Restautant: {0}", rest.toString());
            string restString = JsonConvert.SerializeObject(rest);
            Console.WriteLine("String rest: {0}", restString);
            JObject header = new JObject();
            header.Add("type", 5);
            header.Add("clientID", clientID);
            JObject body = new JObject();
            body = JObject.Parse(restString);
            header.Merge(body);
            string recievedMsg = sendJSON(header);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);

            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "5")
            {
                Console.WriteLine("Server: {0}", receivedJSonObject["status"].ToString());
            }
            else if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                retval = 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            retval = 0;
        }
        return retval;
    }

    public void updateOrderState(int OrderID, int newState)
    {
        try
        {
            JObject obj = new JObject();
            obj.Add("type", 13);
            obj.Add("OrderID", OrderID);
            obj.Add("newState", newState);
            string recievedMsg = sendJSON(obj);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);

            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "13")
            {
                Console.WriteLine("Server: {0}", receivedJSonObject["status"].ToString());
            }
            else if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public int addFood(Food f)
    {
        try
        {
            string restString = JsonConvert.SerializeObject(f);
            JObject header = new JObject();
            header.Add("type", 10);
            header.Add("clientID", clientID);
            JObject body = new JObject();
            body = JObject.Parse(restString);
            header.Merge(body);
            string recievedMsg = sendJSON(header);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);

            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "10")
            {
                Console.WriteLine("Server: {0}", receivedJSonObject["status"].ToString());
                return Int32.Parse(receivedJSonObject["foodID"].ToString());
            }
            else if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        return -1;
    }


    public ListOfRestaurants getRestaurantsList()
    {
        string recievedMsg = "";
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 11);        //11 - Get list of Restaurants
            jobc.Add("clientID", clientID);
            recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);
            ListOfRestaurants rl = Newtonsoft.Json.JsonConvert.DeserializeObject<ListOfRestaurants>(recievedMsg);
            if (rl.RestaurantList == null)
                throw new Exception();
            for (int i = 0; i < rl.RestaurantList.Count; ++i)
            {
                Console.WriteLine("Restaurant {0}\n{1}",i+1, rl.RestaurantList[i].toString());
            }
            return rl;
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                return new ListOfRestaurants();  //Sends empty class
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                return new ListOfRestaurants(); //Sends empty class
            }
        }
        return new ListOfRestaurants();
    }


    public Categories getCategories(int restaurantID) 
    {
        string recievedMsg = "";
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 7);        //7 - Get list of Categories
            jobc.Add("clientID", clientID); 
            jobc.Add("restaurantID", restaurantID);
            recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);
            Categories cat = Newtonsoft.Json.JsonConvert.DeserializeObject<Categories>(recievedMsg);
            Console.WriteLine("GETCATEGORIES");
            Console.WriteLine("ID   NAME");
            if (cat.ListOfCategoryIDs == null)
                throw new Exception();
            for (int i = 0; i < cat.ListOfCategoryIDs.Count; ++i)
            {
                Console.Write(cat.ListOfCategoryIDs[i]);
                Console.Write("   ");
                Console.WriteLine(cat.ListOfCategoryNames[i]);
            }

            return cat;
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                return new Categories(new List<string>(), new List<string>());  //Sends empty lists
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                return new Categories(new List<string>(), new List<string>());  //Sends empty lists
            }
        }
        return new Categories();
    }

    public int registerUser(User user)
    {
        int retval = 1;
        //THIS WORKS:
        try 
        { 
            Console.WriteLine("Basic user: {0}", user.toString());
            string userString = JsonConvert.SerializeObject(user);
            Console.WriteLine("String user2: {0}", userString);
            JObject header = new JObject();
            header.Add("type", 4);
            header.Add("clientID", clientID);
            JObject body = new JObject();
            body = JObject.Parse(userString);
            header.Merge(body);
            string recievedMsg = sendJSON(header);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);

            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "4")
            {
                userSignIn = 1;
                Console.WriteLine("Server: {0}", receivedJSonObject["status"].ToString());
            }
            else if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                userSignIn = 0;
                retval = 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            userSignIn = 0;
            retval = 0;
        }
        return retval;
 }

    public FoodList getFoods(int restaurantID, int categoryID)
    {
        string recievedMsg = "";
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 9);        //9 - Get list of Food
            jobc.Add("clientID", clientID);
            jobc.Add("restaurantID", restaurantID);
            jobc.Add("categoryID", categoryID);
            recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);
            FoodList foodList = Newtonsoft.Json.JsonConvert.DeserializeObject<FoodList>(recievedMsg);
            if (foodList.ListFood == null)
                throw new Exception();
            for (int i = 0; i < foodList.ListFood.Count; ++i)
            {
                Console.WriteLine("FoodID: {0}", foodList.ListFood[i].FoodID);
                Console.WriteLine("Name: {0}", foodList.ListFood[i].Name);
                Console.WriteLine("Price: {0}", foodList.ListFood[i].Price);
                Console.WriteLine("Rating: {0}", foodList.ListFood[i].Rating);
                Console.WriteLine("PictureID: {0}", foodList.ListFood[i].PictureID);
                Console.WriteLine("CatID: {0}", foodList.ListFood[i].CategoryID);
                Console.WriteLine("RestID: {0}", foodList.ListFood[i].RestaurantID);
                Console.WriteLine("From: {0}", foodList.ListFood[i].AvailableFrom);
                Console.WriteLine("To: {0}", foodList.ListFood[i].AvailableTo);
                Console.WriteLine("Allergens:");
                for (int j = 0; j < foodList.ListFood[i].Allergenes.Count; ++j)
                {
                    Console.WriteLine(foodList.ListFood[i].Allergenes[j]);
                }
            }

            return foodList;
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                return new FoodList();  //Sends empty class
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                return new FoodList(); //Sends empty class
            }
        }
        return new FoodList();
    }

    public Food getFoodByID(int foodID)
    {
        string recievedMsg = "";
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 14);        //14 - Get Food by ID
            jobc.Add("clientID", clientID);
            jobc.Add("foodID", foodID);
            recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);
            Food f = Newtonsoft.Json.JsonConvert.DeserializeObject<Food>(recievedMsg);
            if (f == null)
                throw new Exception();

            Console.WriteLine("FoodID: {0}", f.FoodID);
            Console.WriteLine("Name: {0}", f.Name);
            Console.WriteLine("Price: {0}", f.Price);
            Console.WriteLine("Rating: {0}", f.Rating);
            Console.WriteLine("PictureID: {0}", f.PictureID);
            Console.WriteLine("CatID: {0}", f.CategoryID);
            Console.WriteLine("RestID: {0}", f.RestaurantID);
            Console.WriteLine("From: {0}", f.AvailableFrom);
            Console.WriteLine("To: {0}", f.AvailableTo);
            Console.WriteLine("Allergens:");
            for (int j = 0; j < f.Allergenes.Count; ++j)
            {
                Console.WriteLine(f.Allergenes[j]);
            }

            return f;
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                
            }
            return new Food();  //Sends empty class
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return new Food(); //Sends empty class
        }
    }

    private JObject sendFirstConnectionInfo()
    {
        JObject jobc = new JObject();
        jobc.Add("type", 0);    // 0 - Basic Server Info aka "Hello"
        jobc.Add("msgID", msgID);       //#of messages sent     //WILL BE DELETED
        msgID++;
        return jobc;
    }


    public OrderList getOrders(int restaurantID)
    {
        string recievedMsg = "";
        try
        {
            JObject jobc = new JObject();
            jobc.Add("type", 12);        //12 - Get Orders for one Restaurant
            jobc.Add("clientID", clientID);
            jobc.Add("restaurantID", restaurantID);
            recievedMsg = sendJSON(jobc);
            Console.WriteLine("recievedMsg: {0}", recievedMsg);
            OrderList receivedOrderList = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderList>(recievedMsg);
            if (receivedOrderList.ListOrder == null)
            {
                Console.WriteLine("ListOrder == null");
                throw new Exception();
            }
            for (int i = 0; i < receivedOrderList.ListOrder.Count; ++i)
            {
                Console.WriteLine("OrderID: {0}", receivedOrderList.ListOrder[i].OrderID);
                Console.WriteLine("OrderStatus: {0}", receivedOrderList.ListOrder[i].OrderStatus);
                Console.WriteLine("StatusString: {0}", receivedOrderList.ListOrder[i].StatusString);
                Console.WriteLine("OrderTime: {0}", receivedOrderList.ListOrder[i].OrderTime);
                Console.WriteLine("EndorderTime: {0}", receivedOrderList.ListOrder[i].EndorderTime);
                Console.WriteLine("Customer: {0}", receivedOrderList.ListOrder[i].Customer);
                Console.WriteLine("TotalPrice: {0}", receivedOrderList.ListOrder[i].TotalPrice);
                Console.WriteLine("Foods:");
                for (int j = 0; j < receivedOrderList.ListOrder[i].OrderedFoodList.Count; ++j)
                {
                    Console.WriteLine(receivedOrderList.ListOrder[i].OrderedFoodList[j]);
                }
            }
            return receivedOrderList;
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            if (receivedJSonObject["type"].ToString() == "99")
            {
                Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                return new OrderList();  //Sends empty class
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            JObject receivedJSonObject = new JObject();
            receivedJSonObject = JObject.Parse(recievedMsg);
            return new OrderList(); //Sends empty class
        }
        return new OrderList();
    }
}
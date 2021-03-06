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
using System.Threading;

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
        //Socket handler;

        public class StateObject
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 4196;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            // Received data string.
            public StringBuilder sb = new StringBuilder();
        }

        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public SocketListener()
        {
            DatabaseConnection = ConnectToDatabase();
            Console.WriteLine("Connection State: {0}", DatabaseConnection.State);
            clientID = 0;
            //listOfConnectedClients = new List<int>();
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

        //private void waitingForConnection_teszt(Socket listener)
        //{
        //    Console.WriteLine("Waiting for a connection...");
        //    handler = listener.Accept();
        //}

    // State object for reading client data asynchronously
       

        public void StartServer()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[4196];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000));
                listener.Listen(100);
                //listener.Bind(localEndPoint);
                //listener.Listen(100);11
               
                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            Console.WriteLine("AcceptCallback");
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                Send(handler, evaulateData(content.ToString()));
                //if (content.IndexOf("<EOF>") > -1)
                //{
                //    // All the data has been read from the 
                //    // client. Display it on the console.
                //    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                //        content.Length, content);
                //    // Echo the data back to the client.
                //    Send(handler, content);
                //}
                //else
                //{
                //    // Not all data received. Get more.
                //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                //    new AsyncCallback(ReadCallback), state);
                //}
            }
        }

   


        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.

            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private string evaulateData(string data)
        {
            //RECEIVED NEW BYTES
            Console.WriteLine("evaulateData");
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
                    return sendJason.ToString();
                    //handler.Send(Encoding.ASCII.GetBytes(sendJason.ToString()));
                    // handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(sendJason.ToString()));  //Sending Client ID
                    // listOfConnectedClients.Add(clientID - 1); //Adding client to connected list
                }
                else if (receivedJSONObject["type"].ToString() == "1") //Get User Information
                {
                    JObject header = getClientHeader(receivedJSONObject);  //1st JObj  --Header
                    string userInfo = getUserInfo(receivedJSONObject["username"].ToString(), receivedJSONObject["password"].ToString(), receivedJSONObject["userType"].ToString());
                    //userInfo = userInfo.Replace("[", "");
                    //userInfo = userInfo.Replace("]", "");
                    JObject userJason = JObject.Parse(userInfo);    //2nd JObj  --Body
                    Console.WriteLine("BODY: {0}", userJason);
                    //JsonConvert.DeserializeObject(userInfo);
                    header.Merge(userJason);
                    Console.WriteLine("MERGED JSON: {0}", header.ToString());
                    return header.ToString();
                    //handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
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
                    return header.ToString();
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(header.ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
                }
                else if (receivedJSONObject["type"].ToString() == "3") // DP login
                {
                    string register = deliveryPersonLogin(receivedJSONObject["username"].ToString(), receivedJSONObject["password"].ToString(), 2);
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //register = "5";
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "4") // Register User
                {
                    string register = registerUser(receivedJSONObject);
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "41") // Register Delivery Person for piece of shirt Java, because it's a useless language and nothing ever works
                {
                    string register = registerUser(receivedJSONObject);
                    register = "5";
                    //register += "-1";   //trying to send EOF to Java client
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                if (receivedJSONObject["type"].ToString() == "42")  //First Connection From DP Client
                {
                    //Console.WriteLine("First message");
                    /*JObject sendJason = new JObject();
                     sendJason = sendFirstConnectionInfo();
                     String response = sendJason.ToString();
                    */
                    String rsp = "5";
                    // response+= "EXIT";   //trying to send EOF to Java client*/
                    //handler.Send(Encoding.ASCII.GetBytes(rsp));
                    return rsp.ToString();
                    // handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(sendJason.ToString()));  //Sending Client ID
                    // listOfConnectedClients.Add(clientID - 1); //Adding client to connected list
                }
                else if (receivedJSONObject["type"].ToString() == "5") //Register Restaurant
                {
                    string register = registerRestaurant(receivedJSONObject);
                    return register.ToString();
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                }
                else if (receivedJSONObject["type"].ToString() == "6") //Delivery Person Working Hours
                {
                    string register = deliveryPersonWorkingHoursRegister(receivedJSONObject);
                    return register.ToString();
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                }
                else if (receivedJSONObject["type"].ToString() == "7") //Get list of Categories
                {
                    string register = getCategories(receivedJSONObject["restaurantID"].ToString());
                    return register.ToString();
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                }
                else if (receivedJSONObject["type"].ToString() == "8") //Add category
                {
                    string register = addCategory(receivedJSONObject);
                    return register.ToString();
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                }
                else if (receivedJSONObject["type"].ToString() == "9") //9 - Get list of Food
                {
                    string register = getFoods(receivedJSONObject["restaurantID"].ToString(), receivedJSONObject["categoryID"].ToString());
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                }
                else if (receivedJSONObject["type"].ToString() == "10") //10 - Add Food
                {
                    string register = addFood(receivedJSONObject);
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "11") //11 - Get list of Restaurants
                {
                    string register = getRestaurantsList();
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                    //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                }
                else if (receivedJSONObject["type"].ToString() == "12")
                {
                    string register = getOrders(Int32.Parse(receivedJSONObject["restaurantID"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "13")
                {
                    string register = updateOrderState(Int32.Parse(receivedJSONObject["OrderID"].ToString()), Int32.Parse(receivedJSONObject["newState"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "14")
                {
                    string register = foodByID(receivedJSONObject["foodID"].ToString());
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "15")
                {
                    string register = updateDiscount(Int32.Parse(receivedJSONObject["foodID"].ToString()), Double.Parse(receivedJSONObject["discount"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "16")
                {
                    string register = setApproximateDeliveryTime(Int32.Parse(receivedJSONObject["orderID"].ToString()), Int32.Parse(receivedJSONObject["restID"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "17")
                {
                    string register = updateFood(receivedJSONObject);
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "18")
                {
                    string register = addOrder(receivedJSONObject);
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "19")
                {
                    string register = getDeliveryPersonList();
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "20")
                {
                    string register = addOrderToDeliveryPerson(Int32.Parse(receivedJSONObject["boiID"].ToString()), Int32.Parse(receivedJSONObject["orderID"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "21")
                {
                    string register = deleteOrderFromDeliveryPerson(Int32.Parse(receivedJSONObject["boiID"].ToString()), Int32.Parse(receivedJSONObject["orderID"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "22")
                {
                    string register = getDeliveryPersonOrders(Int32.Parse(receivedJSONObject["boiID"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }
                else if (receivedJSONObject["type"].ToString() == "23")
                {
                    string register = getStatusAndETAForUser(Int32.Parse(receivedJSONObject["orderID"].ToString()));
                    //handler.Send(Encoding.ASCII.GetBytes(register));
                    return register.ToString();
                }




            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }
            return "";
        }
        public void StartServer2()
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


                #region ADD ORDER EXAMPLES
                //-------------------REGISTERED USER EXAMPLE--------------------
                //JObject o = new JObject();
                //o.Add("restID", 1);
                //o.Add("username", "alma");
                //o.Add("foods", "1,2");
                //o.Add("price", 1200);
                //Console.WriteLine(addOrder(o));

                //---------------------UNREGISTERED USER EXAMPLE--------------------
                //JObject o = new JObject();
                //o.Add("restID", 1);
                //o.Add("username", "");
                //o.Add("foods", "1,2");
                //o.Add("price", 1200);
                //o.Add("city","Veszprem");
                //o.Add("zipcode",8200);
                //o.Add("line1","ast utca 27");
                //o.Add("line2", "2. emelet 3. ajto");
                //o.Add("firstName","Elek");
                //o.Add("lastName","Kelek");
                //o.Add("phoneNumber","+3620148745");
                //Console.WriteLine(addOrder(o));
                #endregion

                #region Marci & Klau - 05.13
                //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //    socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000));
                //    socket.Listen(10);
                    //waitingForConnection_teszt(socket);
                //while (true)
                //{
                //    // Set the event to nonsignaled state.
                //    allDone.Reset();

                //    // Start an asynchronous socket to listen for connections.
                //    Console.WriteLine("Waiting for a connection...");
                //    listener.BeginAccept(
                //        new AsyncCallback(AcceptCallback),
                //        listener);

                //    // Wait until a connection is made before continuing.
                //    allDone.WaitOne();
                //}

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

                    //while (true)
                    //{
                    //    bytes = new byte[4096];
                    //    int bytesRec = handler.Receive(bytes);
                    //    if (bytesRec != 0)
                    //    {
                    //        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //        break;
                    //    }
                    //}

                    #endregion



                    //RECEIVED NEW BYTES
                    //try
                    //{
                    //    JObject receivedJSONObject = new JObject();
                    //    receivedJSONObject = JObject.Parse(data);
                    //    Console.WriteLine("Received JSON: {0}", receivedJSONObject.ToString());
                    //    if (receivedJSONObject["type"].ToString() == "0")  //First Connection From Client
                    //    {
                    //        //Console.WriteLine("First message");
                    //        JObject sendJason = new JObject();
                    //        sendJason = sendFirstConnectionInfo();
                    //        handler.Send(Encoding.ASCII.GetBytes(sendJason.ToString()));
                    //        // handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(sendJason.ToString()));  //Sending Client ID
                    //        // listOfConnectedClients.Add(clientID - 1); //Adding client to connected list
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "1") //Get User Information
                    //    {
                    //        JObject header = getClientHeader(receivedJSONObject);  //1st JObj  --Header
                    //        string userInfo = getUserInfo(receivedJSONObject["username"].ToString(), receivedJSONObject["password"].ToString(), receivedJSONObject["userType"].ToString());
                    //        userInfo = userInfo.Replace("[", "");
                    //        userInfo = userInfo.Replace("]", "");
                    //        JObject userJason = JObject.Parse(userInfo);    //2nd JObj  --Body
                    //        Console.WriteLine("BODY: {0}", userJason);
                    //        //JsonConvert.DeserializeObject(userInfo);
                    //        header.Merge(userJason);
                    //        Console.WriteLine("MERGED JSON: {0}", header.ToString());
                    //        handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(header.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "2") //Get Restaurant Information
                    //    {
                    //        JObject header = getClientHeader(receivedJSONObject);  //1st JObj  --Header
                    //        string userInfo = getRestaurant(receivedJSONObject["username"].ToString());
                    //        userInfo = userInfo.Replace("[", "");
                    //        userInfo = userInfo.Replace("]", "");
                    //        JObject userJason = JObject.Parse(userInfo);    //2nd JObj  --Body
                    //        Console.WriteLine("BODY: {0}", userJason);
                    //        //JsonConvert.DeserializeObject(userInfo);
                    //        header.Merge(userJason);
                    //        Console.WriteLine("MERGED JSON: {0}", header.ToString());
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(header.ToString()));
                    //        handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "3") // DP login
                    //    {
                    //        string register = deliveryPersonLogin(receivedJSONObject["username"].ToString(), receivedJSONObject["password"].ToString(), receivedJSONObject["userType"].ToString());
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //        //register = "5";
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "4") // Register User
                    //    {
                    //        string register = registerUser(receivedJSONObject);
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "41") // Register Delivery Person for piece of shirt Java, because it's a useless language and nothing ever works
                    //    {
                    //        string register = registerUser(receivedJSONObject);
                    //        register = "5";
                    //        //register += "-1";   //trying to send EOF to Java client
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    if (receivedJSONObject["type"].ToString() == "42")  //First Connection From DP Client
                    //    {
                    //        //Console.WriteLine("First message");
                    //       /*JObject sendJason = new JObject();
                    //        sendJason = sendFirstConnectionInfo();
                    //        String response = sendJason.ToString();
                    //       */
                    //        String rsp="5";
                    //       // response+= "EXIT";   //trying to send EOF to Java client*/
                    //        handler.Send(Encoding.ASCII.GetBytes(rsp));
                    //        // handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(sendJason.ToString()));  //Sending Client ID
                    //        // listOfConnectedClients.Add(clientID - 1); //Adding client to connected list
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "5") //Register Restaurant
                    //    {
                    //        string register = registerRestaurant(receivedJSONObject);
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "6") //Delivery Person Working Hours
                    //    {
                    //        string register = deliveryPersonWorkingHoursRegister(receivedJSONObject);
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "7") //Get list of Categories
                    //    {
                    //        string register = getCategories(receivedJSONObject["restaurantID"].ToString());
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "8") //Add category
                    //    {
                    //        string register = addCategory(receivedJSONObject);
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "9") //9 - Get list of Food
                    //    {
                    //        string register = getFoods(receivedJSONObject["restaurantID"].ToString(), receivedJSONObject["categoryID"].ToString());
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "10") //10 - Add Food
                    //    {
                    //        string register = addFood(receivedJSONObject);
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "11") //11 - Get list of Restaurants
                    //    {
                    //        string register = getRestaurantsList();
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //        //handler.Send(Encoding.GetEncoding("windows-1250").GetBytes(register.ToString()));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "12")
                    //    {
                    //        string register = getOrders(Int32.Parse(receivedJSONObject["restaurantID"].ToString()));
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                            
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "13")
                    //    {
                    //        string register = updateOrderState(Int32.Parse(receivedJSONObject["OrderID"].ToString()), Int32.Parse(receivedJSONObject["newState"].ToString()));
                    //        handler.Send(Encoding.ASCII.GetBytes(register));

                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "14")
                    //    {
                    //        string register = foodByID(receivedJSONObject["foodID"].ToString());
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "15")
                    //    {
                    //        string register = updateDiscount(Int32.Parse(receivedJSONObject["foodID"].ToString()), Double.Parse(receivedJSONObject["discount"].ToString()));
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "16")
                    //    {
                    //        string register = setApproximateDeliveryTime(Int32.Parse(receivedJSONObject["orderID"].ToString()), Int32.Parse(receivedJSONObject["restID"].ToString()));
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "17")
                    //    {
                    //        string register = updateFood(receivedJSONObject);
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }
                    //    else if (receivedJSONObject["type"].ToString() == "18")
                    //    {
                    //        string register = addOrder(receivedJSONObject);
                    //        handler.Send(Encoding.ASCII.GetBytes(register));
                    //    }







                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine(e.ToString());

                    //}


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

      

        private string getRestaurantsList()
        {
            string query = "SELECT restaurantID,name,restaurantDescription,style,owner,phoneNumber, city,zipcode,line1,line2, fromHour,fromMinute,toHour,toMinute,approximateTime FROM Restaurant.Restaurant JOIN Restaurant.RestaurantAddress ON Restaurant.RestaurantAddress.addressID = Restaurant.addressID JOIN Restaurant.OpeningHours ON Restaurant.OpeningHours.openingHoursID = Restaurant.openingHoursID";
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
                        Int32.Parse(dataTable.Rows[i]["approximateTime"].ToString()),
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
            string query = "SELECT foodID,name,price,rating,pictureID,Restaurant.Food.categoryID,Restaurant.Food.restaurantID,availableFrom,availableTo,discount FROM Restaurant.Food JOIN Restaurant.CategoryName ON Restaurant.CategoryName.categoryID = Restaurant.Food.categoryID WHERE Restaurant.Food.restaurantID = @restaurantID AND Restaurant.Food.categoryID = @categoryID";
            DataTable dataTable = new DataTable();
            List<Food> listOfFood = new List<Food>();
            try
            {
                //Console.WriteLine(1);
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@restaurantID", restID);
                command.Parameters.AddWithValue("@categoryID", categoryID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                    return getErrorMessage(70);
                //Console.WriteLine(2);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //Console.WriteLine(3);
                    //GETTING ALLERGENES
                    DataTable dataTable2 = new DataTable();
                    query = "SELECT Restaurant.AllergenNames.name FROM Restaurant.AllergenNames JOIN Restaurant.Allergens ON Restaurant.Allergens.allergenID = Restaurant.AllergenNames.allergenID JOIN Restaurant.Food ON Restaurant.Food.foodID = Restaurant.Allergens.foodID WHERE Restaurant.Allergens.foodID = @foodID";
                    SqlCommand command2 = new SqlCommand(query, DatabaseConnection);
                    //Console.WriteLine(4);
                    command2.Parameters.AddWithValue("@foodID", dataTable.Rows[i]["foodID"].ToString());
                    //Console.WriteLine(5);
                    SqlDataAdapter da2 = new SqlDataAdapter(command2);
                    //Console.WriteLine(6);
                    da2.Fill(dataTable2);
                    //Console.WriteLine(7);
                    List<string> allergens = new List<string>();
                    if (dataTable2.Rows.Count != 0)
                    {
                        //Console.WriteLine(8);
                        for (int k=0;k< dataTable2.Rows.Count;++k)
                            allergens.Add(dataTable2.Rows[k]["name"].ToString());
                        //Console.WriteLine(9);
                    }

                    //Console.WriteLine(10);
                    int picID = 0;
                    Console.WriteLine(dataTable.Rows[i]["pictureID"].ToString());
                    if (dataTable.Rows[i]["pictureID"].ToString() != "")
                    {
                        //Console.WriteLine(11);
                        picID = Int32.Parse(dataTable.Rows[i]["pictureID"].ToString());
                        
                    }
                        
                    //Console.WriteLine(12);
                    double discount = 1;
                    if (dataTable.Rows[i]["discount"].ToString() != "")
                        discount = Double.Parse(dataTable.Rows[i]["discount"].ToString());
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
                        dataTable.Rows[i]["availableTo"].ToString(),
                        discount));

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
                Console.WriteLine("Discount: {0}", listOfFood[i].Discount);
                Console.WriteLine("Allergens:");
                for (int j = 0; j < listOfFood[i].Allergenes.Count; ++j)
                {
                    Console.WriteLine(listOfFood[i].Allergenes[j]);
                }
            }
            FoodList fl = new FoodList(listOfFood);
            return Newtonsoft.Json.JsonConvert.SerializeObject(fl);
        }


        private string foodByID(string foodID)
        {
            string query = "SELECT foodID,name,price,rating,pictureID,Restaurant.Food.categoryID,Restaurant.Food.restaurantID,availableFrom,availableTo,discount FROM Restaurant.Food JOIN Restaurant.CategoryName ON Restaurant.CategoryName.categoryID = Restaurant.Food.categoryID WHERE Restaurant.Food.foodID = @foodID";
            DataTable dataTable = new DataTable();
            try
            {
                //Console.WriteLine(1);
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@foodID", foodID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                    return getErrorMessage(66);
                //Console.WriteLine(2);
                Console.WriteLine(3);
                //GETTING ALLERGENES
                DataTable dataTable2 = new DataTable();
                query = "SELECT Restaurant.AllergenNames.name FROM Restaurant.AllergenNames JOIN Restaurant.Allergens ON Restaurant.Allergens.allergenID = Restaurant.AllergenNames.allergenID JOIN Restaurant.Food ON Restaurant.Food.foodID = Restaurant.Allergens.foodID WHERE Restaurant.Allergens.foodID = @foodID";
                SqlCommand command2 = new SqlCommand(query, DatabaseConnection);
                //Console.WriteLine(4);
                command2.Parameters.AddWithValue("@foodID", foodID);
                //Console.WriteLine(5);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                //Console.WriteLine(6);
                da2.Fill(dataTable2);
                //Console.WriteLine(7);
                List<string> allergens = new List<string>();
                if (dataTable2.Rows.Count != 0)
                {
                    //Console.WriteLine(8);
                    for (int k = 0; k < dataTable2.Rows.Count; ++k)
                        allergens.Add(dataTable2.Rows[k]["name"].ToString());
                    //Console.WriteLine(9);
                }

                //Console.WriteLine(10);
                int picID = 0;
                if (dataTable.Rows[0]["pictureID"].ToString() != "")
                {
                    Console.WriteLine(11);
                    picID = Int32.Parse(dataTable.Rows[0]["pictureID"].ToString());
                }
                double discount = 1;
                if (dataTable.Rows[0]["discount"].ToString() != "")
                    discount = Double.Parse(dataTable.Rows[0]["discount"].ToString());
                Food f = new Food(
                Int32.Parse(dataTable.Rows[0]["foodID"].ToString()),
                dataTable.Rows[0]["name"].ToString(),
                Int32.Parse(dataTable.Rows[0]["price"].ToString()),
                Double.Parse(dataTable.Rows[0]["rating"].ToString()),
                picID,
                allergens,
                Int32.Parse(dataTable.Rows[0]["categoryID"].ToString()),
                Int32.Parse(dataTable.Rows[0]["restaurantID"].ToString()),
                dataTable.Rows[0]["availableFrom"].ToString(),
                dataTable.Rows[0]["availableTo"].ToString(),
                discount);

                da2.Dispose();
                dataTable2.Clear();
                dataTable2.Dispose();
                Console.WriteLine("FoodID: {0}", f.FoodID);
                Console.WriteLine("Name: {0}", f.Name);
                Console.WriteLine("Price: {0}", f.Price);
                Console.WriteLine("Rating: {0}", f.Rating);
                Console.WriteLine("PictureID: {0}", f.PictureID);
                Console.WriteLine("CatID: {0}", f.CategoryID);
                Console.WriteLine("RestID: {0}", f.RestaurantID);
                Console.WriteLine("From: {0}", f.AvailableFrom);
                Console.WriteLine("To: {0}", f.AvailableTo);
                Console.WriteLine("Discount: {0}", f.Discount);
                Console.WriteLine("Allergens:");
                for (int j = 0; j < f.Allergenes.Count; ++j)
                {
                    Console.WriteLine(f.Allergenes[j]);
                }
                da.Dispose();
                dataTable.Clear();
                dataTable.Dispose();
                return Newtonsoft.Json.JsonConvert.SerializeObject(f);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            } 
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

        private string addOrder(JObject obj)
        {
            string username = "";
            JObject o = obj;

            //CHECKING REGISTERED USER
            try
            {
                username = o["username"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("username = o[username].ToString(); FAILED");
            }
            if (checkUsernameAvailable(username, 0) == "1")
            {
                string query1 = "SELECT COUNT(username) FROM Users.Users WHERE username LIKE 'guest%'";
                SqlCommand c = new SqlCommand(query1, DatabaseConnection);
                SqlDataAdapter da1 = new SqlDataAdapter(c);
                DataTable dataTable1 = new DataTable();
                da1.Fill(dataTable1);
                if (dataTable1.Rows.Count == 0)
                {
                    da1.Dispose();
                    dataTable1.Dispose();
                    return getErrorMessage(99);
                }
                int guestCount = 0;
                guestCount = Int32.Parse(dataTable1.Rows[0][0].ToString());
                guestCount++;
                username = "guest" + guestCount.ToString();
                o["username"] = username;
                o.Add("userType", 1);
                o.Add("password", "pass");
                o.Add("email", "email");
                registerUser(o);
            }



            string query2 = "SELECT approximateTime FROM Restaurant.Restaurant WHERE restaurantID = @restID";
            SqlCommand command2 = new SqlCommand(query2, DatabaseConnection);
            command2.Parameters.AddWithValue("@restID", o["restID"].ToString());
            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dataTable2 = new DataTable();
            da2.Fill(dataTable2);
            if (dataTable2.Rows.Count == 0)
            {
                da2.Dispose();
                dataTable2.Dispose();
                return getErrorMessage(71);
            }
            string appTime = "";
            appTime = dataTable2.Rows[0][0].ToString();
            da2.Dispose();
            dataTable2.Dispose();
            if (appTime == "")
                appTime = "10";
            Console.WriteLine("apptime ##### " + appTime);

            string eta = "";
            //CALCULATING DATE
            var dateTime = DateTime.Now;
            DateTime d2 = dateTime.AddMinutes(Int32.Parse(appTime));
            Console.WriteLine("jelenlegi + apptime: " + d2);
            eta = d2.ToShortDateString().Trim() + " " + d2.ToShortTimeString();
            Console.WriteLine("eta: " + eta);
            string startDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            Console.WriteLine("startDate: " + startDate);

            string query3 = "INSERT INTO Restaurant.Orders(restaurantID, username, foods, [status], startOrderTime, price, ETA) VALUES(@restID, @username, @foods, 0, @startDate,@price,@eta)";
            SqlCommand command3 = new SqlCommand(query3, DatabaseConnection);
            command3.Parameters.AddWithValue("@restID", o["restID"].ToString());
            Console.WriteLine(o["restID"].ToString());
            command3.Parameters.AddWithValue("@username", username);
            Console.WriteLine(username);
            command3.Parameters.AddWithValue("@foods", o["foods"].ToString());
            Console.WriteLine(o["foods"].ToString());
            command3.Parameters.AddWithValue("@price", o["price"].ToString());
            Console.WriteLine(o["price"].ToString());
            command3.Parameters.AddWithValue("@startDate", startDate);
            Console.WriteLine(startDate);
            command3.Parameters.AddWithValue("@eta", eta);
            Console.WriteLine(eta);

            int affectedRows = command3.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                return getErrorMessage(99);
            }


            //RETURNING ORDER FOR USER
            string query4 = "SELECT * FROM Restaurant.Orders WHERE restaurantID = @restID AND username = @username AND foods = @foods AND startOrderTime=@startDate AND price = @price";
            SqlCommand command4 = new SqlCommand(query4, DatabaseConnection);
            command4.Parameters.AddWithValue("@restID", o["restID"].ToString());
            Console.WriteLine(o["restID"].ToString());
            command4.Parameters.AddWithValue("@username", username);
            Console.WriteLine(username);
            command4.Parameters.AddWithValue("@foods", o["foods"].ToString());
            Console.WriteLine(o["foods"].ToString());
            command4.Parameters.AddWithValue("@price", o["price"].ToString());
            Console.WriteLine(o["price"].ToString());
            command4.Parameters.AddWithValue("@startDate", startDate);
            Console.WriteLine(startDate);

            SqlDataAdapter da4 = new SqlDataAdapter(command4);
            DataTable dataTable4 = new DataTable();
            da4.Fill(dataTable4);
            if (dataTable4.Rows.Count == 0)
            {
                da4.Dispose();
                dataTable4.Dispose();
                return getErrorMessage(48);
            }
            Order localOrder = new Order(
                                Int32.Parse(dataTable4.Rows[0]["orderID"].ToString()),
                                Int32.Parse(dataTable4.Rows[0]["status"].ToString()),
                                dataTable4.Rows[0]["startOrderTime"].ToString(),
                                dataTable4.Rows[0]["endOrderTime"].ToString(),
                                new User(),
                                Double.Parse(dataTable4.Rows[0]["price"].ToString()),
                                dataTable4.Rows[0]["foods"].ToString(),
                                Int32.Parse(dataTable4.Rows[0]["restaurantID"].ToString()));
            localOrder.Eta = dataTable4.Rows[0]["ETA"].ToString();
            da4.Dispose();
            dataTable4.Dispose();

            return Newtonsoft.Json.JsonConvert.SerializeObject(localOrder);

            //JObject ok2 = new JObject();
            //ok2.Add("type", 18);
            //ok2.Add("status", "Order added succesfully");
            //return ok2.ToString();
        }

        private string getStatusAndETAForUser(int orderID)
        {
            string query = "SELECT [status], ETA FROM Restaurant.Orders WHERE orderID = @orderID";
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@orderID", orderID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                {
                    da.Dispose();
                    return getErrorMessage(48);
                }
                da.Dispose();
                JObject ok = new JObject();
                ok.Add("type", 23);
                ok.Add("status", dataTable.Rows[0]["status"].ToString());
                ok.Add("ETA", dataTable.Rows[0]["ETA"].ToString());
                return ok.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
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
           
            string query = "EXEC registerRestaurant @username, @pass, @lastName, @firstName, @phone, @email, @name, @restaurantDescription, @style, @city, @zipcode, @line1, @line2, @fromHour ,@fromMinute, @toHour, @toMinute, @approx";
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
                command.Parameters.AddWithValue("@approx", Int32.Parse(o["approximateTime"].ToString()));
                Console.WriteLine(o["approximateTime"].ToString());
                
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

        private string deliveryPersonWorkingHoursRegister(JObject o)
        {
            string query = "INSERT INTO DeliveryPerson.WorkingHours(username ,fromHour,fromMinute,toHour,toMinute ,workingDays) VALUES (@username, @fromHour ,@fromMinute, @toHour, @toMinute, @days)";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", o["Username"].ToString());
                Console.WriteLine(o["Username"].ToString());
                command.Parameters.AddWithValue("@fromHour", Int32.Parse(o["FromHour"].ToString()));
                Console.WriteLine(o["FromHour"].ToString());
                command.Parameters.AddWithValue("@fromMinute", Int32.Parse(o["FromMinute"].ToString()));
                Console.WriteLine(o["FromMinute"].ToString());
                command.Parameters.AddWithValue("@toHour", Int32.Parse(o["ToHour"].ToString()));
                Console.WriteLine(o["ToHour"].ToString());
                command.Parameters.AddWithValue("@toMinute", Int32.Parse(o["ToMinute"].ToString()));
                Console.WriteLine(o["ToMinute"].ToString());
                command.Parameters.AddWithValue("@days", o["WorkingDays"].ToString()); //workingDays
                Console.WriteLine(o["WorkingDays"].ToString());

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(99);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type", 6);
            ok.Add("status", "Successful");
            return ok.ToString(); 
        }

        private string updateDiscount(int foodID, double discount)
        {
            string query = "UPDATE Restaurant.Food SET discount = @discount WHERE foodID = @foodID";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@foodID", foodID);
                Console.WriteLine(foodID);
                command.Parameters.AddWithValue("@discount", discount);
                Console.WriteLine(discount);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(66);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type", 15);
            ok.Add("status", "Successful");
            return ok.ToString();

        }

        private string setApproximateDeliveryTime(int orderID, int restaurantID)
        {
            string query = "UPDATE Restaurant.Orders SET ETA = @eta WHERE orderID = @orderID AND restaurantID = @restaurantID";
            string eta = "";
            //CALCULATING DATE
            var dateTime = DateTime.Now;
            DateTime d2 = dateTime.AddMinutes(10);
            eta = d2.ToShortDateString().Trim() + " " + d2.ToShortTimeString();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@orderID", orderID);
                Console.WriteLine(orderID);
                command.Parameters.AddWithValue("@restaurantID", restaurantID);
                Console.WriteLine(restaurantID);
                command.Parameters.AddWithValue("@eta", eta);
                Console.WriteLine(eta);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(66);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type", 16);
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

                if (Int32.Parse(o["userType"].ToString()) == 2)
                {
                    string query1 = "INSERT INTO DeliveryPerson.DeliveryPersonOrders(username) VALUES (@username)";
                    SqlCommand command1 = new SqlCommand(query1, DatabaseConnection);
                    command1.Parameters.AddWithValue("@username", o["username"].ToString());
                    int affectedRows1 = command1.ExecuteNonQuery();
                    if (affectedRows1 == 0)
                    {
                        return getErrorMessage(99);
                    }

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


        private string getDeliveryPersonList()
        {
            
            string query = "SELECT * FROM DeliveryPerson.DeliveryPersonOrders JOIN DeliveryPerson.WorkingHours ON DeliveryPerson.WorkingHours.username = DeliveryPerson.DeliveryPersonOrders.username";
            List<DeliveryBoy> bois = new List<DeliveryBoy>();
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                {
                    da.Dispose();
                    return getErrorMessage(99);
                }
                
                da.Dispose();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //         public workingSchedule(int ID, int fromH, int toH, int fromM, int toM, string workinDays)
                    bois.Add(new DeliveryBoy(Int32.Parse(dataTable.Rows[i]["id"].ToString()),
                                        dataTable.Rows[i]["username"].ToString(),
                                    new workingSchedule(
                                    Int32.Parse(dataTable.Rows[i]["workingHoursID"].ToString()),
                                    Int32.Parse(dataTable.Rows[i]["fromHour"].ToString()),
                                    Int32.Parse(dataTable.Rows[i]["toHour"].ToString()),
                                    Int32.Parse(dataTable.Rows[i]["fromMinute"].ToString()),
                                    Int32.Parse(dataTable.Rows[i]["toMinute"].ToString()),
                                    dataTable.Rows[i]["workingDays"].ToString())));


                    //GETTING ORDERS FOR DELIVERY PERSON
                    string query1 = "SELECT Restaurant.Orders.restaurantID, DeliveryPerson.AssignDelivery.orderID, [status], startOrderTime, endOrderTime, Orders.[username], price, foods,[password],[lastName],[firstName],Users.[phoneNumber],Users.[addressID] ,[userType], Users.[email],UsersAddress.city,UsersAddress.line1,UsersAddress.line2,UsersAddress.zipcode FROM DeliveryPerson.AssignDelivery JOIN Restaurant.Orders ON Restaurant.Orders.orderID = DeliveryPerson.AssignDelivery.orderID JOIN Users.Users ON Users.username = Restaurant.Orders.username JOIN Users.UsersAddress ON UsersAddress.addressID = Users.addressID WHERE deliveryPersonID = @id";
                    List<Order> localOrders = new List<Order>();
                    try
                    {
                        DataTable dataTable1 = new DataTable();
                        SqlCommand command1 = new SqlCommand(query1, DatabaseConnection);
                        command1.Parameters.AddWithValue("@id", bois[i].DeliveryBoyID);
                        SqlDataAdapter da1 = new SqlDataAdapter(command1);
                        da1.Fill(dataTable1);
                        //if (dataTable1.Rows.Count == 0)
                        //    return getErrorMessage(99);
                        for (int j = 0; j < dataTable1.Rows.Count; j++)
                        {
                            localOrders.Add(
                                new Order(
                                Int32.Parse(dataTable1.Rows[j]["orderID"].ToString()),
                                Int32.Parse(dataTable1.Rows[j]["status"].ToString()),
                                dataTable1.Rows[j]["startOrderTime"].ToString(),
                                dataTable1.Rows[j]["endOrderTime"].ToString(),
                                new User(dataTable1.Rows[j]["username"].ToString(), dataTable1.Rows[j]["password"].ToString(), dataTable1.Rows[j]["lastName"].ToString(),
                                dataTable1.Rows[j]["firstName"].ToString(), dataTable1.Rows[j]["phoneNumber"].ToString(), dataTable1.Rows[j]["city"].ToString(),
                                dataTable1.Rows[j]["zipcode"].ToString(), dataTable1.Rows[j]["line1"].ToString(), dataTable1.Rows[j]["line2"].ToString(),
                                Int32.Parse(dataTable1.Rows[j]["userType"].ToString()), dataTable1.Rows[j]["email"].ToString()),
                                Double.Parse(dataTable1.Rows[j]["price"].ToString()),
                                dataTable1.Rows[j]["foods"].ToString(),
                                Int32.Parse(dataTable1.Rows[j]["restaurantID"].ToString())));
                        }
                        da1.Dispose();
                        dataTable1.Clear();
                        dataTable1.Dispose();
                        for (int k = 0; k < localOrders.Count; ++k)
                        {
                            Console.WriteLine("OrderID: {0}", localOrders[k].OrderID);
                            Console.WriteLine("OrderStatus: {0}", localOrders[k].OrderStatus);
                            Console.WriteLine("OrderTime: {0}", localOrders[k].OrderTime);
                            Console.WriteLine("EndorderTime: {0}", localOrders[k].EndorderTime);
                            Console.WriteLine("Customer (User): {0}", localOrders[k].User.toString());
                            Console.WriteLine("TotalPrice: {0}", localOrders[k].TotalPrice);
                            Console.WriteLine("Foods: {0}", localOrders[k].Foods);
                        }
                        bois[i].Orders = localOrders;
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        return getErrorMessage(92);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }

            DeliveryBoyList bl = new DeliveryBoyList();
            bl.ListDevliveryboy = bois;
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(bl);
            Console.WriteLine("JSON:\n {0}", jsonString);
            return jsonString;

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
                string ret = "";
                ret = JsonConvert.SerializeObject(dataTable);
                ret = ret.Replace("[", "");
                ret = ret.Replace("]", "");
                Console.WriteLine("TABLE: {0}", ret);
                if (userType == "2")
                {
                    DataTable dataTable2 = new DataTable();
                    string query2 = "SELECT id FROM DeliveryPerson.DeliveryPersonOrders WHERE username=@username";
                    SqlCommand command2 = new SqlCommand(query2, DatabaseConnection);
                    command2.Parameters.AddWithValue("@username", username);
                    SqlDataAdapter da2 = new SqlDataAdapter(command2);
                    da2.Fill(dataTable2);
                    if (dataTable2.Rows.Count == 0)
                    {
                        da2.Dispose();
                        return getErrorMessage(91);
                    }
                    int id = Int32.Parse(dataTable2.Rows[0][0].ToString());
                    da2.Dispose();
                    JObject obj = new JObject();
                    obj = JObject.Parse(ret);
                    Console.WriteLine(ret);
                    obj.Add("deliveryPersonID", id);
                    return obj.ToString();
                }
                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }   
        }

        private string deliveryPersonLogin(string username, string pass, int userType)
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
                if (dataTable.Rows.Count == 0)
                {
                    da.Dispose();
                    //return getErrorMessage(91);
                    return "1";
                }
                //Console.WriteLine("rows: {0}", dataTable.ToString());
                da.Dispose();
                //Console.WriteLine("TABLE: {0}", JsonConvert.SerializeObject(dataTable));
                //return JsonConvert.SerializeObject(dataTable);
                return "5";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //return getErrorMessage(99);
                return "1";
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

        private string updateFood(JObject o)
        {
            string query = "UPDATE Restaurant.Food SET name =@foodName, price=@price, rating = @rating, availableFrom=@availableFrom, availableTo=@availableTo  WHERE foodID=@foodID";
            SqlCommand command5 = new SqlCommand(query, DatabaseConnection);
            command5.Parameters.AddWithValue("@foodID", o["FoodID"].ToString());
            Console.WriteLine(o["FoodID"].ToString());
            command5.Parameters.AddWithValue("@foodName", o["Name"].ToString());
            Console.WriteLine(o["Name"].ToString());
            command5.Parameters.AddWithValue("@price", Double.Parse(o["Price"].ToString()));
            Console.WriteLine(o["Price"].ToString());
            command5.Parameters.AddWithValue("@rating", o["Rating"].ToString());
            Console.WriteLine(o["Rating"].ToString());
            command5.Parameters.AddWithValue("@availableFrom", o["AvailableFrom"].ToString());
            Console.WriteLine(o["AvailableFrom"].ToString());
            command5.Parameters.AddWithValue("@availableTo", o["AvailableTo"].ToString());
            Console.WriteLine(o["AvailableTo"].ToString());

            int affectedRows = command5.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                return getErrorMessage(66);
            }
            string foodID = (o["FoodID"].ToString());

            query = "DELETE FROM Restaurant.Allergens WHERE foodID=@foodID";
            SqlCommand command = new SqlCommand(query, DatabaseConnection);
            command.Parameters.AddWithValue("@foodID", foodID);
            command.ExecuteNonQuery();

            //FOOD ADDED, NOT LET'S SEE IF ALLERGEN EXISTS OR NOT
            List<string> allergens = new List<string>();
            allergens = JsonConvert.DeserializeObject<List<string>>(o["Allergenes"].ToString());
            for (int i = 0; i < allergens.Count; ++i)
            {
                query = "SELECT allergenID FROM Restaurant.AllergenNames WHERE name = @allergenName";
                SqlCommand command1 = new SqlCommand(query, DatabaseConnection);
                command1.Parameters.AddWithValue("@allergenName", allergens[i]);
                SqlDataAdapter da2 = new SqlDataAdapter(command1);
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

                query = "INSERT INTO Restaurant.Allergens(allergenID,foodID) VALUES(@allergenID,@foodID)";
                SqlCommand command3 = new SqlCommand(query, DatabaseConnection);
                command3.Parameters.AddWithValue("@allergenID", Int32.Parse(allergenID));
                Console.WriteLine("allergenID: {0}", allergenID);
                command3.Parameters.AddWithValue("@foodID", Int32.Parse(foodID));
                Console.WriteLine("foodID: {0}", foodID);
                affectedRows = command3.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(68);
                }
            }
            JObject ok2 = new JObject();
            ok2.Add("type", 17);
            ok2.Add("status", "Food MODIFIED succesfully");
            ok2.Add("foodID", foodID);
            return ok2.ToString();
        }



        private string addFood(JObject o)
        {
            string query = "DECLARE @returnID INT EXEC @returnID = addFood @foodName,@price,@rating,@categoryID,@restaurantID,@availableFrom,@availableTo, @discount SELECT  'foodID' = @returnID";
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
            command5.Parameters.AddWithValue("@discount", o["Discount"].ToString());
            Console.WriteLine(o["Discount"].ToString());
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

        //private string addOrder(JObject obj)  MIÉRT VAN EZ ITT MÉG EGYSZER??
        //{
        //    string username = "";
        //    JObject o = obj;
            
        //    //CHECKING REGISTERED USER
        //    try
        //    {
        //        username = o["username"].ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("username = o[username].ToString(); FAILED");
        //    }
        //    if (checkUsernameAvailable(username, 0) == "1")
        //    {
        //        string query3 = "SELECT COUNT(username) FROM Users.Users WHERE username LIKE 'guest%'";
        //        SqlCommand c = new SqlCommand(query3, DatabaseConnection);
        //        SqlDataAdapter da1 = new SqlDataAdapter(c);
        //        DataTable dataTable1 = new DataTable();
        //        da1.Fill(dataTable1);
        //        if (dataTable1.Rows.Count == 0)
        //        {
        //            da1.Dispose();
        //            dataTable1.Dispose();
        //            return getErrorMessage(99);
        //        }
        //        int guestCount = 0;
        //        guestCount = Int32.Parse(dataTable1.Rows[0][0].ToString());
        //        guestCount++;
        //        username = "guest" + guestCount.ToString();
        //        o["username"] = username;
        //        o.Add("userType",1);
        //        o.Add("password", "pass");
        //        o.Add("email", "email");
        //        registerUser(o);
        //    }


            
        //    string query2 = "SELECT approximateTime FROM Restaurant.Restaurant WHERE restaurantID = @restID";
        //    SqlCommand command = new SqlCommand(query2, DatabaseConnection);
        //    command.Parameters.AddWithValue("@restID", o["restID"].ToString());
        //    SqlDataAdapter da = new SqlDataAdapter(command);
        //    DataTable dataTable4 = new DataTable();
        //    da.Fill(dataTable4);
        //    if (dataTable4.Rows.Count == 0)
        //    {
        //        da.Dispose();
        //        dataTable4.Dispose();
        //        return getErrorMessage(71);
        //    }
        //    string appTime = "";
        //    appTime = dataTable4.Rows[0][0].ToString();
        //    da.Dispose();
        //    dataTable4.Dispose();
        //    if (appTime == "")
        //        appTime = "10";

        //    string eta = "";
        //    //CALCULATING DATE
        //    var dateTime = DateTime.Now;
        //    DateTime d2 = dateTime.AddMinutes(Int32.Parse(appTime));
        //    eta = d2.ToShortDateString().Trim() + " " + d2.ToShortTimeString();
        //    string startDate = d2.ToShortDateString().Trim() + " " + d2.ToShortTimeString();

        //    string query = "INSERT INTO Restaurant.Orders(restaurantID, username, foods, [status], startOrderTime, price, ETA) VALUES(@restID, @username, @foods, 0, @startDate,@price,@eta)";
        //    SqlCommand command5 = new SqlCommand(query, DatabaseConnection);
        //    command5.Parameters.AddWithValue("@restID", o["restID"].ToString());
        //    Console.WriteLine(o["restID"].ToString());
        //    command5.Parameters.AddWithValue("@username", username);
        //    Console.WriteLine(username);
        //    command5.Parameters.AddWithValue("@foods", o["foods"].ToString());
        //    Console.WriteLine(o["foods"].ToString());
        //    command5.Parameters.AddWithValue("@price", o["price"].ToString());
        //    Console.WriteLine(o["price"].ToString());
        //    command5.Parameters.AddWithValue("@startDate", startDate);
        //    Console.WriteLine(startDate);
        //    command5.Parameters.AddWithValue("@eta", eta);
        //    Console.WriteLine(eta);

        //    int affectedRows = command5.ExecuteNonQuery();
        //    if (affectedRows == 0)
        //    {
        //        return getErrorMessage(99);
        //    }

        //    JObject ok2 = new JObject();
        //    ok2.Add("type", 18);
        //    ok2.Add("status", "Order added succesfully");
        //    return ok2.ToString();
        //}

        private string addOrderToDeliveryPerson(int boiID, int orderID)
        {
            string query = "INSERT INTO DeliveryPerson.AssignDelivery(deliveryPersonID,orderID) VALUES (@boiID,@orderID)";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@boiID", boiID);
                Console.WriteLine("boiID: {0}", boiID);
                command.Parameters.AddWithValue("@orderID", orderID);
                Console.WriteLine("orderID: {0}", orderID);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(50);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type", 20);
            ok.Add("status", "Order added to Delivery Person");
            return ok.ToString();

        }

        private string getDeliveryPersonOrders(int boiID)
        {
            string query = "SELECT orderID FROM DeliveryPerson.AssignDelivery WHERE deliveryPersonID=@boiID";
            
            List<Order> orders = new List<Order>();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@boiID", boiID);
                Console.WriteLine("boiID: {0}", boiID);
                DataTable dataTable = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                    return getErrorMessage(49);
                Console.WriteLine("datatb count: " + dataTable.Rows.Count);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Console.WriteLine("orderid: " + dataTable.Rows[i]["orderID"].ToString());
                    string query2 = "SELECT Restaurant.restaurantID, Restaurant.name, orderID, [status], startOrderTime, endOrderTime, Orders.[username], price, foods,[password],[lastName],[firstName],Users.[phoneNumber],Users.[addressID] ,[userType], Users.[email],UsersAddress.city,UsersAddress.line1,UsersAddress.line2,UsersAddress.zipcode FROM Restaurant.Orders JOIN Restaurant.Restaurant ON Restaurant.restaurantID = Orders.restaurantID JOIN Users.Users ON Users.username = Restaurant.Orders.username JOIN Users.UsersAddress ON UsersAddress.addressID = Users.addressID WHERE Orders.orderID = @orderID";
                    SqlCommand command2 = new SqlCommand(query2, DatabaseConnection);
                    command2.Parameters.AddWithValue("@orderID", Int32.Parse(dataTable.Rows[i]["orderID"].ToString()));
                    DataTable dataTable2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter(command2);
                    da2.Fill(dataTable2);
                        orders.Add(
                            new Order(
                            Int32.Parse(dataTable2.Rows[0]["orderID"].ToString()),
                            Int32.Parse(dataTable2.Rows[0]["status"].ToString()),
                            dataTable2.Rows[0]["startOrderTime"].ToString(),
                            dataTable2.Rows[0]["endOrderTime"].ToString(),
                            new User(dataTable2.Rows[0]["username"].ToString(), 
                            dataTable2.Rows[0]["password"].ToString(), 
                            dataTable2.Rows[0]["lastName"].ToString(),
                            dataTable2.Rows[0]["firstName"].ToString(), dataTable2.Rows[0]["phoneNumber"].ToString(),
                            dataTable2.Rows[0]["city"].ToString(),
                            dataTable2.Rows[0]["zipcode"].ToString(), dataTable2.Rows[0]["line1"].ToString(), 
                            dataTable2.Rows[0]["line2"].ToString(),
                            Int32.Parse(dataTable2.Rows[0]["userType"].ToString()), dataTable2.Rows[0]["email"].ToString()),
                            Double.Parse(dataTable2.Rows[0]["price"].ToString()),
                            dataTable2.Rows[0]["foods"].ToString(),
                            Int32.Parse(dataTable2.Rows[0]["restaurantID"].ToString()),
                            dataTable2.Rows[0]["name"].ToString()));
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
                return getErrorMessage(99);
            }
            OrderList ol = new OrderList(orders);
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ol);
            Console.WriteLine("JSON:\n {0}", jsonString);
            return jsonString;
        }


        private string deleteOrderFromDeliveryPerson(int boiID, int orderID)
        {
            string query = "DELETE FROM DeliveryPerson.AssignDelivery WHERE deliveryPersonID=@boiID AND orderID=@orderID";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@boiID", boiID);
                Console.WriteLine("boiID: {0}", boiID);
                command.Parameters.AddWithValue("@orderID", orderID);
                Console.WriteLine("orderID: {0}", orderID);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(50);
                }

                updateOrderState(orderID,2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type", 21);
            ok.Add("status", "Order removed from Delivery Person");
            return ok.ToString();

        }



        private string getOrders(int restaurantID)
        {
            string query = "SELECT Restaurant.restaurantID, orderID, [status], startOrderTime, endOrderTime, Orders.[username], price, foods,[password],[lastName],[firstName],Users.[phoneNumber],Users.[addressID] ,[userType], Users.[email],UsersAddress.city,UsersAddress.line1,UsersAddress.line2,UsersAddress.zipcode FROM Restaurant.Orders JOIN Restaurant.Restaurant ON Restaurant.restaurantID = Orders.restaurantID JOIN Users.Users ON Users.username = Restaurant.Orders.username JOIN Users.UsersAddress ON UsersAddress.addressID = Users.addressID WHERE Orders.restaurantID = @restID";
            List<Order> localOrders = new List<Order>();
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@restID", restaurantID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
                    return getErrorMessage(92);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    localOrders.Add(
                        new Order(
                        Int32.Parse(dataTable.Rows[i]["orderID"].ToString()),
                        Int32.Parse(dataTable.Rows[i]["status"].ToString()),
                        dataTable.Rows[i]["startOrderTime"].ToString(),
                        dataTable.Rows[i]["endOrderTime"].ToString(),
                        new User (dataTable.Rows[i]["username"].ToString(), dataTable.Rows[i]["password"].ToString(), dataTable.Rows[i]["lastName"].ToString(),
                        dataTable.Rows[i]["firstName"].ToString(), dataTable.Rows[i]["phoneNumber"].ToString(), dataTable.Rows[i]["city"].ToString(),
                        dataTable.Rows[i]["zipcode"].ToString(), dataTable.Rows[i]["line1"].ToString(), dataTable.Rows[i]["line2"].ToString(),
                        Int32.Parse(dataTable.Rows[i]["userType"].ToString()), dataTable.Rows[i]["email"].ToString()),
                        Double.Parse(dataTable.Rows[i]["price"].ToString()),
                        dataTable.Rows[i]["foods"].ToString(),
                        Int32.Parse(dataTable.Rows[i]["restaurantID"].ToString())));
                }
                da.Dispose();
                dataTable.Clear();
                dataTable.Dispose();
                for (int k = 0; k < localOrders.Count; ++k)
                {
                    Console.WriteLine("OrderID: {0}", localOrders[k].OrderID);
                    Console.WriteLine("OrderStatus: {0}", localOrders[k].OrderStatus);
                    Console.WriteLine("OrderTime: {0}", localOrders[k].OrderTime);
                    Console.WriteLine("EndorderTime: {0}", localOrders[k].EndorderTime);
                    Console.WriteLine("Customer (User): {0}", localOrders[k].User.toString());
                    Console.WriteLine("TotalPrice: {0}", localOrders[k].TotalPrice);
                    Console.WriteLine("Foods: {0}", localOrders[k].Foods);
                }
                OrderList ol = new OrderList(localOrders);
                //Console.WriteLine("Heyyoooo");
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ol);
                Console.WriteLine("JSON:\n {0}",jsonString);
                return jsonString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(92);
            }
        }


        private string updateOrderState(int orderID, int status)
        {
            if (status == 4)
            {
                string query1 = "SELECT deliveryPersonID FROM DeliveryPerson.AssignDelivery WHERE orderID = @orderID";
                SqlCommand command1 = new SqlCommand(query1, DatabaseConnection);
                command1.Parameters.AddWithValue("@orderID", orderID);
                //Console.WriteLine("orderID: {0}", orderID);
                DataTable dataTable1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(command1);
                da1.Fill(dataTable1);
                if (dataTable1.Rows.Count == 0)
                    return getErrorMessage(92);
                int boiID = Int32.Parse(dataTable1.Rows[0][0].ToString());
                deleteOrderFromDeliveryPerson(boiID, orderID);
            }
            string query = "UPDATE Restaurant.Orders SET [status] = @status WHERE orderID = @orderID";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@status", status);
                Console.WriteLine("Status: {0}",status);
                command.Parameters.AddWithValue("@orderID", orderID);
                Console.WriteLine("orderID: {0}", orderID);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    return getErrorMessage(67);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return getErrorMessage(99);
            }
            JObject ok = new JObject();
            ok.Add("type", 13);
            ok.Add("status", "Successful status update");
            return ok.ToString();
        }




        private string getErrorMessage(int errorNumber)
        {
            JObject errorObject = new JObject();
            errorObject.Add("type", 99);
            switch (errorNumber) 
            {
                case 48:
                    errorObject.Add("error", "Order not found");
                    break;
                case 49:
                    errorObject.Add("error", "Delivery Person ID not found");
                    break;
                case 50:
                    errorObject.Add("error", "Delivery Person ID or Order ID not found");
                    break;
                case 66:
                    errorObject.Add("error", "FoodID not found");
                    break;
                case 67:
                    errorObject.Add("error", "attempt to update Order Status failed");
                    break;
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


        //private void waitingForConnection()
        //{
        //    Console.WriteLine("Waiting for a connection...");
        //    handler = listener.Accept();
        //}
    }
}

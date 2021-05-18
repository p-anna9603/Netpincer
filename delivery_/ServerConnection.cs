using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace delivery_
{
    public class ServerConnection
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

        public ServerConnection()
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
            string receivedMsg = sendJSON(sendFirstConnectionInfo());

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
            //StopClient();
        }

        private string sendJSON(JObject JsonSendObject)     //Returns received string
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
                    var ret = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    StopClient();
                    return ret;
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
            return "";

        }

        public User getUser(string username, string password, UserType userType)   //Returns the User object for the given username ands password (if the parameters are wrong the server dies rip so don't do that)
        {
            try
            {
                JObject jobc = new JObject();
                jobc.Add("type", 1);    // 1 - User login
                jobc.Add("clientID", clientID);
                jobc.Add("username", username);
                jobc.Add("password", password);
                //int userTypeNumber = 4;
                jobc.Add("userType", 2);
                string recievedMsg = sendJSON(jobc);
                Console.WriteLine("recievedMsg: {0}", recievedMsg);

                JObject receivedJSonObject = new JObject();
                receivedJSonObject = JObject.Parse(recievedMsg);
                if (receivedJSonObject["type"].ToString() == "1")
                {
                    //var root = JObject.Parse(jsonString);
                    //return receivedJSonObject["User"].ToObject<User>();   //WIP
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
                        receivedJSonObject["email"].ToString(),
                        Int32.Parse(receivedJSonObject["deliveryPersonID"].ToString())
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
            User a = new User();
            a.empty = true;
            return a;

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

        public int setWorkingHours(WorkingHours wh)
        {
            int retval = 1;
            //THIS WORKS:
            try
            {
                //Console.WriteLine("Basic workingString: {0}", wh.toString());
                string workingString = JsonConvert.SerializeObject(wh);
                Console.WriteLine("String workingString: {0}", workingString);
                JObject header = new JObject();
                header.Add("type", 6);
                //header.Add("username", wh.username);
                JObject body = new JObject();
                body = JObject.Parse(workingString);
                header.Merge(body);
                string recievedMsg = sendJSON(header);
                Console.WriteLine("recievedMsg: {0}", recievedMsg);

                JObject receivedJSonObject = new JObject();
                receivedJSonObject = JObject.Parse(recievedMsg);
                if (receivedJSonObject["type"].ToString() == "6")
                {
                    Console.WriteLine("Server: {0}", receivedJSonObject["status"].ToString());
                    System.Windows.Forms.MessageBox.Show("Sikeres munkarend állítás!");
                }
                else if (receivedJSonObject["type"].ToString() == "99")
                {
                    Console.WriteLine("Error: {0}", receivedJSonObject["error"].ToString());
                    System.Windows.Forms.MessageBox.Show("Sikertelen munkarend állítás!");
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
                /*
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
                */
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

        public void removeOrderFromDeliveryBoy(int boiID, int orderID)
        {

            try
            {
                JObject obj = new JObject();
                obj.Add("type", 21);
                obj.Add("boiID", boiID);
                obj.Add("orderID", orderID);
                string recievedMsg = sendJSON(obj);
                Console.WriteLine("recievedMsg: {0}", recievedMsg);
                JObject receivedJSonObject = new JObject();
                receivedJSonObject = JObject.Parse(recievedMsg);
                if (receivedJSonObject["type"].ToString() == "21")
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

        public OrderList getOrders(int boiID)
        {

            try
            {
                JObject obj = new JObject();
                obj.Add("type", 22);
                obj.Add("boiID", boiID);
                string recievedMsg = sendJSON(obj);
                Console.WriteLine("recievedMsg: {0}", recievedMsg);

                OrderList receivedOrderList = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderList>(recievedMsg);
                if (receivedOrderList.ListOrder == null)
                {
                    Console.WriteLine("ListOrder == null");
                    throw new Exception();
                }
                return receivedOrderList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return new OrderList(true);
        }

        



    }
}


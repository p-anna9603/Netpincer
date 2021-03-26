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
            /*
            * Right Click on Project > Manage Nuget Packages > Search & install 'System.Data.SqlClient'
            */
            DatabaseConnection = ConnectToDatabase();
            Console.WriteLine("Connection State: {0}", DatabaseConnection.State);
            clientID = 0;
            listOfConnectedClients = new List<int>();
            StartServer();

            DatabaseConnection.Close();
        }

        public void StartServer()
        {
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);


            try
            {

                // Create a Socket that will use Tcp protocol      
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method  
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                listener.Listen(10);

                //TESTING
                //TEST JSON OBJECT
                JObject jasonObjectTest = new JObject();
                jasonObjectTest = JObject.Parse(
                                    @"{'type': '1',
                                    'clientID': '0',
                                    'username': 'testUser',
                                     'password': 't3stpassword',
                                      'userType': '1'
                              }");
                //{
                //    JObject header = getClientHeader(jasonObjectTest);  //1st JObj  --Header
                //    Console.WriteLine("0 {0}", header.ToString());
                //    string userInfo = getUserInfo(jasonObjectTest["username"].ToString(), jasonObjectTest["password"].ToString());
                //    userInfo = userInfo.Replace("[", "");
                //    userInfo = userInfo.Replace("]", "");
                //    Console.WriteLine("1 {0}", userInfo);
                //    JObject userJason = JObject.Parse(userInfo);    //2nd JObj  --Body
                //    //JObject userJason = (JObject)JsonConvert.DeserializeObject(userInfo);
                //    Console.WriteLine("1.1 {0}", userJason.ToString());
                //    //JObject userJason = JObject.Parse(stringUser);
                //    Console.WriteLine("2");                                               //JsonConvert.DeserializeObject(userInfo);
                //    header.Merge(userJason);
                //    Console.WriteLine("3");
                //    Console.WriteLine("MERGED JSON: {0}", header.ToString());
                //    //handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
                //}

                //Console.WriteLine("Waiting for a connection...");
                //Socket handler = listener.Accept();
                waitingForConnection();

                // Incoming data from the client.    
                string data = null;
                byte[] bytes = null;
                bool shutdown = false;

                while (!shutdown)
                {
                    while (true)
                    {
                        //if (handler.Poll(1000,SelectMode.SelectRead))
                        {
                        //    waitingForConnection();
                        }
                        bytes = new byte[2048];
                        int bytesRec = handler.Receive(bytes);
                        //Console.WriteLine("bytes recieved {0}", bytesRec);
                        if (bytesRec != 0)
                        {
                            data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            break;
                        }
                    }

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
                            handler.Send(Encoding.ASCII.GetBytes(sendJason.ToString()));  //Sending Client ID
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
                        }
                        else if (receivedJSONObject["type"].ToString() == "4") //Get Register User
                        {
                            string register = registerUser(receivedJSONObject);
                            handler.Send(Encoding.ASCII.GetBytes(register));
                        }
                        else if (receivedJSONObject["type"].ToString() == "5") //Get Register Restaurant
                        {
                            string register = registerRestaurant(receivedJSONObject);
                            handler.Send(Encoding.ASCII.GetBytes(register));
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

        private string registerRestaurant(JObject o)
        {
            string query = "EXEC registerRestaurant @username, @pass, @lastName, @firstName, @phone, @email, @name, @restaurantDescription, @style, @city, @zipcode, @line1, @line2, @fromHour ,@fromMinute, @toHour, @toMinute";
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", o["username"].ToString());
                Console.WriteLine(o["username"].ToString());
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
            //should check if user already exists or not    --WIP
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
            //Getting user info from database 
            Console.WriteLine("UserType wtf: {0}", userType);
            //int user = Int32.Parse(userType);
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


        private string getErrorMessage(int errorNumber)
        {
            JObject errorObject = new JObject();
            errorObject.Add("type", 99);
            switch (errorNumber) 
            {
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

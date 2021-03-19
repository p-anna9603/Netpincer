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
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method  
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                listener.Listen(10);

                //TESTING
                //TEST JSON OBJECT
                JObject jasonObjectTest = new JObject();
                jasonObjectTest = JObject.Parse(
                                    @"{    'type': '1',
                                    'clientID': '0',
                                    'username': 'testUser',
                                     'password': 't3stpassword'
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

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.    
                string data = null;
                byte[] bytes = null;
                bool shutdown = false;

                while (!shutdown)
                {
                    while (true)
                    {
                        bytes = new byte[1024];
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

                        

                        JObject jasonObject = new JObject();
                        jasonObject = JObject.Parse(data);
                        Console.WriteLine("Received JSON: {0}", jasonObject.ToString());
                        if (jasonObject["type"].ToString() == "0")  //First Connection From Client
                        {
                            //Console.WriteLine("First message");
                            JObject sendJason = new JObject();
                            sendJason = sendFirstConnectionInfo();
                            handler.Send(Encoding.ASCII.GetBytes(sendJason.ToString()));  //Sending Client ID
                            listOfConnectedClients.Add(clientID - 1); //Adding client to connected list
                        }
                         else if (jasonObject["type"].ToString() == "1") //Get User Information
                        {
                            JObject header = getClientHeader(jasonObjectTest);  //1st JObj  --Header
                            string userInfo=getUserInfo(jasonObjectTest["username"].ToString(), jasonObjectTest["password"].ToString());
                            userInfo = userInfo.Replace("[", "");
                            userInfo = userInfo.Replace("]", "");
                            //Console.WriteLine("HEADER: {0}", userInfo);
                            // string userInfo=getUserInfo("testUser", "t3stpassword");
                            JObject userJason = JObject.Parse(userInfo);    //2nd JObj  --Body
                            Console.WriteLine("BODY: {0}", userJason);
                            //JsonConvert.DeserializeObject(userInfo);
                            header.Merge(userJason);
                            Console.WriteLine("MERGED JSON: {0}",header.ToString());
                            handler.Send(Encoding.ASCII.GetBytes(header.ToString()));
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

        public static SqlConnection ConnectToDatabase()
        {
            //                      localhost             DatabaseName     UserName     UserPassw
            String cnn = "Data Source=localhost;Initial Catalog=Netpincer;User ID=sa;Password=passw0rd";
            SqlConnection db = new SqlConnection(cnn);
            db.Open();
            //Console.WriteLine("Conn State: {0}", db.State);
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


        private string getUserInfo(string username, string pass)
        {
            //Getting user info from database
            string query = "SELECT* FROM getUser(@username, @pass)";
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand(query, DatabaseConnection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@pass", pass);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                //Console.WriteLine("rows: {0}", dataTable.ToString());
                da.Dispose();
                Console.WriteLine("TABLE: {0}", JsonConvert.SerializeObject(dataTable));
                return JsonConvert.SerializeObject(dataTable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }   
        }

    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestaurantClient;

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
    byte[] bytes = new byte[1024];

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
            ipAddress = host.AddressList[0];
            remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.    
            sender = new Socket(ipAddress.AddressFamily,
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


    private string sendJSON(JObject JsonSendObject)     //Returns received string
    {
        try
        {
            Console.WriteLine("JSONOBJECT:\n {0}", JsonSendObject);
            //string msgJSON = JsonConvert.SerializeObject(JsonSendObject.ToString());
            byte[] msg = Encoding.ASCII.GetBytes(JsonSendObject.ToString());

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
        return Encoding.ASCII.GetString(bytes, 0, bytesRec);

    }

    public User getUser(string username, string password)   //Returns the User object for the given username ands password (if the parameters are wrong the server dies rip so don't do that)
    {
        JObject jobc = new JObject();
        jobc.Add("type", 1);    // 1 - User login
        jobc.Add("clientID", clientID);
        jobc.Add("username", username);
        jobc.Add("password", password);
        string recievedMsg = sendJSON(jobc);
        Console.WriteLine("recievedMsg: {0}", recievedMsg);
        try
        {
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
                    receivedJSonObject["line2"].ToString()
                    );
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            
        }
        return new User();

    }


    private JObject sendFirstConnectionInfo()
    {
        JObject jobc = new JObject();
        jobc.Add("type", 0);    // 0 - Basic Server Info aka "Hello"
        jobc.Add("msgID", msgID);       //#of messages sent     //WILL BE DELETED
        msgID++;
        return jobc;
    }
}
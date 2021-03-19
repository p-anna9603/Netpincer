using System;
using System.Text;
using SocketServer;
using Newtonsoft.Json.Linq;


// Socket Listener acts as a server and listens to the incoming   
// messages on the specified port and protocol.  
public class MainClass
{
    public static int Main(String[] args)
    {
       /* JObject jasonObject = new JObject();
        jasonObject.Add("type","0");
        jasonObject.Add("msgID", "1");
        jasonObject.Add("time", "2");
        if (jasonObject["type"].ToString()=="0")
            Console.WriteLine(jasonObject["msgID"].ToString());*/
        SocketListener server = new SocketListener();
        return 0;
    }

}
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
        SocketListener server = new SocketListener();
        return 0;
    }

}
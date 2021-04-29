package delivery;

import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.json.JsonObject;

/**
 *
 * @author Peti
 */
public class GreetClient {
    private Socket clientSocket;
    private PrintWriter out;
    private BufferedReader in;

    public void startConnection(String ip, int port){
        try {
            clientSocket = new Socket(ip, port);
        } catch (IOException ex) {
            Logger.getLogger(GreetClient.class.getName()).log(Level.SEVERE, null, ex);
        }
        try {
            out = new PrintWriter(clientSocket.getOutputStream(), true);
        } catch (IOException ex) {
            Logger.getLogger(GreetClient.class.getName()).log(Level.SEVERE, null, ex);
        }
        try {
            in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
        } catch (IOException ex) {
            Logger.getLogger(GreetClient.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public String sendMessage(JsonObject msg) 
    {
    	System.out.println(msg);
    	out.println(msg.toString());
        System.out.println("0");
        String resp = null;
        String message = "";
     	String line="";
     	StringBuilder responseString = new StringBuilder();
        BufferedReader bufferedReader = null;
        try {
        	/*System.out.println("01");
            resp = in.readLine();
            
            System.out.println("1");*/
        	
        	//InputStream is = clientSocket.getInputStream();

   
           /* byte[] buffer = new byte[1024];
            int read = -1;
            
            while(read != 0) 
			{
				read = is.read(buffer);
                String output = new String(buffer, 0, read);
                System.out.print(output);
                valasz+=output;
                System.out.flush();
            };
            System.out.println(valasz);*/
        	
        	//BufferedReader in = new BufferedReader( new InputStreamReader( clientSocket.getInputStream() ) );


            // Read data from the server until we finish reading the document
           // line = i.readLine();
        	/*bufferedReader = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            String str;
            str = bufferedReader.readLine();
            responseString.append(str);
            /*while ((str = bufferedReader.readLine()) != null) {
                responseString.append(str);
            }*/
                
        	int a = in.read();
        	char aChar= (char)a;
        	System.out.println("a:" + aChar);
            //message = responseString.toString();
            if (aChar=='5')
            {
            	System.out.println("sendMessage OK");
            }
            message += aChar;
            /*message+=line;
            while( line != "EXIT" )
            {
            	System.out.println("Anyád 1");
                System.out.println( line );
                System.out.println("Anyád 2");
                line = in.readLine();
                System.out.println("Anyád 3");
                message += line;
                System.out.println("Anyád 4");
            }

            // Close our streams
            System.out.println("in close 0" );
            in.close();
            System.out.println("in close 1" );
            
            System.out.println( message );*/
        	
        	
        } catch (IOException ex) {
        	 System.out.println("catch in close" );
            Logger.getLogger(GreetClient.class.getName()).log(Level.SEVERE, null, ex);
        }
        return message;
    }
    
    public String sendMessage2(String msg) 
    {
    	System.out.println(msg);
    	out.println(msg.toString());
        System.out.println("0");
        String message = "";
        try {
        	    
        	int a = in.read();
        	char aChar= (char)a;
        	System.out.println("a:" + aChar);
            if (aChar=='5')
            {
            	System.out.println("OK");
            }
            message += aChar;
        	
        	
        } catch (IOException ex) {
        	 System.out.println("catch in close" );
            Logger.getLogger(GreetClient.class.getName()).log(Level.SEVERE, null, ex);
        }
        return message;
    }

    public void stopConnection() {
        try {
            in.close();
        } catch (IOException ex) {
            Logger.getLogger(GreetClient.class.getName()).log(Level.SEVERE, null, ex);
        }
        out.close();
        try {
            clientSocket.close();
        } catch (IOException ex) {
            Logger.getLogger(GreetClient.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
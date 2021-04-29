package delivery;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.json.Json;
import javax.json.JsonObject;

import com.fasterxml.jackson.databind.ObjectMapper;


/**
 *
 * @author Peti
 */
public class delivery_connection {

    /**
     * @param args the command line arguments
     */
    
    public static void main(String[] args) {
    	System.out.println("itt");
        GreetClient client = new GreetClient();     
        System.out.println("itt1");   
            client.startConnection("192.168.1.12", 11000);
            System.out.println("itt2");
        JsonObject value = Json.createObjectBuilder().add("type", 0).build();  
        System.out.println("itt3");
        String response = client.sendMessage(value);
        
    //    ObjectMapper objectMapper = new ObjectMapper(); 
   
        System.out.println("itt");
        //    givenGreetingClient_whenServerRespondsWhenStarted_thenCorrect();
        try {
			System.in.read();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        System.out.println("vége");
    }
}

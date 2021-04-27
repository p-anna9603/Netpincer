package delivery;

import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.JButton;
import javax.swing.JPasswordField;
import java.awt.Font;
import javax.swing.JLabel;
import javax.swing.SwingConstants;
import javax.json.Json;
import javax.json.JsonObject;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.Color;

import org.codehaus.jackson.map.ObjectMapper;

public class login_window {

	JFrame frame;
	private JTextField txtFnev;
	private JPasswordField passw;
	private String login;
	public GreetClient client;
	private String fnev;
	private String jelszo;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		/*
		//connect to server
        GreetClient client = new GreetClient();       
            client.startConnection("localhost", 11000);
        JsonObject value = Json.createObjectBuilder().add("type", 0).build();  
        String response = client.sendMessage(value);
       */
       
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					login_window window = new login_window();
					window.frame.setVisible(true);
					window.frame.setLocationRelativeTo(null);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the application.
	 */
	public login_window() {
		
		//connect to server
        client = new GreetClient();       
            client.startConnection("localhost", 11000);
        JsonObject value = Json.createObjectBuilder().add("type", 42).build();  
        String response = client.sendMessage(value);
        System.out.println("AAAAAAAAA" +response);
        
		initialize();
	}

	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		frame = new JFrame();
		frame.getContentPane().setBackground(Color.ORANGE);
		frame.setBounds(100, 100, 450, 300);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.getContentPane().setLayout(null);
		
		txtFnev = new JTextField();
		txtFnev.setBackground(new Color(255, 153, 51));
		txtFnev.setBounds(62, 83, 133, 20);
		frame.getContentPane().add(txtFnev);
		txtFnev.setColumns(10);
		
		JButton btnNewButton = new JButton("Bejelentkez\u00E9s");
		btnNewButton.setBackground(new Color(205, 133, 63));
		btnNewButton.setFont(new Font("Tahoma", Font.PLAIN, 15));
		btnNewButton.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				fnev = txtFnev.getText().toString();
				jelszo = String.valueOf(passw.getPassword());
				System.out.println(fnev+" "+jelszo);
				JsonObject value = Json.createObjectBuilder().add("type", 3).add("username",fnev).add("password",jelszo).add("userType",2)
						.build();  
			    String valasz = client.sendMessage(value);
			    System.out.println("login response: " +valasz);
			    char response = valasz.charAt(0);
			    if (response == '1')
			    {
			    	//felugro ablak not ok
			    	popup loginerror = new popup("Sikertelen bejelentkezés!");
			    	loginerror.setVisible(true);
			    	loginerror.setLocationRelativeTo(null);
			    	System.out.println("login not OK");
			    }
			    else  if (response == '5')
			    {
			    	//felugro ablak  ok
			    	System.out.println("login OK");
			    	work workWindow=new work(fnev);
			    	workWindow.setClient(client);
			    	workWindow.setVisible(true);
			    	workWindow.setLocationRelativeTo(null);
			    	workWindow.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
			    	
			    }
			}
				
			}
		);
		btnNewButton.setBounds(62, 175, 133, 38);
		frame.getContentPane().add(btnNewButton);
		
		JButton btnNewButton_1 = new JButton("Regisztr\u00E1ci\u00F3");
		btnNewButton_1.setBackground(new Color(205, 133, 63));
		btnNewButton_1.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				registration register=new registration();
				register.setClient(client);
				register.setVisible(true);
				register.setLocationRelativeTo(null);
				register.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
				//frame.setVisible(false);
			}
		});
		btnNewButton_1.setFont(new Font("Tahoma", Font.PLAIN, 15));
		btnNewButton_1.setBounds(263, 152, 133, 38);
		frame.getContentPane().add(btnNewButton_1);
		
		passw = new JPasswordField();
		passw.setBackground(new Color(255, 153, 51));
		passw.setBounds(62, 139, 133, 20);
		frame.getContentPane().add(passw);
		
		JLabel lblNewLabel = new JLabel("Jelsz\u00F3");
		lblNewLabel.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel.setBounds(62, 114, 46, 14);
		frame.getContentPane().add(lblNewLabel);
		
		JLabel lblNewLabel_1 = new JLabel("Felhaszn\u00E1l\u00F3n\u00E9v");
		lblNewLabel_1.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_1.setBounds(62, 58, 133, 14);
		frame.getContentPane().add(lblNewLabel_1);
		
		JLabel lblNewLabel_2 = new JLabel("M\u00E9g nem regisztr\u00E1lt?");
		lblNewLabel_2.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_2.setBounds(254, 73, 154, 36);
		frame.getContentPane().add(lblNewLabel_2);
		
		JLabel lblNewLabel_3 = new JLabel("Itt megteheti:");
		lblNewLabel_3.setHorizontalAlignment(SwingConstants.CENTER);
		lblNewLabel_3.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_3.setBounds(275, 113, 104, 18);
		frame.getContentPane().add(lblNewLabel_3);
		
		JPanel panel = new JPanel();
		panel.setBackground(Color.YELLOW);
		panel.setBounds(242, 47, 170, 170);
		frame.getContentPane().add(panel);
	}
}

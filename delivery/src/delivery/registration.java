package delivery;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.EventQueue;
import java.awt.Font;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.IOException;
import java.io.StringReader;

import javax.json.Json;
import javax.json.JsonObject;
import javax.json.JsonReader;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JTextField;
import javax.swing.border.EmptyBorder;

import org.codehaus.jackson.JsonGenerationException;
import org.codehaus.jackson.map.JsonMappingException;
import org.codehaus.jackson.map.ObjectMapper;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import javax.swing.JLabel;

public class registration extends JFrame{

	private JPanel contentPane;
	private JFrame frame;
	private JTextField vnev;
	private JTextField knev;
	private JTextField tszam;
	private JTextField irszam;
	private JTextField varos;
	private JTextField utca;
	private JTextField hazszam;
	private JTextField felhnev;
	private JTextField email;
	private JPasswordField jelszo;
	private JPasswordField jelszo_conf;
	private JButton vissza;
	GreetClient client;

	/**
	 * Launch the application.
	 */
	/*public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				
				try {
					registration frame = new registration(client);
					frame.frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
				
			}
		});
	}
	*/

	/**
	 * Create the frame.
	 */
	
	public void setClient(GreetClient client2) {
		client = client2;
	}
	
	public registration() {
		/*
		frame = new JFrame();
		frame.getContentPane().setBackground(Color.ORANGE);
		frame.setBounds(100, 100, 450, 300);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.getContentPane().setLayout(null);
		*/
		
		
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 500, 700);
		contentPane = new JPanel();
		contentPane.setBackground(Color.ORANGE);
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		vnev = new JTextField();
		vnev.setBackground(Color.YELLOW);
		vnev.setBounds(60, 107, 168, 20);
		contentPane.add(vnev);
		vnev.setColumns(10);
		
		knev = new JTextField();
		knev.setBackground(Color.YELLOW);
		knev.setBounds(60, 163, 168, 20);
		contentPane.add(knev);
		knev.setColumns(10);
		
		tszam = new JTextField();
		tszam.setBackground(Color.YELLOW);
		tszam.setFont(new Font("Tahoma", Font.PLAIN, 14));
		tszam.setText("36-");
		tszam.setBounds(60, 219, 168, 20);
		contentPane.add(tszam);
		tszam.setColumns(10);
		
		irszam = new JTextField();
		irszam.setBackground(Color.YELLOW);
		irszam.setFont(new Font("Tahoma", Font.PLAIN, 14));
		irszam.setBounds(60, 275, 51, 20);
		contentPane.add(irszam);
		irszam.setColumns(10);
		
		varos = new JTextField();
		varos.setBackground(Color.YELLOW);
		varos.setBounds(60, 331, 134, 20);
		contentPane.add(varos);
		varos.setColumns(10);
		
		utca = new JTextField();
		utca.setBackground(Color.YELLOW);
		utca.setBounds(60, 387, 140, 20);
		contentPane.add(utca);
		utca.setColumns(10);
		
		hazszam = new JTextField();
		hazszam.setBackground(Color.YELLOW);
		hazszam.setBounds(210, 387, 45, 20);
		contentPane.add(hazszam);
		hazszam.setColumns(10);
		
		felhnev = new JTextField();
		felhnev.setBackground(Color.YELLOW);
		felhnev.setBounds(289, 107, 125, 20);
		contentPane.add(felhnev);
		felhnev.setColumns(10);
		
		email = new JTextField();
		email.setBackground(Color.YELLOW);
		email.setBounds(289, 163, 125, 20);
		contentPane.add(email);
		email.setColumns(10);
		
		jelszo = new JPasswordField();
		jelszo.setBackground(Color.YELLOW);
		jelszo.setBounds(289, 219, 125, 20);
		contentPane.add(jelszo);
		
		jelszo_conf = new JPasswordField();
		jelszo_conf.setBackground(Color.YELLOW);
		jelszo_conf.setBounds(289, 277, 125, 20);
		contentPane.add(jelszo_conf);
		
		JLabel lblNewLabel = new JLabel("Vezet\u00E9kn\u00E9v");
		lblNewLabel.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel.setBounds(60, 82, 97, 20);
		contentPane.add(lblNewLabel);
		
		JLabel lblNewLabel_1 = new JLabel("Keresztn\u00E9v");
		lblNewLabel_1.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_1.setBounds(60, 138, 86, 14);
		contentPane.add(lblNewLabel_1);
		
		JLabel lblNewLabel_2 = new JLabel("Telefonsz\u00E1m");
		lblNewLabel_2.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_2.setBounds(60, 194, 97, 14);
		contentPane.add(lblNewLabel_2);
		
		JLabel lblNewLabel_3 = new JLabel("Ir\u00E1ny\u00EDt\u00F3sz\u00E1m");
		lblNewLabel_3.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_3.setBounds(60, 250, 97, 14);
		contentPane.add(lblNewLabel_3);
		
		JLabel lblNewLabel_4 = new JLabel("V\u00E1ros");
		lblNewLabel_4.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_4.setBounds(60, 306, 46, 14);
		contentPane.add(lblNewLabel_4);
		
		JLabel lblNewLabel_5 = new JLabel("Lakc\u00EDm (Utca/H\u00E1zsz\u00E1m)");
		lblNewLabel_5.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_5.setBounds(60, 362, 168, 14);
		contentPane.add(lblNewLabel_5);
		
		JLabel lblNewLabel_6 = new JLabel("Felhaszn\u00E1l\u00F3n\u00E9v");
		lblNewLabel_6.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_6.setBounds(289, 82, 106, 14);
		contentPane.add(lblNewLabel_6);
		
		JLabel lblNewLabel_7 = new JLabel("E-mail c\u00EDm");
		lblNewLabel_7.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_7.setBounds(289, 138, 75, 14);
		contentPane.add(lblNewLabel_7);
		
		JLabel lblNewLabel_8 = new JLabel("Jelsz\u00F3");
		lblNewLabel_8.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_8.setBounds(289, 194, 46, 14);
		contentPane.add(lblNewLabel_8);
		
		JLabel lblNewLabel_9 = new JLabel("Jelsz\u00F3 meger\u0151s\u00EDt\u00E9se");
		lblNewLabel_9.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_9.setBounds(289, 250, 140, 20);
		contentPane.add(lblNewLabel_9);
		
		JButton regisztracio = new JButton("Regiszt\u00E1ci\u00F3");
		regisztracio.setBackground(new Color(205, 133, 63));
		regisztracio.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				//if(String.valueOf(jelszo.getPassword()) == String.valueOf(jelszo_conf.getPassword())) {
					User regUser = new User(felhnev.getText().toString(), String.valueOf(jelszo.getPassword()), knev.getText().toString(), vnev.getText().toString(), tszam.getText().toString(),
											varos.getText().toString(), irszam.getText().toString(), utca.getText().toString()+" "+hazszam.getText().toString(), "", 2, email.getText().toString(), "41");
					
					ObjectMapper omap = new ObjectMapper();
					try {
						String user = omap.writeValueAsString(regUser);
						System.out.println(user);
						String valasz = client.sendMessage2(user);
						System.out.println(valasz);
						/*JsonReader jsonReader = Json.createReader(new StringReader(valasz));
						
						JsonObject object = jsonReader.readObject();
						//jsonReader.close();

							if(object.get("type").toString() == "4") {
								//okés ablak
								System.out.println("megvan fonok");
							}*/
						System.out.println("login response: " +valasz);
					    char response = valasz.charAt(0);
						if (response=='5')
						{
							System.out.println(" REG OK, felugró ablak");
							popup regOK = new popup("Sikeres regisztráció!");
							regOK.setVisible(true);
							regOK.setLocationRelativeTo(null);
						}
						
					} catch (JsonGenerationException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					} catch (JsonMappingException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					} catch (IOException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}
			}
		});
		regisztracio.setFont(new Font("Tahoma", Font.PLAIN, 16));
		regisztracio.setBounds(175, 460, 150, 45);
		contentPane.add(regisztracio);
		
		vissza = new JButton("Vissza");
		vissza.setBackground(new Color(205, 133, 63));
		vissza.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				setVisible(false);
			}
		});
		vissza.setFont(new Font("Tahoma", Font.PLAIN, 16));
		vissza.setBounds(175, 537, 150, 45);
		contentPane.add(vissza);
		
		JPanel panel = new JPanel();
		panel.setBackground(new Color(255, 153, 51));
		panel.setBounds(40, 45, 400, 394);
		contentPane.add(panel);
	}
}

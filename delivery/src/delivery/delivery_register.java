package delivery;

import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JTextField;
import javax.swing.JPasswordField;
import javax.swing.JLabel;
import java.awt.Font;
import javax.swing.JButton;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class delivery_register {

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

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					delivery_register window = new delivery_register();
					window.frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the application.
	 */
	public delivery_register() {
		initialize();
	}

	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		frame = new JFrame();
		frame.setBounds(100, 100, 500, 700);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.getContentPane().setLayout(null);
		
		vnev = new JTextField();
		vnev.setBounds(60, 107, 168, 20);
		frame.getContentPane().add(vnev);
		vnev.setColumns(10);
		
		knev = new JTextField();
		knev.setBounds(60, 163, 168, 20);
		frame.getContentPane().add(knev);
		knev.setColumns(10);
		
		tszam = new JTextField();
		tszam.setFont(new Font("Tahoma", Font.PLAIN, 14));
		tszam.setText("36-");
		tszam.setBounds(60, 219, 168, 20);
		frame.getContentPane().add(tszam);
		tszam.setColumns(10);
		
		irszam = new JTextField();
		irszam.setFont(new Font("Tahoma", Font.PLAIN, 14));
		irszam.setBounds(60, 275, 51, 20);
		frame.getContentPane().add(irszam);
		irszam.setColumns(10);
		
		varos = new JTextField();
		varos.setBounds(60, 331, 134, 20);
		frame.getContentPane().add(varos);
		varos.setColumns(10);
		
		utca = new JTextField();
		utca.setBounds(60, 387, 140, 20);
		frame.getContentPane().add(utca);
		utca.setColumns(10);
		
		hazszam = new JTextField();
		hazszam.setBounds(210, 387, 45, 20);
		frame.getContentPane().add(hazszam);
		hazszam.setColumns(10);
		
		felhnev = new JTextField();
		felhnev.setBounds(289, 107, 125, 20);
		frame.getContentPane().add(felhnev);
		felhnev.setColumns(10);
		
		email = new JTextField();
		email.setBounds(289, 163, 125, 20);
		frame.getContentPane().add(email);
		email.setColumns(10);
		
		jelszo = new JPasswordField();
		jelszo.setBounds(289, 219, 125, 20);
		frame.getContentPane().add(jelszo);
		
		jelszo_conf = new JPasswordField();
		jelszo_conf.setBounds(289, 277, 125, 20);
		frame.getContentPane().add(jelszo_conf);
		
		JLabel lblNewLabel = new JLabel("Vezet\u00E9kn\u00E9v");
		lblNewLabel.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel.setBounds(60, 82, 97, 20);
		frame.getContentPane().add(lblNewLabel);
		
		JLabel lblNewLabel_1 = new JLabel("Keresztn\u00E9v");
		lblNewLabel_1.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_1.setBounds(60, 138, 86, 14);
		frame.getContentPane().add(lblNewLabel_1);
		
		JLabel lblNewLabel_2 = new JLabel("Telefonsz\u00E1m");
		lblNewLabel_2.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_2.setBounds(60, 194, 97, 14);
		frame.getContentPane().add(lblNewLabel_2);
		
		JLabel lblNewLabel_3 = new JLabel("Ir\u00E1ny\u00EDt\u00F3sz\u00E1m");
		lblNewLabel_3.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_3.setBounds(60, 250, 97, 14);
		frame.getContentPane().add(lblNewLabel_3);
		
		JLabel lblNewLabel_4 = new JLabel("V\u00E1ros");
		lblNewLabel_4.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_4.setBounds(60, 306, 46, 14);
		frame.getContentPane().add(lblNewLabel_4);
		
		JLabel lblNewLabel_5 = new JLabel("Lakc\u00EDm (Utca/H\u00E1zsz\u00E1m)");
		lblNewLabel_5.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_5.setBounds(60, 362, 168, 14);
		frame.getContentPane().add(lblNewLabel_5);
		
		JLabel lblNewLabel_6 = new JLabel("Felhaszn\u00E1l\u00F3n\u00E9v");
		lblNewLabel_6.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_6.setBounds(289, 82, 106, 14);
		frame.getContentPane().add(lblNewLabel_6);
		
		JLabel lblNewLabel_7 = new JLabel("E-mail c\u00EDm");
		lblNewLabel_7.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_7.setBounds(289, 138, 75, 14);
		frame.getContentPane().add(lblNewLabel_7);
		
		JLabel lblNewLabel_8 = new JLabel("Jelsz\u00F3");
		lblNewLabel_8.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_8.setBounds(289, 194, 46, 14);
		frame.getContentPane().add(lblNewLabel_8);
		
		JLabel lblNewLabel_9 = new JLabel("Jelsz\u00F3 meger\u0151s\u00EDt\u00E9se");
		lblNewLabel_9.setFont(new Font("Tahoma", Font.PLAIN, 16));
		lblNewLabel_9.setBounds(289, 250, 140, 20);
		frame.getContentPane().add(lblNewLabel_9);
		
		JButton regisztracio = new JButton("Regiszt\u00E1ci\u00F3");
		regisztracio.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
			}
		});
		regisztracio.setFont(new Font("Tahoma", Font.PLAIN, 16));
		regisztracio.setBounds(175, 460, 150, 45);
		frame.getContentPane().add(regisztracio);
		
		vissza = new JButton("Vissza");
		vissza.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
			}
		});
		vissza.setFont(new Font("Tahoma", Font.PLAIN, 16));
		vissza.setBounds(175, 537, 150, 45);
		frame.getContentPane().add(vissza);
	}
}

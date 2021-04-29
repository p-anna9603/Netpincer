package delivery;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import org.codehaus.jackson.JsonGenerationException;
import org.codehaus.jackson.map.JsonMappingException;
import org.codehaus.jackson.map.ObjectMapper;

import javax.swing.JCheckBox;
import javax.swing.JTextField;
import javax.swing.JButton;
import java.awt.Font;
import javax.swing.SwingConstants;
import javax.swing.JLabel;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.IOException;
import java.awt.Color;

public class work extends JFrame {

	private JPanel contentPane;
	private JTextField fromH;
	private JTextField fromM;
	private JTextField toH;
	private JTextField toM;
	public GreetClient client;
	String username;

	/**
	 * Create the frame.
	 */
	
	public void setClient(GreetClient client2) {
		client = client2;
	}
	
	public work(String username_) {
		username=username_;
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 450, 475);
		contentPane = new JPanel();
		contentPane.setBackground(Color.ORANGE);
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		JCheckBox hetfo = new JCheckBox("H\u00E9tf\u0151");
		hetfo.setBackground(Color.ORANGE);
		hetfo.setFont(new Font("Tahoma", Font.PLAIN, 15));
		hetfo.setBounds(95, 68, 100, 23);
		contentPane.add(hetfo);
		
		JCheckBox kedd = new JCheckBox("Kedd");
		kedd.setBackground(Color.ORANGE);
		kedd.setFont(new Font("Tahoma", Font.PLAIN, 15));
		kedd.setBounds(95, 105, 59, 23);
		contentPane.add(kedd);
		
		JCheckBox szerda = new JCheckBox("Szerda");
		szerda.setBackground(Color.ORANGE);
		szerda.setFont(new Font("Tahoma", Font.PLAIN, 15));
		szerda.setBounds(95, 142, 70, 23);
		contentPane.add(szerda);
		
		JCheckBox csutortok = new JCheckBox("Cs\u00FCt\u00F6rt\u00F6k");
		csutortok.setBackground(Color.ORANGE);
		csutortok.setFont(new Font("Tahoma", Font.PLAIN, 15));
		csutortok.setBounds(275, 68, 97, 23);
		contentPane.add(csutortok);
		
		JCheckBox pentek = new JCheckBox("P\u00E9ntek");
		pentek.setBackground(Color.ORANGE);
		pentek.setFont(new Font("Tahoma", Font.PLAIN, 15));
		pentek.setBounds(275, 105, 97, 23);
		contentPane.add(pentek);
		
		JCheckBox szombat = new JCheckBox("Szombat");
		szombat.setBackground(Color.ORANGE);
		szombat.setFont(new Font("Tahoma", Font.PLAIN, 15));
		szombat.setBounds(275, 142, 97, 23);
		contentPane.add(szombat);
		
		JCheckBox vasarnap = new JCheckBox("Vas\u00E1rnap");
		vasarnap.setBackground(Color.ORANGE);
		vasarnap.setFont(new Font("Tahoma", Font.PLAIN, 15));
		vasarnap.setBounds(175, 180, 100, 23);
		contentPane.add(vasarnap);
		
		fromH = new JTextField();
		fromH.setBackground(Color.YELLOW);
		fromH.setHorizontalAlignment(SwingConstants.CENTER);
		fromH.setFont(new Font("Tahoma", Font.PLAIN, 16));
		fromH.setBounds(52, 258, 39, 20);
		contentPane.add(fromH);
		fromH.setColumns(10);
		
		fromM = new JTextField();
		fromM.setBackground(Color.YELLOW);
		fromM.setHorizontalAlignment(SwingConstants.CENTER);
		fromM.setFont(new Font("Tahoma", Font.PLAIN, 16));
		fromM.setBounds(127, 258, 39, 20);
		contentPane.add(fromM);
		fromM.setColumns(10);
		
		toH = new JTextField();
		toH.setBackground(Color.YELLOW);
		toH.setHorizontalAlignment(SwingConstants.CENTER);
		toH.setFont(new Font("Tahoma", Font.PLAIN, 16));
		toH.setBounds(246, 260, 39, 20);
		contentPane.add(toH);
		toH.setColumns(10);
		
		toM = new JTextField();
		toM.setBackground(Color.YELLOW);
		toM.setHorizontalAlignment(SwingConstants.CENTER);
		toM.setFont(new Font("Tahoma", Font.PLAIN, 16));
		toM.setBounds(322, 260, 39, 20);
		contentPane.add(toM);
		toM.setColumns(10);
		
		JButton btnNewButton = new JButton("Munkarend\r\n be\u00E1ll\u00EDt\u00E1sa");
		btnNewButton.setBackground(new Color(205, 133, 63));
		btnNewButton.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				String napok = "";
				if(hetfo.isSelected()) {
					napok += "1, ";
				}
				if(kedd.isSelected()) {
					napok += "2, ";
				}
				if(szerda.isSelected()) {
					napok += "3, ";
				}
				if(csutortok.isSelected()) {
					napok += "4, ";
				}
				if(pentek.isSelected()) {
					napok += "5, ";
				}
				if(szombat.isSelected()) {
					napok += "6, ";
				}
				if(vasarnap.isSelected()) {
					napok += "7";
				}
				
				
				WorkingHours whours = new WorkingHours(fromH.getText(), fromM.getText(), toH.getText(), toM.getText(), napok.trim(),"6",username);
				
				ObjectMapper omap = new ObjectMapper();
				try {
					String hours;
					
					hours = omap.writeValueAsString(whours);
					 
					System.out.println(hours);
					String valasz = client.sendMessage2(hours);
					char response = valasz.charAt(0);
					if (response == '1')
				    {
				    	//felugro ablak not ok
						popup workerror = new popup("Beállítás sikertelen!");
				    	workerror.setVisible(true);
				    	workerror.setLocationRelativeTo(null);
						System.out.println("work not OK");
				    }
				    else  if (response == '5')
				    {
				    	//felugro ablak  ok
				    	popup workOK = new popup("Sikeresen beállítva!");
				    	workOK.setVisible(true);
				    	workOK.setLocationRelativeTo(null);
				    	System.out.println("work ok");
				    	
				    	
				    }
					
					} catch (JsonGenerationException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					} catch (JsonMappingException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}catch (IOException e1) {
							// TODO Auto-generated catch block
							e1.printStackTrace();
						}
			
				}});
		btnNewButton.setFont(new Font("Tahoma", Font.PLAIN, 16));
		btnNewButton.setBounds(125, 300, 200, 44);
		contentPane.add(btnNewButton);
		
		JLabel lblNewLabel = new JLabel("\u00F3ra");
		lblNewLabel.setFont(new Font("Tahoma", Font.PLAIN, 15));
		lblNewLabel.setBounds(97, 261, 46, 14);
		contentPane.add(lblNewLabel);
		
		JLabel lblNewLabel_1 = new JLabel("perc");
		lblNewLabel_1.setFont(new Font("Tahoma", Font.PLAIN, 15));
		lblNewLabel_1.setBounds(174, 257, 46, 23);
		contentPane.add(lblNewLabel_1);
		
		JLabel lblNewLabel_2 = new JLabel("\u00F3ra");
		lblNewLabel_2.setFont(new Font("Tahoma", Font.PLAIN, 15));
		lblNewLabel_2.setBounds(294, 265, 46, 14);
		contentPane.add(lblNewLabel_2);
		
		JLabel lblNewLabel_1_1 = new JLabel("perc");
		lblNewLabel_1_1.setFont(new Font("Tahoma", Font.PLAIN, 15));
		lblNewLabel_1_1.setBounds(367, 260, 46, 23);
		contentPane.add(lblNewLabel_1_1);
		
		JLabel lblNewLabel_3 = new JLabel("Mett\u0151l?");
		lblNewLabel_3.setFont(new Font("Tahoma", Font.PLAIN, 17));
		lblNewLabel_3.setBounds(97, 223, 64, 14);
		contentPane.add(lblNewLabel_3);
		
		JLabel lblNewLabel_4 = new JLabel("Meddig?");
		lblNewLabel_4.setFont(new Font("Tahoma", Font.PLAIN, 17));
		lblNewLabel_4.setBounds(294, 223, 67, 20);
		contentPane.add(lblNewLabel_4);
		
		JLabel lblNewLabel_5 = new JLabel("Melyik napokon?");
		lblNewLabel_5.setHorizontalAlignment(SwingConstants.CENTER);
		lblNewLabel_5.setFont(new Font("Tahoma", Font.PLAIN, 17));
		lblNewLabel_5.setBounds(164, 32, 121, 23);
		contentPane.add(lblNewLabel_5);
		
		JButton btnNewButton_1 = new JButton("Vissza");
		btnNewButton_1.setBackground(new Color(205, 133, 63));
		btnNewButton_1.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				setVisible(false);
			}
		});
		btnNewButton_1.setFont(new Font("Tahoma", Font.PLAIN, 17));
		btnNewButton_1.setBounds(136, 366, 180, 44);
		contentPane.add(btnNewButton_1);
	}
}


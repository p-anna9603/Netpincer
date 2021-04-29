package delivery;

import java.awt.BorderLayout;
import java.awt.FlowLayout;

import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JLabel;
import java.awt.Font;
import javax.swing.SwingConstants;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.Color;

public class popup extends JDialog {

	private final JPanel contentPanel = new JPanel();

	/**
	 * Launch the application.
	 */
/*
	public static void main(String[] args) {
		try {
			popup dialog = new popup();
			dialog.setDefaultCloseOperation(JDialog.DISPOSE_ON_CLOSE);
			dialog.setVisible(true);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
*/
	/**
	 * Create the dialog.
	 */
	public popup(String message) {
		setBounds(100, 100, 450, 300);
		getContentPane().setLayout(new BorderLayout());
		contentPanel.setBackground(Color.ORANGE);
		contentPanel.setBorder(new EmptyBorder(5, 5, 5, 5));
		getContentPane().add(contentPanel, BorderLayout.CENTER);
		contentPanel.setLayout(null);
		{
			JButton btnNewButton = new JButton("Rendben");
			btnNewButton.setBackground(new Color(205, 133, 63));
			btnNewButton.addMouseListener(new MouseAdapter() {
				@Override
				public void mouseClicked(MouseEvent e) {
					setVisible(false);
				}
			});
			btnNewButton.setFont(new Font("Tahoma", Font.PLAIN, 17));
			btnNewButton.setBounds(152, 179, 145, 52);
			contentPanel.add(btnNewButton);
		}
		{
			JLabel messageLabel = new JLabel(message);
			messageLabel.setHorizontalAlignment(SwingConstants.CENTER);
			messageLabel.setFont(new Font("Tahoma", Font.PLAIN, 20));
			messageLabel.setBounds(0, 56, 434, 86);
			contentPanel.add(messageLabel);
		}
	}

}

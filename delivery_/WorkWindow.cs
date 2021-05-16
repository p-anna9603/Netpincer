using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace delivery_
{
    public partial class WorkWindow : Form
    {
		string uname;
        public WorkWindow(string username)
        {
			uname = username;
            InitializeComponent();
        }

        private void SetBt_Click(object sender, EventArgs e)
        {
			string napok = "";
			if (hetfo.Checked)
			{
				napok += "1,";
			}
			if (kedd.Checked)
			{
				napok += "2,";
			}
			if (szerda.Checked)
			{
				napok += "3,";
			}
			if (csutortok.Checked)
			{
				napok += "4,";
			}
			if (pentek.Checked)
			{
				napok += "5,";
			}
			if (szombat.Checked)
			{
				napok += "6,";
			}
			if (vasarnap.Checked)
			{
				napok += "7";
			}
		//	if (napok.ElementAt(napok.Length) = ',')

			WorkingHours whours = new WorkingHours(fromH.Text, fromM.Text, ToH.Text, ToM.Text, napok, "6", uname);


		}

		private void BackBt_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

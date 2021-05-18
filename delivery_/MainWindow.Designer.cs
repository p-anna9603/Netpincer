
namespace delivery_
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.felBt = new System.Windows.Forms.Button();
            this.kiBt = new System.Windows.Forms.Button();
            this.noBt = new System.Windows.Forms.Button();
            this.munkaBt = new System.Windows.Forms.Button();
            this.rausBt = new System.Windows.Forms.Button();
            this.orderlista = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelVnevKnev = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // felBt
            // 
            this.felBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.felBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.felBt.Location = new System.Drawing.Point(141, 331);
            this.felBt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.felBt.Name = "felBt";
            this.felBt.Size = new System.Drawing.Size(196, 44);
            this.felBt.TabIndex = 0;
            this.felBt.Text = "Kiszállítás alatt";
            this.felBt.UseVisualStyleBackColor = false;
            this.felBt.Click += new System.EventHandler(this.felBt_Click);
            // 
            // kiBt
            // 
            this.kiBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kiBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kiBt.Location = new System.Drawing.Point(345, 331);
            this.kiBt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kiBt.Name = "kiBt";
            this.kiBt.Size = new System.Drawing.Size(196, 44);
            this.kiBt.TabIndex = 1;
            this.kiBt.Text = "Kiszállítva";
            this.kiBt.UseVisualStyleBackColor = false;
            this.kiBt.Click += new System.EventHandler(this.kiBt_Click);
            // 
            // noBt
            // 
            this.noBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.noBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.noBt.Location = new System.Drawing.Point(249, 398);
            this.noBt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.noBt.Name = "noBt";
            this.noBt.Size = new System.Drawing.Size(196, 46);
            this.noBt.TabIndex = 2;
            this.noBt.Text = "Visszautasítás";
            this.noBt.UseVisualStyleBackColor = false;
            this.noBt.Click += new System.EventHandler(this.noBt_Click);
            // 
            // munkaBt
            // 
            this.munkaBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.munkaBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.munkaBt.Location = new System.Drawing.Point(684, 197);
            this.munkaBt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.munkaBt.Name = "munkaBt";
            this.munkaBt.Size = new System.Drawing.Size(313, 64);
            this.munkaBt.TabIndex = 3;
            this.munkaBt.Text = "Munkaidő módosítása";
            this.munkaBt.UseVisualStyleBackColor = false;
            this.munkaBt.Click += new System.EventHandler(this.munkaBt_Click);
            // 
            // rausBt
            // 
            this.rausBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.rausBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rausBt.Location = new System.Drawing.Point(716, 289);
            this.rausBt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rausBt.Name = "rausBt";
            this.rausBt.Size = new System.Drawing.Size(255, 62);
            this.rausBt.TabIndex = 4;
            this.rausBt.Text = "Kilépés";
            this.rausBt.UseVisualStyleBackColor = false;
            this.rausBt.Click += new System.EventHandler(this.rausBt_Click);
            // 
            // orderlista
            // 
            this.orderlista.BackColor = System.Drawing.Color.Yellow;
            this.orderlista.FormattingEnabled = true;
            this.orderlista.ItemHeight = 16;
            this.orderlista.Location = new System.Drawing.Point(141, 122);
            this.orderlista.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.orderlista.Name = "orderlista";
            this.orderlista.Size = new System.Drawing.Size(399, 180);
            this.orderlista.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(822, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Belépve mint:";
            // 
            // labelVnevKnev
            // 
            this.labelVnevKnev.AutoSize = true;
            this.labelVnevKnev.Location = new System.Drawing.Point(921, 13);
            this.labelVnevKnev.Name = "labelVnevKnev";
            this.labelVnevKnev.Size = new System.Drawing.Size(76, 17);
            this.labelVnevKnev.TabIndex = 7;
            this.labelVnevKnev.Text = "vnev+knev";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.labelVnevKnev);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.orderlista);
            this.Controls.Add(this.rausBt);
            this.Controls.Add(this.munkaBt);
            this.Controls.Add(this.noBt);
            this.Controls.Add(this.kiBt);
            this.Controls.Add(this.felBt);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button felBt;
        private System.Windows.Forms.Button kiBt;
        private System.Windows.Forms.Button noBt;
        private System.Windows.Forms.Button munkaBt;
        private System.Windows.Forms.Button rausBt;
        private System.Windows.Forms.ListBox orderlista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVnevKnev;
    }
}
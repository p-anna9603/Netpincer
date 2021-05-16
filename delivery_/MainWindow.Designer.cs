
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
            this.SuspendLayout();
            // 
            // felBt
            // 
            this.felBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.felBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.felBt.Location = new System.Drawing.Point(106, 269);
            this.felBt.Name = "felBt";
            this.felBt.Size = new System.Drawing.Size(147, 36);
            this.felBt.TabIndex = 0;
            this.felBt.Text = "Kiszállítás alatt";
            this.felBt.UseVisualStyleBackColor = false;
            this.felBt.Click += new System.EventHandler(this.felBt_Click);
            // 
            // kiBt
            // 
            this.kiBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kiBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kiBt.Location = new System.Drawing.Point(259, 269);
            this.kiBt.Name = "kiBt";
            this.kiBt.Size = new System.Drawing.Size(147, 36);
            this.kiBt.TabIndex = 1;
            this.kiBt.Text = "Kiszállítva";
            this.kiBt.UseVisualStyleBackColor = false;
            this.kiBt.Click += new System.EventHandler(this.kiBt_Click);
            // 
            // noBt
            // 
            this.noBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.noBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.noBt.Location = new System.Drawing.Point(187, 323);
            this.noBt.Name = "noBt";
            this.noBt.Size = new System.Drawing.Size(147, 37);
            this.noBt.TabIndex = 2;
            this.noBt.Text = "Visszautasítás";
            this.noBt.UseVisualStyleBackColor = false;
            this.noBt.Click += new System.EventHandler(this.noBt_Click);
            // 
            // munkaBt
            // 
            this.munkaBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.munkaBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.munkaBt.Location = new System.Drawing.Point(513, 160);
            this.munkaBt.Name = "munkaBt";
            this.munkaBt.Size = new System.Drawing.Size(235, 52);
            this.munkaBt.TabIndex = 3;
            this.munkaBt.Text = "Munkaidő módosítása";
            this.munkaBt.UseVisualStyleBackColor = false;
            this.munkaBt.Click += new System.EventHandler(this.munkaBt_Click);
            // 
            // rausBt
            // 
            this.rausBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.rausBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rausBt.Location = new System.Drawing.Point(537, 235);
            this.rausBt.Name = "rausBt";
            this.rausBt.Size = new System.Drawing.Size(191, 50);
            this.rausBt.TabIndex = 4;
            this.rausBt.Text = "Kilépés";
            this.rausBt.UseVisualStyleBackColor = false;
            this.rausBt.Click += new System.EventHandler(this.rausBt_Click);
            // 
            // orderlista
            // 
            this.orderlista.BackColor = System.Drawing.Color.Yellow;
            this.orderlista.FormattingEnabled = true;
            this.orderlista.Location = new System.Drawing.Point(106, 99);
            this.orderlista.Name = "orderlista";
            this.orderlista.Size = new System.Drawing.Size(300, 147);
            this.orderlista.TabIndex = 5;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.orderlista);
            this.Controls.Add(this.rausBt);
            this.Controls.Add(this.munkaBt);
            this.Controls.Add(this.noBt);
            this.Controls.Add(this.kiBt);
            this.Controls.Add(this.felBt);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button felBt;
        private System.Windows.Forms.Button kiBt;
        private System.Windows.Forms.Button noBt;
        private System.Windows.Forms.Button munkaBt;
        private System.Windows.Forms.Button rausBt;
        private System.Windows.Forms.ListBox orderlista;
    }
}
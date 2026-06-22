namespace SIP_KMW
{
    partial class FormKoneksi
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
            this.btnCekKoneksi = new System.Windows.Forms.Button();
            this.btnLanjut = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCekKoneksi
            // 
            this.btnCekKoneksi.BackColor = System.Drawing.SystemColors.Info;
            this.btnCekKoneksi.Location = new System.Drawing.Point(314, 252);
            this.btnCekKoneksi.Name = "btnCekKoneksi";
            this.btnCekKoneksi.Size = new System.Drawing.Size(172, 23);
            this.btnCekKoneksi.TabIndex = 0;
            this.btnCekKoneksi.Text = "Cek Koneksi";
            this.btnCekKoneksi.UseVisualStyleBackColor = false;
            this.btnCekKoneksi.Click += new System.EventHandler(this.btnCekKoneksi_Click);
            // 
            // btnLanjut
            // 
            this.btnLanjut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnLanjut.Location = new System.Drawing.Point(314, 301);
            this.btnLanjut.Name = "btnLanjut";
            this.btnLanjut.Size = new System.Drawing.Size(172, 23);
            this.btnLanjut.TabIndex = 1;
            this.btnLanjut.Text = "Login";
            this.btnLanjut.UseVisualStyleBackColor = false;
            this.btnLanjut.Click += new System.EventHandler(this.btnLanjut_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(323, 215);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(154, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status: Menunggu Konfirmasi...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SIP_KMW.Properties.Resources.Gemini_Generated_Image_6ut1716ut1716ut1;
            this.pictureBox1.Location = new System.Drawing.Point(237, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(326, 191);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FormKoneksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnLanjut);
            this.Controls.Add(this.btnCekKoneksi);
            this.Name = "FormKoneksi";
            this.Text = "FormInputKematian";
            this.Load += new System.EventHandler(this.FormKoneksi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCekKoneksi;
        private System.Windows.Forms.Button btnLanjut;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
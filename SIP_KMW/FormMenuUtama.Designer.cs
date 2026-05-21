using System;
using System.Data.SqlClient;

namespace SIP_KMW
{
    partial class FormMenuUtama
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 

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
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblTotalData = new System.Windows.Forms.Label();
            this.btnBukaInput = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnManajemenUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblJudul
            // 
            this.lblJudul.AutoSize = true;
            this.lblJudul.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJudul.Location = new System.Drawing.Point(293, 53);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(215, 25);
            this.lblJudul.TabIndex = 0;
            this.lblJudul.Text = "Dashboard SIP-KMW";
            // 
            // lblTotalData
            // 
            this.lblTotalData.AutoSize = true;
            this.lblTotalData.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalData.Location = new System.Drawing.Point(208, 184);
            this.lblTotalData.Name = "lblTotalData";
            this.lblTotalData.Size = new System.Drawing.Size(385, 22);
            this.lblTotalData.TabIndex = 1;
            this.lblTotalData.Text = "Total Warga Terdata Meninggal : 0 orang";
            this.lblTotalData.Click += new System.EventHandler(this.lblTotalData_Click);
            // 
            // btnBukaInput
            // 
            this.btnBukaInput.Location = new System.Drawing.Point(279, 229);
            this.btnBukaInput.Name = "btnBukaInput";
            this.btnBukaInput.Size = new System.Drawing.Size(243, 23);
            this.btnBukaInput.TabIndex = 2;
            this.btnBukaInput.Text = "INPUT";
            this.btnBukaInput.UseVisualStyleBackColor = true;
            this.btnBukaInput.Click += new System.EventHandler(this.btnBukaInput_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLogout.Location = new System.Drawing.Point(658, 381);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "LOGOUT";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(374, 328);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(52, 13);
            this.lblWelcome.TabIndex = 4;
            this.lblWelcome.Text = "Welcome";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(348, 357);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(105, 13);
            this.lblRole.TabIndex = 5;
            this.lblRole.Text = "Akses : Administrator";
            // 
            // btnManajemenUser
            // 
            this.btnManajemenUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnManajemenUser.Location = new System.Drawing.Point(279, 272);
            this.btnManajemenUser.Name = "btnManajemenUser";
            this.btnManajemenUser.Size = new System.Drawing.Size(243, 23);
            this.btnManajemenUser.TabIndex = 6;
            this.btnManajemenUser.Text = "ManajemenUser";
            this.btnManajemenUser.UseVisualStyleBackColor = false;
            this.btnManajemenUser.Visible = false;
            this.btnManajemenUser.Click += new System.EventHandler(this.btnManajemenUser_Click);
            // 
            // FormMenuUtama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnManajemenUser);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnBukaInput);
            this.Controls.Add(this.lblTotalData);
            this.Controls.Add(this.lblJudul);
            this.Name = "FormMenuUtama";
            this.Text = "FormUtama";
            this.Load += new System.EventHandler(this.FormMenuUtama_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblTotalData;
        private System.Windows.Forms.Button btnBukaInput;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnManajemenUser;
    }
}
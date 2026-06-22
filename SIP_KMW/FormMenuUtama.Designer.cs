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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblTotalData = new System.Windows.Forms.Label();
            this.btnBukaInput = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.chartKematian = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartJK = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBukaLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartKematian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartJK)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(343, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "DASHBOARD SIP_KMW";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(412, 408);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(69, 13);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Halo, (Nama)";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(410, 434);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(73, 13);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Akses : (Role)";
            this.lblRole.Click += new System.EventHandler(this.lblRole_Click);
            // 
            // lblTotalData
            // 
            this.lblTotalData.AutoSize = true;
            this.lblTotalData.Location = new System.Drawing.Point(374, 382);
            this.lblTotalData.Name = "lblTotalData";
            this.lblTotalData.Size = new System.Drawing.Size(144, 13);
            this.lblTotalData.TabIndex = 3;
            this.lblTotalData.Text = "Total Data Warga Meninggal";
            this.lblTotalData.Click += new System.EventHandler(this.lblTotalData_Click);
            // 
            // btnBukaInput
            // 
            this.btnBukaInput.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBukaInput.Location = new System.Drawing.Point(329, 459);
            this.btnBukaInput.Name = "btnBukaInput";
            this.btnBukaInput.Size = new System.Drawing.Size(234, 23);
            this.btnBukaInput.TabIndex = 4;
            this.btnBukaInput.Text = "Input Data";
            this.btnBukaInput.UseVisualStyleBackColor = false;
            this.btnBukaInput.Click += new System.EventHandler(this.btnBukaInput_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(806, 537);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // chartKematian
            // 
            this.chartKematian.BackColor = System.Drawing.Color.Transparent;
            chartArea11.Name = "ChartArea1";
            this.chartKematian.ChartAreas.Add(chartArea11);
            legend11.Name = "Legend1";
            this.chartKematian.Legends.Add(legend11);
            this.chartKematian.Location = new System.Drawing.Point(114, 79);
            this.chartKematian.Name = "chartKematian";
            series11.ChartArea = "ChartArea1";
            series11.Legend = "Legend1";
            series11.Name = "Series1";
            this.chartKematian.Series.Add(series11);
            this.chartKematian.Size = new System.Drawing.Size(300, 300);
            this.chartKematian.TabIndex = 7;
            this.chartKematian.Text = "chartKematian";
            this.chartKematian.Click += new System.EventHandler(this.chartKematian_Click);
            // 
            // chartJK
            // 
            this.chartJK.BackColor = System.Drawing.Color.Transparent;
            chartArea12.Name = "ChartArea1";
            this.chartJK.ChartAreas.Add(chartArea12);
            legend12.Name = "Legend1";
            this.chartJK.Legends.Add(legend12);
            this.chartJK.Location = new System.Drawing.Point(485, 79);
            this.chartJK.Name = "chartJK";
            series12.ChartArea = "ChartArea1";
            series12.Legend = "Legend1";
            series12.Name = "Series1";
            this.chartJK.Series.Add(series12);
            this.chartJK.Size = new System.Drawing.Size(300, 300);
            this.chartJK.TabIndex = 8;
            this.chartJK.Text = "chartJK";
            this.chartJK.Click += new System.EventHandler(this.chartJK_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.Location = new System.Drawing.Point(329, 488);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(234, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Manajemen User";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnManajemenUser_Click);
            // 
            // btnBukaLog
            // 
            this.btnBukaLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBukaLog.Location = new System.Drawing.Point(329, 517);
            this.btnBukaLog.Name = "btnBukaLog";
            this.btnBukaLog.Size = new System.Drawing.Size(234, 23);
            this.btnBukaLog.TabIndex = 11;
            this.btnBukaLog.Text = "Sistem Log";
            this.btnBukaLog.UseVisualStyleBackColor = false;
            this.btnBukaLog.Click += new System.EventHandler(this.btnBukaLog_Click_1);
            // 
            // FormMenuUtama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(893, 572);
            this.Controls.Add(this.btnBukaLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chartJK);
            this.Controls.Add(this.chartKematian);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnBukaInput);
            this.Controls.Add(this.lblTotalData);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.label1);
            this.Name = "FormMenuUtama";
            this.Text = "FormUtama";
            this.Activated += new System.EventHandler(this.FormMenuUtama_Activated);
            this.Load += new System.EventHandler(this.FormMenuUtama_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartKematian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartJK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblTotalData;
        private System.Windows.Forms.Button btnBukaInput;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKematian;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartJK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBukaLog;
    }
}
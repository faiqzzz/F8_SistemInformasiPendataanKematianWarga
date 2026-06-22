namespace SIP_KMW
{
    partial class FormLog
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
            this.dgvPilihLog = new System.Windows.Forms.DataGridView();
            this.cmbPilihLog = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPilihLog)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPilihLog
            // 
            this.dgvPilihLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPilihLog.Location = new System.Drawing.Point(59, 63);
            this.dgvPilihLog.Name = "dgvPilihLog";
            this.dgvPilihLog.Size = new System.Drawing.Size(682, 316);
            this.dgvPilihLog.TabIndex = 0;
            this.dgvPilihLog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgcLog_CellContentClick);
            // 
            // cmbPilihLog
            // 
            this.cmbPilihLog.FormattingEnabled = true;
            this.cmbPilihLog.Location = new System.Drawing.Point(168, 33);
            this.cmbPilihLog.Name = "cmbPilihLog";
            this.cmbPilihLog.Size = new System.Drawing.Size(121, 21);
            this.cmbPilihLog.TabIndex = 1;
            this.cmbPilihLog.SelectedIndexChanged += new System.EventHandler(this.cmbPilihLog_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pilih Jenis Log";
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPilihLog);
            this.Controls.Add(this.dgvPilihLog);
            this.Name = "FormLog";
            this.Text = "FormLog";
            this.Load += new System.EventHandler(this.FormLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPilihLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPilihLog;
        private System.Windows.Forms.ComboBox cmbPilihLog;
        private System.Windows.Forms.Label label1;
    }
}
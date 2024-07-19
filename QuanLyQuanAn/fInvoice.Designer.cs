namespace QuanLyQuanAn
{
    partial class fInvoice
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
            this.Report = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dtpkReportFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpkReportToDate = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Report
            // 
            this.Report.LocalReport.ReportEmbeddedResource = "QuanLyQuanAn.Report1.rdlc";
            this.Report.Location = new System.Drawing.Point(44, 133);
            this.Report.Name = "Report";
            this.Report.ServerReport.BearerToken = null;
            this.Report.Size = new System.Drawing.Size(1060, 600);
            this.Report.TabIndex = 0;
            // 
            // dtpkReportFromDate
            // 
            this.dtpkReportFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtpkReportFromDate.Location = new System.Drawing.Point(44, 12);
            this.dtpkReportFromDate.Name = "dtpkReportFromDate";
            this.dtpkReportFromDate.Size = new System.Drawing.Size(346, 30);
            this.dtpkReportFromDate.TabIndex = 1;
            // 
            // dtpkReportToDate
            // 
            this.dtpkReportToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtpkReportToDate.Location = new System.Drawing.Point(44, 76);
            this.dtpkReportToDate.Name = "dtpkReportToDate";
            this.dtpkReportToDate.Size = new System.Drawing.Size(346, 30);
            this.dtpkReportToDate.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.Location = new System.Drawing.Point(472, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "Xuất báo cáo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 762);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpkReportToDate);
            this.Controls.Add(this.dtpkReportFromDate);
            this.Controls.Add(this.Report);
            this.Name = "fInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "fInvoice";
            this.Load += new System.EventHandler(this.fInvoice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer Report;
        private System.Windows.Forms.DateTimePicker dtpkReportFromDate;
        private System.Windows.Forms.DateTimePicker dtpkReportToDate;
        private System.Windows.Forms.Button button1;
    }
}
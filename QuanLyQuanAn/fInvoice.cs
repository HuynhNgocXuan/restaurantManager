using Microsoft.Reporting.WinForms;
using QuanLyQuanAn.DAO;
using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanAn
{
    public partial class fInvoice : Form
    {
        public fInvoice()
        {
            InitializeComponent();
            LoadDateTimePicker();
        }
        void LoadInvoice()
        {
            DateTime dateIn = dtpkReportFromDate.Value;
            DateTime dateOut = dtpkReportToDate.Value;

            DataTable data = BillDAO.Instance.GetListBillForReport(dateIn, dateOut);

            Report.LocalReport.DataSources.Clear();
            Report.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", data));

            this.Report.RefreshReport();
        }

        private void fInvoice_Load(object sender, EventArgs e)
        {
            LoadInvoice();
        }


        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpkReportFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkReportToDate.Value = dtpkReportFromDate.Value.AddMonths(1).AddDays(-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadInvoice();
        }
    }
}

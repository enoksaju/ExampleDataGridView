using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewAndReportViewer
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// Load DataSource of binding source
			studentBindingSource.DataSource = StudentService.GetAll();
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			// Create new Report Form
			var frmReport = new ReportForm(StudentService.GetAll().ToList());

			// Showing as dialog
			frmReport.ShowDialog();
		}
	}
}

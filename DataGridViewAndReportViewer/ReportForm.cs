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
	public partial class ReportForm : Form
	{
		public ReportForm(List<Student> data)
		{
			
			InitializeComponent();

			// Load DataSource with student Data
			this.StudentBindingSource.DataSource = data;
		}

		private void ReportForm_Load(object sender, EventArgs e)
		{
			// Refresh Report
			this.reportViewer1.RefreshReport();
		}
	}
}

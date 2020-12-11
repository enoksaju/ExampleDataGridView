using iTextSharp.text;
using iTextSharp.text.pdf;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleDataGridViewAndITextSharp
{
	public partial class MainForm : Form
	{
		public MainForm()
		{

			InitializeComponent();

			// Load DataSource 
			studentBindingSource.DataSource = StudentService.GetAll();
		}

		private void btnSavePDF_Click(object sender, EventArgs e)
		{
			// Create new Document and set the page size
			Document doc = new Document(PageSize.LETTER);

			// Create a writer
			PdfWriter writer = PdfWriter.GetInstance(doc, 
				// FileStream contains the name of generate file
				new FileStream(Path.Combine(Application.StartupPath, $"Report {DateTime.Now:dd MM yyyy HHmm}.pdf"), FileMode.Create));

			// Customize metadata 
			doc.AddTitle("Students PDF");
			doc.AddCreator("Henoc Salinas");

			// Open the document to writing
			doc.Open();

			// Set Default Font

			iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

			// Write document header
			doc.Add(new Paragraph("List of Students"));
			doc.Add(Chunk.NEWLINE);

			// Create Table for students
			PdfPTable tblStudents = new PdfPTable(5);
			tblStudents.WidthPercentage = 100;

			// Set table Headers
			PdfPCell clId = new PdfPCell(new Phrase("Id", _standardFont)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

			PdfPCell clFirstName = new PdfPCell(new Phrase("First Name", _standardFont)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

			PdfPCell clLastName = new PdfPCell(new Phrase("Last Name", _standardFont)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

			PdfPCell clPhone = new PdfPCell(new Phrase("Phone", _standardFont)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

			PdfPCell clGrade = new PdfPCell(new Phrase("Grade", _standardFont)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

			// Add cells to Table
			tblStudents.AddCell(clId);
			tblStudents.AddCell(clFirstName);
			tblStudents.AddCell(clLastName);
			tblStudents.AddCell(clPhone);
			tblStudents.AddCell(clGrade);

			// Fill the table with data

			foreach (var student in StudentService.GetAll())
			{
				clId = new PdfPCell(new Phrase($"{student.Id}", _standardFont)) { BorderWidth = 0 };
				clFirstName = new PdfPCell(new Phrase($"{student.FirstName}", _standardFont)) { BorderWidth = 0 };
				clLastName = new PdfPCell(new Phrase($"{student.LastName}", _standardFont)) { BorderWidth = 0 };
				clPhone = new PdfPCell(new Phrase($"{student.Phone}", _standardFont)) { BorderWidth = 0 };
				clGrade = new PdfPCell(new Phrase($"{student.Grade}", _standardFont)) { BorderWidth = 0 };

				// Add cells to Table
				tblStudents.AddCell(clId);
				tblStudents.AddCell(clFirstName);
				tblStudents.AddCell(clLastName);
				tblStudents.AddCell(clPhone);
				tblStudents.AddCell(clGrade);
			}

			// Add Table to doc
			doc.Add(tblStudents);

			// Close Doc
			doc.Close();

			// Close Writer
			writer.Close();
		}
	}
}

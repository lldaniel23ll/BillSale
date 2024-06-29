using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Globalization;
using SpreadsheetLight;

namespace FacturaVenta
{
    public partial class Report : Form
    {
        
        public Report()
        {
            InitializeComponent();
        }
        private void Report_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            btnPDF.Enabled = false;
            btnExcel.Enabled = false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            CB_Sales objectCB = new CB_Sales();
            dataGridView1.DataSource = objectCB.searchSales(textBox1.Text);
            btnPDF.Enabled = true;
            btnExcel.Enabled = true;
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Factura numero " + textBox1.Text + ".pdf";
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";

            string client = string.Empty;
            string address = string.Empty;
            string fullDate = string.Empty;
            string number = string.Empty;
            string state = string.Empty;
            string formattedDate = string.Empty;
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Cliente"].Value != null)
                client = dataGridView1.Rows[0].Cells["Cliente"].Value.ToString();
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Direccion"].Value != null)
                address = dataGridView1.Rows[0].Cells["Direccion"].Value.ToString();
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Fecha"].Value != null)
            {
                fullDate = dataGridView1.Rows[0].Cells["Fecha"].Value.ToString();

                string[] hourdate = fullDate.Split(' ');
                string date = hourdate[0];

                formattedDate = date;

                if (!formattedDate.Contains("/"))
                {
                    string[] dateSpliit = date.Split('/');
                    string day = dateSpliit[0];
                    string month = dateSpliit[1];
                    string year = dateSpliit[2];

                    formattedDate = day + "/" + month + "/" + year;
                }
            }
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["No"].Value != null)
                number = dataGridView1.Rows[0].Cells["No"].Value.ToString();
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Estado"].Value != null)
                state = dataGridView1.Rows[0].Cells["Estado"].Value.ToString();

            string rows = string.Empty;
            decimal total = 0;
            decimal quantity = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                rows += "<tr>";
                rows += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                rows += "<td>" + row.Cells["Concepto"].Value.ToString() + "</td>";
                rows += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                rows += "<td>" + row.Cells["Total"].Value.ToString() + "</td>";
                rows += "</tr>";
                quantity += decimal.Parse(row.Cells["Cantidad"].Value.ToString());
                total += decimal.Parse(row.Cells["Total"].Value.ToString());
            }

            string HTML = Properties.Resources.pdf.ToString();

            HTML = HTML.Replace("@CLIENTE", client);
            HTML = HTML.Replace("@DIRECCION", address);
            HTML = HTML.Replace("@CODIGO", number);
            HTML = HTML.Replace("@FECHA", formattedDate);
            HTML = HTML.Replace("@ESTADO", state);

            HTML = HTML.Replace("@FILA", rows);
            HTML = HTML.Replace("@CANTIDAD", quantity.ToString());
            HTML = HTML.Replace("@TOTAL", total.ToString());

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create)) 
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Properties.Resources.LOGO_removebg, System.Drawing.Imaging.ImageFormat.Png);
                    image.ScaleToFit(80, 60);
                    image.Alignment = iTextSharp.text.Image.UNDERLYING;
                    image.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                    pdfDoc.Add(image);

                    using(StringReader reader = new StringReader(HTML))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                    }
                    pdfDoc.Close();
                    stream.Close();
                    textBox1.Clear();
                    textBox1.Focus();
                }
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Factura numero " + textBox1.Text + ".xlsx";
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SLDocument sl = new SLDocument();
                SLStyle style = new SLStyle();
                style.Font.Bold = true;

                int IC = 0;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    sl.SetCellValue(1, IC, column.HeaderText.ToString());
                    sl.SetCellStyle(1, IC, style);
                    IC++;
                }

                int IR = 2;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        sl.SetCellValue(IR, i, row.Cells[i].Value.ToString());
                    }
                    IR++;
                }

                sl.SaveAs(saveFileDialog.FileName);
            }
        }
    }
}

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

namespace FacturaVenta
{
    public partial class Report : Form
    {
        
        public Report()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CB_Sales objectCB = new CB_Sales();
            dataGridView1.DataSource = objectCB.searchSales(textBox1.Text);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Factura numero " + textBox1.Text + ".pdf";
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog.ShowDialog();

            string client = string.Empty;
            string address = string.Empty;
            string fullDate = string.Empty;
            string number = string.Empty;
            string state = string.Empty;
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Cliente"].Value != null)
                client = dataGridView1.Rows[0].Cells["Cliente"].Value.ToString();
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Direccion"].Value != null)
                address = dataGridView1.Rows[0].Cells["Direccion"].Value.ToString();
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Fecha"].Value != null)
                fullDate = dataGridView1.Rows[0].Cells["Fecha"].Value.ToString();
            //DateTime hourDate = DateTime.ParseExact(fullDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            //string formatedDate = hourDate.ToString("dd/MM/yyyy");
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["No"].Value != null)
                number = dataGridView1.Rows[0].Cells["No"].Value.ToString();
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells["Estado"].Value != null)
                state = dataGridView1.Rows[0].Cells["Estado"].Value.ToString();

            string rows = string.Empty;
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                rows += "<tr>";
                rows += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                rows += "<td>" + row.Cells["Concepto"].Value.ToString() + "</td>";
                rows += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                rows += "<td>" + row.Cells["Total"].Value.ToString() + "</td>";
                rows += "</tr>";
                total += decimal.Parse(row.Cells["Total"].Value.ToString());
            }

            string HTML = Properties.Resources.pdf.ToString();

            HTML = HTML.Replace("@CLIENTE", client);
            HTML = HTML.Replace("@DIRECCION", address);
            HTML = HTML.Replace("@CODIGO", number);
            HTML = HTML.Replace("@FECHA", fullDate);
            HTML = HTML.Replace("@ESTADO", state);

            HTML = HTML.Replace("@FILA", rows);
            HTML = HTML.Replace("@TOTAL", total.ToString());

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create)) 
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Properties.Resources.LOGO, System.Drawing.Imaging.ImageFormat.Jpeg);
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
                }
            }
        }
    }
}

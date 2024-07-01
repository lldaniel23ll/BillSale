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
using DocumentFormat.OpenXml.Spreadsheet;

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
                    MessageBox.Show("Datos exportados exitosamente!");
                }
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "Factura numero " + textBox1.Text + ".xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SLDocument sl = new SLDocument();
                    SLStyle style = new SLStyle();
                    style.Font.Bold = true;

                    // Define border and centered style
                    SLStyle borderCenteredStyle = new SLStyle();
                    borderCenteredStyle.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                    borderCenteredStyle.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                    borderCenteredStyle.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                    borderCenteredStyle.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
                    borderCenteredStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                    borderCenteredStyle.Alignment.Vertical = VerticalAlignmentValues.Center;

                    // Define style for wrapping text
                    SLStyle wrapTextStyle = new SLStyle();
                    wrapTextStyle.SetWrapText(true);
                    wrapTextStyle.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                    wrapTextStyle.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                    wrapTextStyle.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                    wrapTextStyle.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
                    wrapTextStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                    wrapTextStyle.Alignment.Vertical = VerticalAlignmentValues.Center;

                    // Set column headers
                    sl.SetCellValue(1, 1, "No");
                    sl.SetCellValue(1, 2, "Cliente");
                    sl.SetCellValue(1, 3, "Dirección");
                    sl.SetCellValue(1, 4, "Concepto");
                    sl.SetCellValue(1, 5, "Fecha");
                    sl.SetCellValue(1, 6, "Estado");
                    sl.SetCellValue(1, 7, "Precio");
                    sl.SetCellValue(1, 8, "Cantidad");
                    sl.SetCellValue(1, 9, "Total");

                    // Apply bold, border, and centered style to headers
                    for (int col = 1; col <= 9; col++)
                    {
                        sl.SetCellStyle(1, col, style);
                        sl.SetCellStyle(1, col, borderCenteredStyle);
                    }

                    // Set column widths
                    sl.SetColumnWidth(2, 20); // Cliente
                    sl.SetColumnWidth(3, 20); // Dirección
                    sl.SetColumnWidth(4, 20); // Concepto
                    sl.SetColumnWidth(5, 12); // Fecha

                    int rowIndex = 2; // Start at row 2 for data
                    double totalCantidad = 0; // Variable to hold the sum of Cantidad
                    double totalTotal = 0; // Variable to hold the sum of Total

                    // Loop through each row in the DataGridView
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["No"].Value != null)
                            sl.SetCellValue(rowIndex, 1, row.Cells["No"].Value.ToString());

                        if (row.Cells["Cliente"].Value != null)
                            sl.SetCellValue(rowIndex, 2, row.Cells["Cliente"].Value.ToString());

                        if (row.Cells["Direccion"].Value != null)
                            sl.SetCellValue(rowIndex, 3, row.Cells["Direccion"].Value.ToString());

                        if (row.Cells["Concepto"].Value != null)
                            sl.SetCellValue(rowIndex, 4, row.Cells["Concepto"].Value.ToString());

                        if (row.Cells["Fecha"].Value != null)
                        {
                            string fullDate = row.Cells["Fecha"].Value.ToString();
                            string[] hourdate = fullDate.Split(' ');
                            string date = hourdate[0];
                            string formattedDate = date.Contains("/") ? date : $"{date.Split('/')[0]}/{date.Split('/')[1]}/{date.Split('/')[2]}";
                            sl.SetCellValue(rowIndex, 5, formattedDate);
                        }

                        if (row.Cells["Estado"].Value != null)
                            sl.SetCellValue(rowIndex, 6, row.Cells["Estado"].Value.ToString());

                        if (row.Cells["Precio"].Value != null)
                            sl.SetCellValue(rowIndex, 7, row.Cells["Precio"].Value.ToString());

                        if (row.Cells["Cantidad"].Value != null)
                        {
                            double cantidad;
                            if (double.TryParse(row.Cells["Cantidad"].Value.ToString(), out cantidad))
                            {
                                sl.SetCellValue(rowIndex, 8, cantidad);
                                totalCantidad += cantidad; // Sum the Cantidad values
                            }
                        }

                        if (row.Cells["Total"].Value != null)
                        {
                            double total;
                            if (double.TryParse(row.Cells["Total"].Value.ToString(), out total))
                            {
                                sl.SetCellValue(rowIndex, 9, total);
                                totalTotal += total; // Sum the Total values
                            }
                        }

                        // Apply border and centered style to each cell in the current row
                        for (int col = 1; col <= 9; col++)
                        {
                            if (col == 2 || col == 3 || col == 4) // Apply wrap text style to specific columns
                            {
                                sl.SetCellStyle(rowIndex, col, wrapTextStyle);
                            }
                            else
                            {
                                sl.SetCellStyle(rowIndex, col, borderCenteredStyle);
                            }
                        }

                        rowIndex++;
                    }

                    // Write the total Cantidad and Total values to a new row
                    SLStyle totalStyle = new SLStyle();
                    totalStyle.Font.Bold = true;
                    totalStyle.Font.FontColor = System.Drawing.Color.Black;
                    totalStyle.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Yellow, System.Drawing.Color.Yellow);
                    totalStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                    totalStyle.Alignment.Vertical = VerticalAlignmentValues.Center;
                    totalStyle.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                    totalStyle.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                    totalStyle.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                    totalStyle.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;

                    int totalRowIndex = rowIndex; // Index of the total row
                    sl.MergeWorksheetCells(totalRowIndex, 1, totalRowIndex, 7); // Merge cells from A to G
                    sl.SetCellValue(totalRowIndex, 1, "Total");
                    sl.SetCellValue(totalRowIndex, 8, totalCantidad);
                    sl.SetCellValue(totalRowIndex, 9, totalTotal);
                    sl.SetCellStyle(totalRowIndex, 1, totalStyle);
                    sl.SetCellStyle(totalRowIndex, 8, totalStyle);
                    sl.SetCellStyle(totalRowIndex, 9, totalStyle);

                    // Apply border and centered style to the merged cells
                    for (int col = 1; col <= 7; col++)
                    {
                        sl.SetCellStyle(totalRowIndex, col, totalStyle);
                    }

                    // Save the Excel file
                    sl.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Datos exportados exitosamente!");
                }
            }
            else
            {
                MessageBox.Show("No hay datos para exportar.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturaVenta
{
    public partial class Form1 : Form
    {
        CB_Sales objectCB = new CB_Sales();
        private string idSale = null;
        private bool editSale = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void showSales()
        {
            CB_Sales obj = new CB_Sales();
            dataGridView1.DataSource = obj.showSales();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Color.FromArgb((int)(0.88 * 255), (int)(0.88 * 255), (int)(0.88 * 255));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(editSale == false)
            {
                try
                {
                    DateTime getDate = dateTimePicker1.Value;
                    objectCB.insertSales(txtNumber.Text, txtClient.Text, txtAddress.Text, getDate.ToString(), txtConcept1.Text,
                                         txtQuantity1.Text, txtPrice1.Text, comboBox1.Text);
                    MessageBox.Show("Se inserto correctamente!");
                    showSales();
                    clearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo editar!\n" + ex.Message);
                }
            }
            if (editSale == true)
            {
                try
                {
                    DateTime getDate = dateTimePicker1.Value;
                    objectCB.editSales(idSale, txtNumber.Text, txtClient.Text, txtAddress.Text, getDate.ToString(), txtConcept1.Text,
                                        txtQuantity1.Text, txtPrice1.Text, comboBox1.Text);
                    MessageBox.Show("Se edito correctamente!");
                    showSales();
                    clearForm();
                    editSale = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo editar!\n" + ex.Message);
                }
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                editSale = true;
                txtNumber.Text = dataGridView1.CurrentRow.Cells["No"].Value.ToString();
                txtClient.Text = dataGridView1.CurrentRow.Cells["Cliente"].Value.ToString();
                txtAddress.Text = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    object dateCellValue = dataGridView1.CurrentRow.Cells["Fecha"].Value;
                    if (dateCellValue != null && dateCellValue != DBNull.Value)
                    {
                        if (DateTime.TryParse(dateCellValue.ToString(), out DateTime dateSelected))
                            dateTimePicker1.Value = dateSelected;
                        else
                            MessageBox.Show("La fecha no tiene un formato válido.");
                    }
                    else
                        MessageBox.Show("La celda de fecha está vacía o no contiene un valor válido.");
                }
                txtConcept1.Text = dataGridView1.CurrentRow.Cells["Concepto"].Value.ToString();
                
                txtQuantity1.Text = dataGridView1.CurrentRow.Cells["Cantidad"].Value.ToString();
                txtPrice1.Text = dataGridView1.CurrentRow.Cells["Precio"].Value.ToString();
                try
                {
                    comboBox1.Text = dataGridView1.CurrentRow.Cells["Estado"].Value.ToString();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                
                idSale = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            }
            else
                MessageBox.Show("Seleccione toda la fila");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idSale = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                objectCB.deleteSales(idSale);
                MessageBox.Show("Se elimino correctamente!");
                showSales();
            }
            else
                MessageBox.Show("Seleccione toda la fila");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showSales();
        }
        private void clearForm()
        {
            //txtClient.Clear();
            //txtAddress.Clear();
            //txtNumber.Clear();
            txtConcept1.Clear();
            txtQuantity1.Clear();
            txtPrice1.Clear();
        }
    }
}

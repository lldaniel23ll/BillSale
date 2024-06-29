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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private void Insert()
        {
            if (editSale == false)
            {
                try
                {
                    DateTime getDate = dateTimePicker1.Value;
                    objectCB.insertSales(txtNumber.Text, txtClient.Text, txtAddress.Text, getDate.ToString(), txtConcept1.Text,
                                         txtQuantity1.Text, txtPrice1.Text, comboBox1.Text);
                    MessageBox.Show("Se inserto correctamente!");
                    showSales();
                    txtConcept1.Focus();
                    clearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar!\n" + ex.Message);
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
                    txtConcept1.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo editar!\n" + ex.Message);
                }
            }
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
            Insert();
            
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

            txtConcept1.Focus();
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

            txtConcept1.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtClient.Focus();
            showSales();
            comboBox1.SelectedIndex = 0;
        }
        private void clearForm()
        {
            txtConcept1.Clear();
            txtQuantity1.Clear();
            txtPrice1.Clear();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
        }

        private void btnSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Insert();
        }

        private void txtClient_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Insert();
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Insert();
        }

        private void txtConcept1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Insert();
        }

        private void txtQuantity1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Insert();
        }

        private void txtPrice1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Insert();
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //Insert();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string seletion = comboBox1.SelectedItem.ToString();
            if (seletion != "Anulado")
            {
                txtClient.Enabled = true;
                txtAddress.Enabled = true;
                txtConcept1.Enabled = true;
                txtPrice1.Enabled = true;
                txtQuantity1.Enabled = true;
                txtClient.Clear();
                txtAddress.Clear();
                clearForm();
            }
            else if (seletion == "Anulado")
            {
                txtClient.Text = "ANULADO";
                txtAddress.Text = "ANULADO";
                txtConcept1.Text = "ANULADO";
                txtPrice1.Text = "0";
                txtQuantity1.Text = "0";

                txtClient.Enabled = false;
                txtAddress.Enabled = false;
                txtConcept1.Enabled = false;
                txtPrice1.Enabled = false;
                txtQuantity1.Enabled = false;
            }
        }
    }
}

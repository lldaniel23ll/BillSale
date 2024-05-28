using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturaVenta
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        public void getText()
        {
            string client = txtClient.Text;
            string address = txtAddress.Text;
            string item = comboBox1.Text;
            string number = txtNumber.Text;
            DateTime getDate = dateTimePicker1.Value;

            string concept1 = txtConcept1.Text;
            string concept2 = txtConcept2.Text;
            string concept3 = txtConcept3.Text;
            string concept4 = txtConcept4.Text;
            string concept5 = txtConcept5.Text;
            string concept6 = txtConcept6.Text;
            string concept7 = txtConcept7.Text;
            string concept8 = txtConcept8.Text;
            string concept9 = txtConcept9.Text;
            string concept10 = txtConcept10.Text;
            string concept11 = txtConcept11.Text;
            string concept12 = txtConcept12.Text;
            string concept13 = txtConcept13.Text;
            string concept14 = txtConcept14.Text;
            string concept15 = txtConcept15.Text;
            string concept16 = txtConcept16.Text;
            string concept17 = txtConcept17.Text;
            string concept18 = txtConcept18.Text;
            string concept19 = txtConcept19.Text;
            string concept20 = txtConcept20.Text;
            string concept21 = txtConcept21.Text;
            string concept22 = txtConcept22.Text;
            string concept23 = txtConcept23.Text;
            string concept24 = txtConcept24.Text;
            string concept25 = txtConcept25.Text;
            string concept26 = txtConcept26.Text;
            string concept27 = txtConcept27.Text;
            string concept28 = txtConcept28.Text;
            string concept29 = txtConcept29.Text;
        }

        public void getSumQ()
        {
            int quantity1 = Convert.ToInt32(txtQuantity1.Text);
            int quantity2 = Convert.ToInt32(txtQuantity2.Text);
            int quantity3 = Convert.ToInt32(txtQuantity3.Text);
            int quantity4 = Convert.ToInt32(txtQuantity4.Text);
            int quantity5 = Convert.ToInt32(txtQuantity5.Text);
            int quantity6 = Convert.ToInt32(txtQuantity6.Text);
            int quantity7 = Convert.ToInt32(txtQuantity7.Text);
            int quantity8 = Convert.ToInt32(txtQuantity8.Text);
            int quantity9 = Convert.ToInt32(txtQuantity9.Text);
            int quantity10 = Convert.ToInt32(txtQuantity10.Text);
            int quantity11 = Convert.ToInt32(txtQuantity11.Text);
            int quantity12 = Convert.ToInt32(txtQuantity12.Text);
            int quantity13 = Convert.ToInt32(txtQuantity13.Text);
            int quantity14 = Convert.ToInt32(txtQuantity14.Text);
            int quantity15 = Convert.ToInt32(txtQuantity15.Text);
            int quantity16 = Convert.ToInt32(txtQuantity16.Text);
            int quantity17 = Convert.ToInt32(txtQuantity17.Text);
            int quantity18 = Convert.ToInt32(txtQuantity18.Text);
            int quantity19 = Convert.ToInt32(txtQuantity19.Text);
            int quantity20 = Convert.ToInt32(txtQuantity20.Text);
            int quantity21 = Convert.ToInt32(txtQuantity21.Text);
            int quantity22 = Convert.ToInt32(txtQuantity22.Text);
            int quantity23 = Convert.ToInt32(txtQuantity23.Text);
            int quantity24 = Convert.ToInt32(txtQuantity24.Text);
            int quantity25 = Convert.ToInt32(txtQuantity25.Text);
            int quantity26 = Convert.ToInt32(txtQuantity26.Text);
            int quantity27 = Convert.ToInt32(txtQuantity27.Text);
            int quantity28 = Convert.ToInt32(txtQuantity28.Text);
            int quantity29 = Convert.ToInt32(txtQuantity29.Text);

            int totalSum = quantity1 + quantity2 + quantity3 + quantity4 + quantity5 + quantity6 + quantity7 + quantity8 + quantity9 + quantity10
             + quantity11 + quantity12 + quantity13 + quantity14 + quantity15 + quantity16 + quantity17 + quantity18 + quantity19 + quantity20
             + quantity21 + quantity22 + quantity23 + quantity24 + quantity25 + quantity26 + quantity27 + quantity28 + quantity29;

        }

        public void getSumP()
        {
            double price1 = Convert.ToDouble(txtPrice1.Text);
            double price2 = Convert.ToDouble(txtPrice2.Text);
            double price3 = Convert.ToDouble(txtPrice3.Text);
            double price4 = Convert.ToDouble(txtPrice4.Text);
            double price5 = Convert.ToDouble(txtPrice5.Text);
            double price6 = Convert.ToDouble(txtPrice6.Text);
            double price7 = Convert.ToDouble(txtPrice7.Text);
            double price8 = Convert.ToDouble(txtPrice8.Text);
            double price9 = Convert.ToDouble(txtPrice9.Text);
            double price10 = Convert.ToDouble(txtPrice10.Text);
            double price11 = Convert.ToDouble(txtPrice11.Text);
            double price12 = Convert.ToDouble(txtPrice12.Text);
            double price13 = Convert.ToDouble(txtPrice13.Text);
            double price14 = Convert.ToDouble(txtPrice14.Text);
            double price15 = Convert.ToDouble(txtPrice15.Text);
            double price16 = Convert.ToDouble(txtPrice16.Text);
            double price17 = Convert.ToDouble(txtPrice17.Text);
            double price18 = Convert.ToDouble(txtPrice18.Text);
            double price19 = Convert.ToDouble(txtPrice19.Text);
            double price20 = Convert.ToDouble(txtPrice20.Text);
            double price21 = Convert.ToDouble(txtPrice21.Text);
            double price22 = Convert.ToDouble(txtPrice22.Text);
            double price23 = Convert.ToDouble(txtPrice23.Text);
            double price24 = Convert.ToDouble(txtPrice24.Text);
            double price25 = Convert.ToDouble(txtPrice25.Text);
            double price26 = Convert.ToDouble(txtPrice26.Text);
            double price27 = Convert.ToDouble(txtPrice27.Text);
            double price28 = Convert.ToDouble(txtPrice28.Text);
            double price29 = Convert.ToDouble(txtPrice29.Text);
            double totalSum = price1 + price2 + price3 + price4 + price5 + price6 + price7 + price8 + price9 + price10
                + price11 + price12 + price13 + price14 + price15 + price16 + price17 + price18 + price19 + price20
                + price21 + price22 + price23 + price24 + price25 + price26 + price27 + price28 + price29;
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
            DateTime getDate = dateTimePicker1.Value;
            dataGridView1.Rows.Add(txtNumber.Text,txtClient.Text,txtAddress.Text,getDate,txtConcept1.Text,
                                    txtQuantity1.Text,txtPrice1.Text,comboBox1.Text);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                txtNumber.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtClient.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtAddress.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                
                if (dataGridView1.CurrentRow.Cells[3].Value is DateTime fechaSeleccionada)
                    dateTimePicker1.Value = fechaSeleccionada;
                else
                    MessageBox.Show("Halgo salio mal");
                txtConcept1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtQuantity1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtPrice1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione toda la fila");
            }
        }
    }
}

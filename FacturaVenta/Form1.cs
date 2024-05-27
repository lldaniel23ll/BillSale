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
            //this.WindowState = FormWindowState.Maximized;
            //this.MaximizeBox = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Color.FromArgb((int)(0.88 * 255), (int)(0.88 * 255), (int)(0.88 * 255));
        }
    }
}

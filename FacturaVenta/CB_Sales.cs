using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FacturaVenta
{
    class CB_Sales
    {
        private CD_Sales objectCD = new CD_Sales();
        public DataTable showSales() 
        {
            DataTable table = new DataTable();
            table = objectCD.Show();
            return table;
        }
        public void insertSales(string No, string Client, string Address, string Date, string Concept, string Quantity, string Price, string State)
        {
            objectCD.Insert(No, Client, Address, Convert.ToDateTime(Date), Concept, Convert.ToInt32(Quantity), Convert.ToDouble(Price), State);
        }
        public void editSales(string ID, string No, string Client, string Address, string Date, string Concept, string Quantity, string Price, string State)
        {
            objectCD.Edit(Convert.ToInt32(ID), No, Client, Address, Convert.ToDateTime(Date), Concept, Convert.ToInt32(Quantity), Convert.ToDouble(Price), State);
        }
        public void deleteSales(string ID)
        {
            objectCD.Delete(Convert.ToInt32(ID));
        }
    }
}

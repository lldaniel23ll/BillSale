using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturaVenta
{
    internal class DataAccessLayer
    {
        public SqlConnection connection = new SqlConnection("Server=localhost;Database=SalesInvoice;Integrated Security=True;");
        public void Insert()
        {
            try
            {
                connection.Open();
                string query = @"INSERT INTO Sales(Id, Client, Address, Concept, Date, Quantity, Price, State)
                                VALUES (@No, @Client, @Address, @Concept, @Date, @Quantity, @Price, @Status)"
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@No";
                parameter.Value = DBNull.Value;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo conecta con la base de datos\n" + e.Message);
            }
        }
    }
}

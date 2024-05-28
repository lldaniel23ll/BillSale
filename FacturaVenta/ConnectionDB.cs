using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturaVenta
{
    class ConnectionDB
    {
        public static SqlConnection Connection() 
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=SalesInvoice;Integrated Security=True;");
            connection.Open();
            return connection;
        }
    }
}

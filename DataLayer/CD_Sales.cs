using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class CD_Sales
    {
        DBConnection connection = new DBConnection();

        SqlDataReader read;
        DataTable table = new DataTable();
        SqlCommand cmd = new SqlCommand();

        public DataTable Show()
        {
            cmd.Connection = connection.openConnection();
            cmd.CommandText = "SELECT * FROM SALES";
            read = cmd.ExecuteReader();
            table.Load(read);
            connection.closeConnection();
            return table;
        }
    }
}

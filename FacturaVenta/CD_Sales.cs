using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FacturaVenta
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
            cmd.CommandText = "ShowSales";
            cmd.CommandType = CommandType.StoredProcedure;
            read = cmd.ExecuteReader();
            table.Load(read);
            connection.closeConnection();
            return table;
        }
        public void Insert(string No, string Client, string Address, DateTime Date, string Concept, int Quantity, double Price, string State)
        {
            cmd.Connection = connection.openConnection();
            cmd.CommandText = "InsertSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@No", No);
            cmd.Parameters.AddWithValue("@Client", Client);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Concept", Concept);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@State", State);
            
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
        public void Edit(int ID, string No, string Client, string Address, DateTime Date, string Concept, int Quantity, double Price, string State)
        {
            cmd.Connection= connection.openConnection();
            cmd.CommandText = "UpdateSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@No", No);
            cmd.Parameters.AddWithValue("@Client", Client);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Concept", Concept);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@State", State);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
        public void Delete(int ID)
        {
            cmd.Connection = connection.openConnection();
            cmd.CommandText = "DeleteSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
    }
}

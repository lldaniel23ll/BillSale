using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient; // Cambiado para usar MySQL
using System.Text;
using System.Threading.Tasks;

namespace FacturaVenta
{
    public class CD_Sales
    {
        DBConnection connection = new DBConnection(); // Asegúrate de que tu DBConnection también use MySQL

        MySqlDataReader read; // Cambiado de SqlDataReader a MySqlDataReader
        DataTable table = new DataTable();
        MySqlCommand cmd = new MySqlCommand(); // Cambiado de SqlCommand a MySqlCommand

        public DataTable Show()
        {
            cmd.Connection = connection.openConnection(); // Abre la conexión
            cmd.CommandText = "ShowSales"; // Nombre del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            read = cmd.ExecuteReader();
            table.Load(read);
            connection.closeConnection(); // Cierra la conexión
            return table;
        }

        public void Insert(string No, string Client, string Address, DateTime Date, string Concept, int Quantity, double Price, string State)
        {
            cmd.Connection = connection.openConnection();
            cmd.CommandText = "InsertSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_No", No); // Usa "p_" para coincidir con los nombres en MySQL
            cmd.Parameters.AddWithValue("p_Client", Client);
            cmd.Parameters.AddWithValue("p_Address", Address);
            cmd.Parameters.AddWithValue("p_Date", Date);
            cmd.Parameters.AddWithValue("p_Concept", Concept);
            cmd.Parameters.AddWithValue("p_Quantity", Quantity);
            cmd.Parameters.AddWithValue("p_Price", Price);
            cmd.Parameters.AddWithValue("p_State", State);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connection.closeConnection(); // Cierra la conexión después de la operación
        }

        public void Edit(int ID, string No, string Client, string Address, DateTime Date, string Concept, int Quantity, double Price, string State)
        {
            cmd.Connection = connection.openConnection();
            cmd.CommandText = "UpdateSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_ID", ID);
            cmd.Parameters.AddWithValue("p_No", No); // Usa "p_" para coincidir con los nombres en MySQL
            cmd.Parameters.AddWithValue("p_Client", Client);
            cmd.Parameters.AddWithValue("p_Address", Address);
            cmd.Parameters.AddWithValue("p_Date", Date);
            cmd.Parameters.AddWithValue("p_Concept", Concept);
            cmd.Parameters.AddWithValue("p_Quantity", Quantity);
            cmd.Parameters.AddWithValue("p_Price", Price);
            cmd.Parameters.AddWithValue("p_State", State);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connection.closeConnection(); // Cierra la conexión después de la operación
        }

        public void Delete(int ID)
        {
            cmd.Connection = connection.openConnection();
            cmd.CommandText = "DeleteSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_ID", ID);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connection.closeConnection(); // Cierra la conexión después de la operación
        }

        public DataTable Search(string No)
        {
            cmd.Connection = connection.openConnection();
            cmd.CommandText = "SearchSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_No", No);

            read = cmd.ExecuteReader();
            table.Load(read);
            cmd.Parameters.Clear();
            connection.closeConnection(); // Cierra la conexión después de la operación
            return table;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace SisPmsCore4.Util
{
    public class DAL
    {
        private static string server = "localhost";
        private static string database = "sispmscore";
        private static string user = "root";
        private static string password = "";

        private string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password}";
        //private MySqlConnection connection;
        private MySqlConnection connection;

        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        //Executa Select
        public DataTable RetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dataTable);
            return dataTable;
        }

        //Executa Insert, Update, Delete
        public void ExecutarComandoSQL(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}

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
        //private static string server = "localhost";
        private static string server = "bd.asp.hostazul.com.br";
        private static string  database = "9256_sispmscore";
        //private static string user = "root";
        private static string user = "9256_aplicacao";
        //private static string password = "";
        private static string password = "cqt024dcqt024d";
        private static string port = "4406";

        private string connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password}";
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

        public void FechaarConexao()
        {
            connection.Close();
        }
    }
}

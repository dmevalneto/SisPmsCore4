using MySql.Data.MySqlClient;
using SisPmsCore4.Resources;
using System.Data;
using System;
namespace SisPmsCore4.Util
{
    public class DAL
    {
        ////private static string server = "localhost";
        //private static string server = "bd.asp.hostazul.com.br";
        //private static string  database = "9256_sispmscore";
        ////private static string user = "root";
        //private static string user = "9256_aplicacao";
        ////private static string password = "";
        //private static string password = "cqt024dcqt024d";
        //private static string port = "4406";


        //ESTA É UMA MANEIRA MELHOR DE ARMAZENAR SUA CONNECTIONSTRING
        //SE PRECISAR ALTERAR, VC NÃO PRECISA RECOMPILAR NOVAMENTE, BASTA MODIFICAR O ARQUIVO JSON
        private string connectionString = SystemResources.stringConnection;// $"Server={server};Port={port};Database={database};Uid={user};Pwd={password}";
        private MySqlConnection connection;

        private void Setup()
        {
            if (String.IsNullOrWhiteSpace(SystemResources.stringConnection))
                SystemResources.SetSystemJsonVariables();

            this.connectionString = SystemResources.stringConnection;
            this.connection = new MySqlConnection();
        }

        public DAL() => Setup();

        private void ManageConnection()
        {
            if (this.connection.State == ConnectionState.Closed)
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
        }

        //Executa Select
        public DataTable RetDataTable(string sql)
        {
            try
            {
                ManageConnection();
                DataTable dataTable = new DataTable();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                da.Fill(dataTable);
                return dataTable;
            }
            finally
            {
                connection.Close();
            }

        }

        //Executa Insert, Update, Delete
        public void ExecutarComandoSQL(string sql)
        {
            try
            {
                ManageConnection();
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

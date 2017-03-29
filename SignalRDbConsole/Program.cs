using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDbConsole
{
    class Program
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private static string connString =
@"Data Source=HCTOITDEV1\SQLEXPRESS;Database=SqlDependencyTest;Persist Security Info=false;
  Integrated Security=false;User Id=startUser;Password=startUser";

        static void Main(string[] args)
        {
            DateTime date = System.DateTime.Today;

            for (int i = 0;i < 1000000; i++)
            {
                System.Threading.Thread.Sleep(400);
                string message = "message" + i.ToString();
                string emptyMessage = "Empty Message" + i.ToString();
                AddMessageToTable(message, emptyMessage, date);
                Console.WriteLine("writing: " + i.ToString());

            }

            Console.WriteLine("done here");
            Console.ReadLine();
        }

        static void AddMessageToTable(string message, string emptyMessage, DateTime date)
        {
            //string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    string connString = @"Data Source=HCTOITDEV1\SQLEXPRESS;Database=SqlDependencyTest;Persist Security Info=false;
            Integrated Security=false;User Id=subscribeUser;Password=subscribeUser";

            string query = "INSERT INTO [dbo].[Messages] (Message, EmptyMessage, Date) VALUES( '" + message
                + "','" + emptyMessage + "','" + date + "')";

            System.Data.SqlClient.SqlConnection sqlConnection =
               new System.Data.SqlClient.SqlConnection(connString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();


        }
    }
}

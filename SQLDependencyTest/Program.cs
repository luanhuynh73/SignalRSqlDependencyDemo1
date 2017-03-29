using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace SqlDependencyTest
{
    //https://www.codeproject.com/Articles/12862/Minimum-Database-Permissions-Required-for-SqlDepen
    class Program
    {
        private static string mStarterConnectionString =
    @"Data Source=HCTOITDEV1\SQLEXPRESS;Database=SqlDependencyTest;Persist Security Info=false;
  Integrated Security=false;User Id=startUser;Password=startUser";
        private static string mSubscriberConnectionString =
    @"Data Source=HCTOITDEV1\SQLEXPRESS;Database=SqlDependencyTest;Persist Security Info=false;
Integrated Security=false;User Id=subscribeUser;Password=subscribeUser";

        static void Main(string[] args)
        {
            // Starting the listener infrastructure...
            SqlDependency.Start(mStarterConnectionString);

            // Registering for changes... 
            RegisterForChanges();

            // Waiting...
            Console.WriteLine("At this point, you should start the Sql Server ");
            Console.WriteLine("Management Studio and make ");
            Console.WriteLine("some changes to the Users table that you'll find");
            Console.WriteLine(" in the SqlDependencyTest ");
            Console.WriteLine("database. Every time a change happens in this ");
            Console.WriteLine("table, this program should be ");
            Console.WriteLine("notified.\n");
            Console.WriteLine("Press enter to quit this program.");
            Console.ReadLine();

            // Quitting...
            SqlDependency.Stop(mStarterConnectionString);
        }

        public static void RegisterForChanges()
        {
            // Connecting to the database using our subscriber connection string 
            // and waiting for changes...
            SqlConnection oConnection
                                = new SqlConnection(mSubscriberConnectionString);
            oConnection.Open();
            try
            {
                SqlCommand oCommand = new SqlCommand(
                  "SELECT ID, Name FROM dbo.Users",
                  oConnection);
                SqlDependency oDependency = new SqlDependency(oCommand);
                oDependency.OnChange += new OnChangeEventHandler(OnNotificationChange);
                SqlDataReader objReader = oCommand.ExecuteReader();
                try
                {
                    while (objReader.Read())
                    {
                        // Doing something here...
                    }
                }
                finally
                {
                    objReader.Close();
                }
            }
            finally
            {
                oConnection.Close();
            }
        }

        public static void OnNotificationChange(object caller,
                                                SqlNotificationEventArgs e)
        {
            Console.WriteLine(e.Info.ToString() + ": " + e.Type.ToString());
            RegisterForChanges();
        }
    }
}
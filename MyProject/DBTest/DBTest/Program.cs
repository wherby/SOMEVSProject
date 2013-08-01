using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string source = "server=WIN-AT4PG4A7DL4;" + "integrated security=SSPI;" + "database=TestTao;";
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            string select = "SELECT * FROM Class";
            string delete = @"DELETE FROM Class WHERE ID<>11";
            SqlCommand deleteCMD = new SqlCommand(delete, conn);
            deleteCMD.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0} ,{1}", reader.GetString(0), reader.GetSqlValue(1));
            }

            conn.Close();

            conn.Open();
   
            string insert=@"INSERT INTO Class(Class,ID) Values('test',13)";
            SqlCommand insertCMD = new SqlCommand(insert, conn);
            insertCMD.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand(select, conn);
            SqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                Console.WriteLine("{0} ,{1}", reader2.GetString(0), reader2.GetSqlValue(1));
            }
            conn.Close();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ADDForDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "server=WIN-AT4PG4A7DL4;" + "integrated security=SSPI;" + "database=TestTao;";
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            string select = "SELECT * FROM Class";

            SqlCommand cmd2 = new SqlCommand(select, conn);
            SqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                Console.WriteLine("{0} ,{1}", reader2.GetString(0), reader2.GetSqlValue(1));
            }
            conn.Close();
            Console.ReadLine();
        }
    }
}

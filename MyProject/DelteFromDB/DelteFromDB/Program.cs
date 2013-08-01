using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace DelteFromDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "server=VMM-Test-SQL;" + "integrated security=SSPI;" + "database=TestTao;";
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
        }
    }
}

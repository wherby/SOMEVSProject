using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

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


            int number = 12;

            SqlCommand cmd2 = new SqlCommand(select, conn);
            SqlDataReader reader2 = cmd2.ExecuteReader();

            try
            {
                while (reader2.Read())
                {
                    //  Console.WriteLine("{0} ,{1}", reader2.GetString(0), reader2.GetSqlValue(1));
                    number = int.Parse(reader2.GetSqlValue(1).ToString());

                }
            }
            catch { }
            reader2.Close();

            string className = "Before snap";
            while (className != "STOP")
            {
                try
                {
                    className = File.ReadAllLines(@".\className.txt")[0];
                }
                catch
                {
                }
                number = number + 1;
                string insert = string.Format("INSERT INTO Class(Class,ID) Values('{0}',{1})", className, number);
                SqlCommand insertCMD = new SqlCommand(insert, conn);
                insertCMD.ExecuteNonQuery();
                Thread.Sleep(1000);


                 cmd2 = new SqlCommand(select, conn);
                 reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    Console.WriteLine("{0} ,{1}", reader2.GetString(0), reader2.GetSqlValue(1));
                    number = int.Parse(reader2.GetSqlValue(1).ToString());

                }
                reader2.Close();

            }
            conn.Close();
        }
    }
}

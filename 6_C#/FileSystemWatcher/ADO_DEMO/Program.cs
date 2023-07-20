using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DEMO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSet ds = new DataSet();
            var connectionString = "server=127.0.0.1;uid=root;pwd=mindfire;database=profileapplication";
            using (var con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                var sde = new MySqlDataAdapter("Select * from countries", con);
                sde.Fill(ds,"countries");
                foreach (DataRow row in ds.Tables["countries"].Rows)
                {
                    Console.WriteLine(row["countryId"] + ":  " + row["countryName"]);
                }

                Console.ReadLine();
            }
        }
    }
}

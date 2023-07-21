using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ADO_DEMO
{
    internal class Program
    {
        public static void InsertCountries(string connectionString)
        {   
            using (var con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO countries VALUES (7,'Afghanistan')", con))
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected);
                    con.Close();
                }
            }
        }
        public static void SelectCountries(string connectionString)
        {
            var dt = new DataTable();
            using (var con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                var sde = new MySqlDataAdapter("Select * from countries", con);
                sde.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["countryId"] + ":  " + row["countryName"]);
                }
            }
        }
        public static void GetStatesAndCountry(string connectionString)
        {
            DataTable dt = new DataTable();
            using (var con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getStatesAndCountry", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", 1);
                    var sde = new MySqlDataAdapter(cmd);
                    sde.Fill(dt);
                    Console.WriteLine("States and country of the required user:");
                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine(row["firstName"]+"   "+row["countryName"] + ":  " + row["stateName"]);
                    }
                }

                
            }
        }
        public static void UpdateCountry(string connectionString)
        {

            using (var con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("update countries set countryName='France' where countryId=4", con))
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected+" Updated");
                    con.Close();
                }
            }
        }

        public static void DeleteCountry(string connectionString)
        {

            using (var con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from countries where countryId=4", con))
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected+" Deleted");
                    con.Close();
                }
            }
        }
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MySQLConString"].ConnectionString;
            //InsertCountries(connectionString);
            //SelectCountries(connectionString);
            GetStatesAndCountry(connectionString);
            //UpdateCountry(connectionString);
            Console.ReadKey();
        }
    }
}

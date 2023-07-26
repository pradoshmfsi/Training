using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ADO_DEMO
{
    internal class Program
    {
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLConString"].ConnectionString;

        public static void InsertCountries(MySqlConnection con)
        {
            Console.WriteLine("Enter the details(country ID,country Name");
            int countryId = Int32.Parse(Console.ReadLine());
            string countryName = Console.ReadLine();
            using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO countries VALUES ({countryId},'{countryName}')", con))
            {

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected+" rows inserted");

            }

        }
        public static void SelectCountries(MySqlConnection con)
        {
            var dt = new DataTable();

            var sde = new MySqlDataAdapter("Select * from countries", con);
            sde.Fill(dt);
            Console.WriteLine("\n The country list is as follows \n");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["countryId"] + ":  " + row["countryName"]);
            }

        }
        public static void GetStatesAndCountry(MySqlConnection con)
        {
            DataTable dt = new DataTable();
            Console.WriteLine("Enter the userId");
            int userId = Int32.Parse(Console.ReadLine());
            using (MySqlCommand cmd = new MySqlCommand("getStatesAndCountry", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                var sde = new MySqlDataAdapter(cmd);
                sde.Fill(dt);
                Console.WriteLine("States and country of the required user:");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["firstName"]+"   "+row["countryName"] + ":  " + row["stateName"]);
                }
            }
        }
        public static void UpdateCountry(MySqlConnection con)
        {
            Console.WriteLine("Enter the countryId");
            int countryId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new countryName");
            string countryName = Console.ReadLine();
            using (MySqlCommand cmd = new MySqlCommand($"update countries set countryName='{countryName}' where countryId={countryId}", con))
                {
                
           //     var sde = new MySqlDataAdapter("Select * from countries", con);
           //     sde.UpdateCommand = new MySqlCommand(
           //"UPDATE countries SET countryName = @countryName WHERE countryId = @countryId", con);

           //     sde.UpdateCommand.Parameters.Add(
           //        "@countryName", MySqlDbType.String, 15, "countryName");

           //     MySqlParameter parameter = sde.UpdateCommand.Parameters.Add(
           //       "@countryId", MySqlDbType.Int64);
           //     parameter.SourceColumn = "countryId";
           //     parameter.SourceVersion = DataRowVersion.Original;
                
           //     var dt = new DataTable();
           //     sde.Fill(dt);


           //     dt.Rows[0]["countryName"] = "NewIndia";
           //     dt.Rows[1]["countryName"] = "NewBanglaDesh";

                //Console.WriteLine("Reached update function");

                //Console.WriteLine(sde.Update(dt));



                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " Updated");

            }
        }

        public static void DeleteCountry(MySqlConnection con)
        {
            Console.WriteLine("Enter the countryId");
            int countryId = Int32.Parse(Console.ReadLine());
            using (MySqlCommand cmd = new MySqlCommand($"delete from countries where countryId={countryId}", con))
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " Deleted");
            }
        }
        static void Main()
        {
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                
                int choice = 1;
                while (choice!=6)
                {
                    Console.WriteLine("1.Insert into countries\n2.Display all the countries\n3.Update a country\n4.Delete a country\n5.Get the country and state of an user\nEnter your choice");

                    choice = Int32.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            InsertCountries(con);
                            break;
                        case 2:
                            SelectCountries(con);
                            break;
                        case 3:
                            UpdateCountry(con);
                            break;
                        case 4:
                            DeleteCountry(con);
                            break;
                        case 5:
                            GetStatesAndCountry(con);
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;

                    }
                }
                
                con.Close();
            }
        }
    }
}

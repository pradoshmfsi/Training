using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DEMO
{
    internal class Program
    {
        public static void UpdateCountry()
        {
            Console.WriteLine("Enter the countryId");
            int countryId = Int32.Parse(Console.ReadLine());
            using(var dbcontext = new profileapplicationEntities()){
                var country = dbcontext.country_table.FirstOrDefault(i => i.countryId == countryId);
                Console.WriteLine("Enter the countryName");
                country.countryName = Console.ReadLine();
                
                dbcontext.SaveChanges();
                Console.WriteLine("Updated successfully");
            }

        }
        public static void DeleteCountry()
        {
            Console.WriteLine("Enter the countryId");
            int countryId = Int32.Parse(Console.ReadLine());
            using (var dbcontext = new profileapplicationEntities())
            {
                var country = dbcontext.country_table.Where(i => i.countryId == countryId);
                dbcontext.country_table.Remove(country.ToList()[0]);
                dbcontext.SaveChanges();
                Console.WriteLine("Deleted successfully");
            }
        }
        public static void GetCountry()
        {
            using (var dbcontext = new profileapplicationEntities())
            {
                Console.WriteLine("Stored procedures");
                var country = dbcontext.getData();
                foreach (var row in country)
                    Console.WriteLine(row.countryName);
            }
        }

        public static void GetStates()
        {
            using (var dbcontext = new profileapplicationEntities())
            {
                Console.WriteLine("Stored procedures");
                Console.WriteLine("Enter the stateId you want to retrieve");
                var state = dbcontext.getStates(Int32.Parse(Console.ReadLine()));
                foreach (var row in state)
                    Console.WriteLine(row.stateId +"-->"+row.stateName);
            }
        }
        public static void InsertCountry()
        {
            Console.WriteLine("Enter countryId");
            int countryId  = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter countryName");
            string countryName =Console.ReadLine();
            using (var dbcontext = new profileapplicationEntities())
            {
                country_table ct = new country_table { countryId = countryId, countryName = countryName };
                dbcontext.country_table.Add(ct);
                dbcontext.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            using(var dbcontext = new profileapplicationEntities())
            {
                var country = dbcontext.country_table;
                
                var innerJoin = dbcontext.country_table.Join(dbcontext.state_table,
                      c => c.countryId,
                      s => s.countryId,
                      (c,s) => new
                      {
                          cName = c.countryName,
                          sName = s.stateName
                      }).Where(s=>s.cName == "India");

                foreach (var ele in innerJoin)
                    Console.WriteLine(ele.cName + " " + ele.sName);


                InsertCountry();

                GetCountry();

                GetStates();

                UpdateCountry();

                DeleteCountry();
            }
            Console.ReadKey();
        }
    }
}

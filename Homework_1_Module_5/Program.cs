using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Homework_1_Module_5
{
    class Program
    {
        static void Main(string[] args)
        {


            var queryA = from a in Area.GetAreas()
                         where (a.TypeArea) == 1
                         orderby a.Name descending
                         select new { a.Name, a.FullName, a.IP };

            foreach (var item in queryA)
            {
                Console.WriteLine("{0} - {1} - {2}", item.Name, item.FullName, item.IP);
            }

           
            Console.Read();
        

    }
    }
}

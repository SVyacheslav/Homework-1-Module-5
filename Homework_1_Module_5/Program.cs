using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Homework_1_Module_5
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con.ConnectionString = connectionString;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Area; SELECT COUNT(*) as countArea FROM Area", con);
            SqlCommandBuilder cmdBldr = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();

            da.Fill(ds);
            
            int countArea = Convert.ToInt32(ds.Tables[1].Rows[0]["countArea"]);
            Area[] Areas = new Area[countArea];
            List<Area> areas = new List<Area>();
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Area area = new Area();
                area.AreaId = (int)row["AreaId"];
                area.TypeArea = (int)row["TypeArea"];
                area.Name = (string)row["Name"];
                area.ParentId = (int)row["ParentId"];
                bool.TryParse(row["NoSplit"].ToString(), out bool NoSplit);
                area.NoSplit = NoSplit;                
                bool.TryParse(row["AssemblyArea"].ToString(), out bool AssemblyArea);
                area.AssemblyArea = AssemblyArea;                
                area.FullName = (string)row["FullName"];
                bool.TryParse(row["MultipleOrders"].ToString(), out bool MultipleOrders);
                area.MultipleOrders = MultipleOrders;               
                bool.TryParse(row["HiddenArea"].ToString(), out bool HiddenArea);
                area.HiddenArea = HiddenArea;
                area.IP = row["IP"].ToString();
                area.PavilionId = (int)row["PavilionId"];
                area.TypeId = (int)row["TypeId"];
                area.OrderExecution = (int)row["OrderExecution"];
                area.Dependence = (int)row["Dependence"];
                area.WorkingPeople = (int)row["WorkingPeople"];
                area.ComponentTypeId = (int)row["ComponentTypeId"];
                area.GroupId = (int)row["GroupId"];
                Int32.TryParse(row["Segment"].ToString(), out int Segment);
                area.Segment = Segment;
                areas.Add(area);
            }

            //Подзадание a

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Подзадание a\n");
            Console.ResetColor();
            var queryA = areas
                .Where(w=>w.TypeArea == 1)
                .OrderByDescending(o=> o.Name)
                .Select(s => new{ s.Name, s.FullName, s.IP });
           
            foreach (var item in queryA)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t", item.Name, item.FullName, item.IP);
            }

            //Подзадание b

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание b\n");
            Console.ResetColor();
            var queryB = from a in areas
                         where a.ParentId == 0
                         select new { a.Name, a.FullName, a.IP };
            foreach (var item in queryB)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t", item.Name, item.FullName, item.IP);
            }

            //Подзадание c

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание c\n");
            Console.ResetColor();
            int[] Pavilion = { 1, 2, 3, 4, 5, 6 };  // Создание массива Pavilion

            var queryC = areas
                .Where(w=>w.PavilionId == (Pavilion.Where(w2 => w2 % 2 == 0).Select(s2 => s2).First() | Pavilion.Where(w2 => w2 % 2 == 0).Select(s2 => s2).ElementAt(1) | Pavilion.Where(w2 => w2 % 2 == 0).Select(s2=>s2).Last()))
                .Select(s => new { s.PavilionId, s.Name, s.FullName, s.IP });
            foreach (var item in queryC)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t", item.PavilionId, item.Name, item.FullName, item.IP);
            }

            //Подзадание d

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание d\n");
            Console.ResetColor();
            var queryD = from a in areas
                         where a.PavilionId == ((from p in Pavilion where p % 2 == 0 select p).First() | (from p in Pavilion where p % 2 == 0 select p).ElementAt(1) | (from p in Pavilion where p % 2 == 0 select p).Last())
                         select new { a.PavilionId, a.Name, a.FullName, a.IP };
            foreach (var item in queryD)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t {3}\t", item.PavilionId, item.Name, item.FullName, item.IP);
            }

            //Подзадание e



            Console.Read();
        

    }
    }
}

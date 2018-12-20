using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Homework_1_Module_5
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Area", con);
            SqlCommandBuilder cmdBldr = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();

            da.Fill(ds);
            
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
            // Проверка
            foreach (var i in queryA)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t", i.Name, i.FullName, i.IP);
            }

            //Подзадание b

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание b\n");
            Console.ResetColor();
            var queryB = from a in areas
                         where a.ParentId == 0
                         select new { a.Name, a.FullName, a.IP };
            // Выполнение запроса
            foreach (var i in queryB)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t", i.Name, i.FullName, i.IP);
            }

            //Подзадание c

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание c\n");
            Console.ResetColor();
            int[] Pavilion = { 1, 2, 3, 4, 5, 6 };  // Создание массива Pavilion
            //Для повышения эффективности в работе с локальной коллекцией тело подзапроса выносится в отдельный запрос pavilion
            var pavilion = Pavilion
                .Where(w2 => w2 % 2 == 0)
                .Select(s2 => s2);

            var queryC = areas
                .Where(w=>w.PavilionId == (pavilion.First() | pavilion.ElementAt(1) | pavilion.Last()))
                .Select(s => new { s.PavilionId, s.Name, s.FullName, s.IP });
            // Проверка
            foreach (var i in queryC)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t {3}\t", i.PavilionId, i.Name, i.FullName, i.IP);
            }

            //Подзадание d

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание d\n");
            Console.ResetColor();
            //Для повышения эффективности в работе с локальной коллекцией тело подзапроса выносится в отдельный запрос pavilion2
            var pavilion2 = from p in Pavilion
                            where p % 2 == 0
                            select p;

            var queryD = from a in areas
                         where a.PavilionId == (pavilion2.First() | pavilion2.ElementAt(1) | pavilion2.Last())
                         select new { a.PavilionId, a.Name, a.FullName, a.IP };
            // Проверка
            foreach (var i in queryD)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t {3}\t", i.PavilionId, i.Name, i.FullName, i.IP);
            }

            //Подзадание e

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание e\n");
            Console.ResetColor();

            var queryE = from a in areas
                         let newWorkingPeople = a.WorkingPeople
                         where newWorkingPeople > 1
                         select a;
            // Проверка
            foreach (var i in queryE)
            {
                Console.WriteLine("{0} ", i.WorkingPeople);
            }

            //Подзадание f

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nПодзадание f\n");
            Console.ResetColor();

            var queryF = from a in areas
                         select new { a.ParentId, a.FullName, a.Dependence }
                         into temp
                         where temp.Dependence > 0
                         select new { temp.ParentId, temp.FullName, temp.Dependence };
            // Проверка
            foreach (var i in queryF)
            {
                Console.WriteLine("{0}\t {1}\t {2}\t ", i.ParentId, i.FullName, i.Dependence);
            }


            Console.Read();
        

    }
    }
}

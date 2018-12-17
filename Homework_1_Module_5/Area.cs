using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_Module_5
{
    class Area
    {
        public int AreaId { get; set; }

        public int? TypeArea { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public bool? NoSplit { get; set; }

        public bool? AssemblyArea { get; set; }
        
        public string FullName { get; set; }

        public bool? MultipleOrders { get; set; }

        public bool? HiddenArea { get; set; }

        public string IP { get; set; }

        public int PavilionId { get; set; }

        public int TypeId { get; set; }

        public int? OrderExecution { get; set; }

        public int? Dependence { get; set; }

        public int? WorkingPeople { get; set; }

        public int? ComponentTypeId { get; set; }

        public int? GroupId { get; set; }

        public int? Segment { get; set; }


        public static Area[] GetAreas()
        {
            Area[] Areas;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Area; SELECT COUNT(*) as countArea FROM Area", con);
            DataSet ds = new DataSet();

            da.Fill(ds);

            int countArea = Convert.ToInt32(ds.Tables[1].Rows[0]["countArea"]);
            Areas = new Area[countArea];

            int i = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Areas[i] = new Area
                {
                   AreaId = Convert.ToInt32(row["AreaId"]),
                   TypeArea = Convert.ToInt32(row["TypeArea"]),
                   Name = row["Name"].ToString(),
                   ParentId = Convert.ToInt32(row["ParentId"]),
                   NoSplit = Convert.ToBoolean(row["NoSplit"]),
                   AssemblyArea = Convert.ToBoolean(row["AssemblyArea"]),
                   FullName = row["FullName"].ToString(),
                   MultipleOrders = Convert.ToBoolean(row["MultipleOrders"]),
                   HiddenArea = Convert.ToBoolean(row["HiddenArea"]),
                   IP = row["IP"].ToString(),
                   PavilionId = Convert.ToInt32(row["PavilionId"]),
                   TypeId = Convert.ToInt32(row["TypeId"]),
                   OrderExecution = Convert.ToInt32(row["OrderExecution"]),
                   Dependence = Convert.ToInt32(row["Dependence"]),
                   WorkingPeople = Convert.ToInt32(row["WorkingPeople"]),
                   ComponentTypeId = Convert.ToInt32(row["ComponentTypeId"]),
                   GroupId = Convert.ToInt32(row["GroupId"]),
                   Segment = Convert.ToInt32(row["Segment"])
                };
                i++;
            }

            return Areas;
        }



    }
}


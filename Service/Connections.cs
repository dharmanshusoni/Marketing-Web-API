using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Connections
    {
        public static string Connect()
        {
            return "Data Source=.\\SQLEXPRESS;Initial Catalog=MarketingToolV1;Trusted_Connection=True;";
            //return "Data Source=SQL5108.site4now.net;Initial Catalog=db_a8da31_marketing;User Id=db_a8da31_marketing_admin;Password=Market@123;";
            //=> optionsBuilder.UseSqlServer("Server=SQL5108.site4now.net;Database=db_a8da31_marketing;User Id=db_a8da31_marketing_admin;Password=Market@123;");
        }
    }
}

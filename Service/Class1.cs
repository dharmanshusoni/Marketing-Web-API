using System.Data.Common;
using System.Data;
using System.Diagnostics;

namespace Service
{
    public class Class1
    {
//        private readonly MarketingToolContext _context;

//        public Class1(MarketingToolContext context)
//        {
//            _context = context;
//        }

//        public int GetUser()
//        {
//            int rowsAffected;
//            string sqlVerify = "EXEC VerifyUser @ProductID, @ListPrice";
//            List<SqlParameter> parms = new List<SqlParameter>
//            { 
//                new SqlParameter { ParameterName = "@ProductID", Value = 706 },
//                new SqlParameter { ParameterName = "@ListPrice", Value = 1500 }
//            };
//            rowsAffected = _context.Database.ExecuteSqlRaw(sqlVerify, parms.ToArray());


//            // Type 2
//            List<marketing_api.Models.User> black = new List<marketing_api.Models.User>();
//            List<marketing_api.Models.User> red = new List<marketing_api.Models.User>();
//            DbCommand cmd;
//            DbDataReader rdr;

//            string sql = "EXEC SalesLT.MultipleResultsColors";

//            // Build command object  
//            cmd = _context.Database.GetDbConnection().CreateCommand();
//            cmd.CommandText = sql;

//            // Open database connection  
//            _context.Database.OpenConnection();

//            // Create a DataReader  
//            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

//            // Build collection of Black products  
//            while (rdr.Read())
//            {
//                black.Add(new marketing_api.Models.User
//                {
//                    UserId = rdr.GetInt32(0),
//                    UserFirstName = rdr.GetString(1),
//                    UserLastName = rdr.GetString(2)
//                });
//            }

//            // Advance to the next result set  
//            rdr.NextResult();

//            // Build collection of Red products  
//            while (rdr.Read())
//            {
//                red.Add(new marketing_api.Models.User
//                {
//                    UserId = rdr.GetInt32(0),
//                    UserFirstName = rdr.GetString(1),
//                    UserLastName = rdr.GetString(2)
//                });
//            }

//            Debugger.Break();

//            // Close Reader and Database Connection  
//            rdr.Close();


//            return rowsAffected;
        }
}
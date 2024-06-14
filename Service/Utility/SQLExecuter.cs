using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utility
{
    public static class SQLExecuter
    {
        public static DataTable Execute(this string SP,List<SqlParameter> parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SP);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
                SqlConnection con = new SqlConnection(Connections.Connect());
                con.Open();
                cmd.Connection = con;
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.Fill(ds);
                con.Close();
                con.Dispose();
                cmd.Dispose();

                return ds.Tables[0];
            }catch(Exception ex) {
                throw ex;
            }
        }
    }
}

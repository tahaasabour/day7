
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;

public class Program
{
    public static void Main()
    {
        using (SqlConnection con
                = new SqlConnection(ConfigurationManager.ConnectionStrings["POSBD"].ToString()))
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"sp_add_cutomer_with_orders";
            if (con.State != ConnectionState.Open)
                con.Open();

            cmd.Parameters.AddWithValue("@name", "Soliman");
            cmd.Parameters.AddWithValue("@mobile", "9998898");

            DataTable dt = new DataTable();
            dt.Columns.Add("Price", typeof(float));
            dt.Columns.Add("Date", typeof(DateTime));

            dt.Rows.Add(300, DateTime.Now);
            dt.Rows.Add(500, DateTime.Now);

            SqlParameter P = new SqlParameter();
            P.ParameterName = "@orders";
            P.SqlDbType = SqlDbType.Structured;
            P.Value = dt;

            cmd.Parameters.Add(P);


            SqlTransaction trans =   con.BeginTransaction();

            try
            {
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch 
            {
                trans.Rollback();
            }




         
            con.Close();
        }
        Console.ReadKey();
    }
}
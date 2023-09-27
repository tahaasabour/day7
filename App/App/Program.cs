
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
            cmd.CommandText = @"sp_get_customers";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
                Console.WriteLine($"ID: {row[0].ToString()}, Name: {row[1].ToString()}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            DataRow selectedRow = dt.Rows[0];
            selectedRow.Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update(dt);


        }
        Console.ReadKey();
    }
}
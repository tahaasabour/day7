
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
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top(1) Name from Customer";
            if (con.State != ConnectionState.Open)
                con.Open();
            string NumberOfCustomers = (string)cmd.ExecuteScalar();
            Console.WriteLine(NumberOfCustomers);
        }
        Console.ReadKey();
    }
}
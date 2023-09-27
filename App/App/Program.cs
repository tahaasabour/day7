
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
            cmd.CommandText = "select * from Customer; select count(ID) from Customer";
            if (con.State != ConnectionState.Open)
                con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                Console.WriteLine("No Customers");
            else
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {((int)reader[0]).ToString()}, " +
                        $"  Name: {((string)reader[1]).ToString()}, " +
                        $"Mobile: {((string)reader[2]).ToString()}");
                }

                reader.NextResult();

                while (reader.Read())
                {
                    Console.WriteLine($"Count {(int)reader[0]}");
                }

            }


        }
        Console.ReadKey();
    }
}
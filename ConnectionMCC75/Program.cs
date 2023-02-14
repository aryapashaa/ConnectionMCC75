using ConnectionMCC75.Model;
using System.Data.SqlClient;

namespace ConnectionMCC75;

public class Program
{
    SqlConnection sqlConnection;
    string connectionString = "Data Source=ARYA;Initial Catalog=db_hr_arya;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public static void Main(string[] args)
    {
        Program program = new Program();
        //Region region = new Region();
        //region.Name = "coba";

        program.GetRegions();
        //program.InsertRegion(region);
    }

    //Table Regions
    //Create
    public void InsertRegion(Region entity)
    {
        using (sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.CommandText = "INSERT INTO tb_m_regions VALUES (@name);";

            // Untuk menambahkan parameter
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = entity.Name;
            sqlCommand.Parameters.Add(pName);

            // Untuk menjalankan perintah transaksi
            sqlCommand.ExecuteNonQuery();
            sqlTransaction.Commit();

            Console.WriteLine("Data berhasil dimasukkan");

            sqlConnection.Close();
        }
    }
    //Read / View
    public void GetRegions()
    {
        sqlConnection= new SqlConnection(connectionString);
        using (sqlConnection = new SqlConnection(connectionString))
        {
            // Membuat instance SQLCommand untuk mendefinisikan query & connection
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_regions;";

            // Membuka koneksi
            sqlConnection.Open();

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                // Mengecek apakah ada data atau tidak
                if (sqlDataReader.HasRows)
                {
                    // Jika ada, maka tampilkan datanya
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine("Id : " + sqlDataReader[0]);
                        Console.WriteLine("Name : " + sqlDataReader[1]);
                    }
                }
                else
                {
                    Console.WriteLine("Data Kosong");
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();
        }
    }
    //Read by Id
    public void GetByIdRegion(int id)
    {
        sqlConnection = new SqlConnection(connectionString);
        using (sqlConnection = new SqlConnection(connectionString))
        {
            // Membuat instance SQLCommand untuk mendefinisikan query & connection
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_regions WHERE id = @id;";

            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = id;
            sqlCommand.Parameters.Add(id);
            
            // Membuka koneksi
            sqlConnection.Open();

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                // Mengecek apakah ada data atau tidak
                if (sqlDataReader.HasRows)
                {
                    // Jika ada, maka tampilkan datanya
                    Console.WriteLine("Id : " + sqlDataReader[0]);
                    Console.WriteLine("Name : " + sqlDataReader[1]);
                }
                else
                {
                    Console.WriteLine("Id tidak ditemukan");
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();
        }
    }

    //Update
    //Delete
}
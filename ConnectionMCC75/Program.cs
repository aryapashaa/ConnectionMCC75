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
        Region region = new Region();

        Console.Write("Pilih Menu: ");
        int pilih = Convert.ToInt32(Console.ReadLine());

        switch (pilih)
        {
            case 1:
                Console.Write("Masukkan nama Region baru : ");
                string inputRegion = Console.ReadLine();
                region.Name = inputRegion;
                program.InsertRegion(region);
                break;
            case 2:
                program.GetRegions();
                break;
            case 3:
                Console.Write("Masukkan ID yang ingin ditampilkan : ");
                int tampilId = Convert.ToInt32(Console.ReadLine());
                program.GetByIdRegion(tampilId);
                break;
            case 4:
                Console.Write("Masukkan ID yang ingin diubah : ");
                int editId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Masukkan nama Region baru : ");
                string editRegion = Console.ReadLine();
                region.Name = editRegion;
                program.UpdateRegion(region, editId);
                break;
            case 5:
                Console.Write("Masukkan ID yang ingin dihapus : ");
                int hapusId = Convert.ToInt32(Console.ReadLine());
                region.Id = hapusId;
                program.DeleteRegion(region);
                break;
            default:
                break;
        }
    }
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
    //Read by Id
    public void GetByIdRegion(int id)
    {
        sqlConnection = new SqlConnection(connectionString);

        // Membuat instance SQLCommand untuk mendefinisikan query & connection
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "SELECT * FROM tb_m_regions WHERE id = @id;";

        SqlParameter pId = new SqlParameter();
        pId.ParameterName = "@id";
        pId.SqlDbType = System.Data.SqlDbType.Int;
        pId.Value = id;
        sqlCommand.Parameters.Add(pId);
            
        // Membuka koneksi
        sqlConnection.Open();

        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            // Mengecek apakah ada data atau tidak
            if (sqlDataReader.HasRows)
            {
                // Jika ada, maka tampilkan datanya
                sqlDataReader.Read();
                
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
    //Update
    public void UpdateRegion(Region entity, int id)
    {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.CommandText = "UPDATE tb_m_regions SET name = @edit WHERE id = @edit_id;";

            // Untuk menambahkan parameter
            SqlParameter pEdit = new SqlParameter();
            pEdit.ParameterName = "@edit";
            pEdit.SqlDbType = System.Data.SqlDbType.VarChar;
            pEdit.Value = entity.Name;
            sqlCommand.Parameters.Add(pEdit);

            // Untuk menambahkan parameter
            SqlParameter pEditId = new SqlParameter();
            pEditId.ParameterName = "@edit_id";
            pEditId.SqlDbType = System.Data.SqlDbType.Int;
            pEditId.Value = id;
            sqlCommand.Parameters.Add(pEditId);

            // Untuk menjalankan perintah transaksi
            sqlCommand.ExecuteNonQuery();
            sqlTransaction.Commit();

        Console.WriteLine("Data berhasil diedit");

            sqlConnection.Close();
    }
    //Delete
    public void DeleteRegion(Region entity)
    {
        sqlConnection = new SqlConnection(connectionString);

        // Membuat instance SQLCommand untuk mendefinisikan query & connection
        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
        SqlCommand sqlCommand = sqlConnection.CreateCommand();
        sqlCommand.Transaction = sqlTransaction;
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "DELETE FROM tb_m_regions WHERE id = @delete_id;";

        SqlParameter pDelete_Id = new SqlParameter();
        pDelete_Id.ParameterName = "@delete_id";
        pDelete_Id.SqlDbType = System.Data.SqlDbType.Int;
        pDelete_Id.Value = entity.Id;
        sqlCommand.Parameters.Add(pDelete_Id);

        // Membuka koneksi
        sqlConnection.Open();

        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            // Mengecek apakah ada data atau tidak
            if (sqlDataReader.HasRows)
            {
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();

                Console.WriteLine("Data berhasil dihapus");
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
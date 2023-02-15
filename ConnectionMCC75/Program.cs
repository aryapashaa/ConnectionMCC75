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
        Console.WriteLine("==CRUD==");
        string[] crud = new string[] { "Create", "Read", "Read by ID","Update", "Delete" };
        for (int i = 0; i < crud.Length; i++)
        {
            Console.WriteLine($"[{i+1}] - {crud[i]}");
        }
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

            try
            {
                sqlCommand.CommandText = "INSERT INTO tb_m_regions VALUES (@name);";
                // INSERT INTO tb_m_regions VALUES ('entity.Name')

                // Parameter Name
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = entity.Name;
                sqlCommand.Parameters.Add(pName);

                // Untuk menjalankan perintah transaksi
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();

                Console.WriteLine("Data Berhasil Di Masukan");

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                try
                {
                    sqlTransaction.Rollback();
                }
                catch (Exception exRollback)
                {
                    Console.WriteLine(exRollback.Message);
                }
            }
        }
    }
    //Read / View
    public void GetRegions()
    {
        sqlConnection = new SqlConnection(connectionString);

        // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "SELECT * FROM tb_m_regions;";

        // membuka koneksi
        sqlConnection.Open();

        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            // Mengecek apakah ada data atau tidak
            if (sqlDataReader.HasRows)
            {
                //jika ada, maka tampilkan datanya
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

        try
        {
            // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_regions WHERE id = @id;";

            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            // membuka koneksi
            sqlConnection.Open();

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                // Mengecek apakah ada data atau tidak
                if (sqlDataReader.HasRows)
                {
                    //jika ada, maka tampilkan datanya
                    sqlDataReader.Read();

                    Console.WriteLine("Id : " + sqlDataReader[0]);
                    Console.WriteLine("Name : " + sqlDataReader[1]);
                }
                else
                {
                    Console.WriteLine("Id Tidak Ditemukan");
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    //Update
    public void UpdateRegion()
    {
        //sqlCommand.CommandText = "UPDATE tb_m_regions SET name = @edit WHERE id = @edit_id;";
    }
    //Delete
    public void DeleteRegion()
    {
        //sqlCommand.CommandText = "DELETE FROM tb_m_regions WHERE id = @delete_id;";
    }
}
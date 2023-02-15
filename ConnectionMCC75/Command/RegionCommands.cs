using ConnectionMCC75.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionMCC75.Command;

public class RegionCommands
{
    SqlConnection sqlConnection;
    string connectionString = "Data Source=ARYA;Initial Catalog=db_hr_arya;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    public int Insert(Region entity)
    {
        int result = 0;

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
                result = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                sqlConnection.Close();

                return result;
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
            return result;
        }
    }
    public List<Region> GetAll()
    {
        List<Region> listRegion = new List<Region>();

        sqlConnection = new SqlConnection(connectionString);
        
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "SELECT * FROM tb_m_regions;";

        sqlConnection.Open();

        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Region region = new Region();
                    region.Id = Convert.ToInt16(sqlDataReader[0]);
                    region.Name = sqlDataReader[1].ToString();

                    listRegion.Add(region);
                }
            }
            sqlDataReader.Close();
        }
        sqlConnection.Close();

        return listRegion;
    }
    public Region GetById(int id)
    {
        Region region = new Region();

        sqlConnection = new SqlConnection(connectionString);

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_regions WHERE id = @id;";

            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            sqlConnection.Open();

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();

                    region.Id = Convert.ToInt16(sqlDataReader[0]);
                    region.Name = sqlDataReader[1].ToString();
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();

            return region;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return region;
    }
    public int Update(Region entity)
    {
        int result = 0;

        using (sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "UPDATE tb_m_regions SET name = @edit WHERE id = @edit_id;";
                // INSERT INTO tb_m_regions VALUES ('entity.Name')

                // Parameter Name
                SqlParameter pEditName = new SqlParameter()
                {
                    ParameterName = "@edit",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = entity.Name
                };
                sqlCommand.Parameters.Add(pEditName);

                SqlParameter pEditId = new SqlParameter();
                pEditId.ParameterName = "@edit_id";
                pEditId.SqlDbType = System.Data.SqlDbType.Int;
                pEditId.Value = entity.Id;
                sqlCommand.Parameters.Add(pEditId);

                // Untuk menjalankan perintah transaksi
                result = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();

                sqlConnection.Close();

                return result;
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
            return result;
        }
    }
    public int Delete(int id)
    {
        int result = 0;

        using (sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "DELETE FROM tb_m_regions WHERE id = @delete_id;";

                // Parameter Name
                SqlParameter pDeleteId = new SqlParameter();
                pDeleteId.ParameterName = "@delete_id";
                pDeleteId.SqlDbType = System.Data.SqlDbType.Int;
                pDeleteId.Value = id;
                sqlCommand.Parameters.Add(pDeleteId);

                // Untuk menjalankan perintah transaksi
                result = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();

                sqlConnection.Close();

                return result;
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
            return result;
        }
    }
}

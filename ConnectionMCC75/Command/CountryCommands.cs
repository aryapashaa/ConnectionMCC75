using ConnectionMCC75.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionMCC75.Command;

public class CountryCommands
{
    SqlConnection sqlConnection;
    string connectionString = "Data Source=ARYA;Initial Catalog=db_hr_arya;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    public int Insert(Country entity)
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
                sqlCommand.CommandText = "INSERT INTO tb_m_countries VALUES (@name, @reg_id);";

                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = entity.Name;
                sqlCommand.Parameters.Add(pName);

                SqlParameter pRegName = new SqlParameter();
                pRegName.ParameterName = "@reg_id";
                pRegName.SqlDbType = System.Data.SqlDbType.VarChar;
                pRegName.Value = entity.RegionId;
                sqlCommand.Parameters.Add(pRegName);

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
    public List<Country> GetAll()
    {
        List<Country> listCountry = new List<Country>();

        sqlConnection = new SqlConnection(connectionString);

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "SELECT * FROM tb_m_countries;";

        sqlConnection.Open();

        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Country country = new Country();
                    country.Id = Convert.ToInt16(sqlDataReader[0]);
                    country.Name = sqlDataReader[1].ToString();
                    country.RegionId = Convert.ToInt16(sqlDataReader[2]);

                    listCountry.Add(country);
                }
            }
            sqlDataReader.Close();
        }
        sqlConnection.Close();

        return listCountry;
    }
    public Country GetById(int id)
    {
        Country country = new Country();

        sqlConnection = new SqlConnection(connectionString);

        try
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_countries WHERE id = @id;";

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

                    country.Id = Convert.ToInt16(sqlDataReader[0]);
                    country.Name = sqlDataReader[1].ToString();
                    country.RegionId = Convert.ToInt16(sqlDataReader[2]);
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();

            return country;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return country;
    }
    public int Update(Country entity)
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
                sqlCommand.CommandText = "UPDATE tb_m_countries SET name = @edit, region_id = @edit_region_id WHERE id = @edit_id;";

                // Parameter Name
                SqlParameter pEditName = new SqlParameter();
                pEditName.ParameterName = "@edit";
                pEditName.SqlDbType = System.Data.SqlDbType.VarChar;
                pEditName.Value = entity.Name;
                sqlCommand.Parameters.Add(pEditName);

                SqlParameter pEditRegId = new SqlParameter()
                {
                    ParameterName = "@edit_region_id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = entity.RegionId
                };
                sqlCommand.Parameters.Add(pEditRegId);

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
                sqlCommand.CommandText = "DELETE FROM tb_m_countries WHERE id = @delete_id;";

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

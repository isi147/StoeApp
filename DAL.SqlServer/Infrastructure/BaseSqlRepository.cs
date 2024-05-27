using Microsoft.Data.SqlClient;

namespace DAL.SqlServer.Infrastructure;

public abstract class BaseSqlRepository
{
	private readonly string _connectionString;

	internal BaseSqlRepository(string connectionString)
	{
		_connectionString = connectionString;
	}
	public SqlConnection OpenConnection()
	{
		var con = new SqlConnection(_connectionString);
		con.Open();
		return con;
	}

}

using DAL.SqlServer.Context;
using Dapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlUserRepository : BaseSqlRepository, IUserRepository
{
	private readonly AppDbContext _context;

	public SqlUserRepository(string connectionString, AppDbContext context) : base(connectionString)
	{
		_context = context;
	}

	public void Delete(int id)
	{
		var currentUser = _context.Users.FirstOrDefault(x => x.Id == id);
		currentUser.IsDeleted = true;
		currentUser.DeletedDate = DateTime.Now;
	}

	public IQueryable<User> GetAll()
	{
		return _context.Users.Where(x => x.IsDeleted == false).AsNoTracking();
	}

	public async Task<User> GetByIdAsync(int id)
	{
		var sql = $@"SELECT *FROM Users Where Id = @id AND IsDeleted = 0";
		using var con = OpenConnection();
		return await con.QueryFirstOrDefaultAsync<User>(sql, new { id });
	}

	public async Task<User> GetUserByEmailAsync(string email)
	{
		var sql = $@"SELECT *FROM Users Where email = @email AND IsDeleted = 0";
		using var con = OpenConnection();
		return await con.QueryFirstOrDefaultAsync<User>(sql, new { email });
	}

	public async Task RegisterAsync(User user)
	{
		await _context.AddAsync(user);
	}



	public void Update(User user)
	{
		user.UpdatedDate = DateTime.Now;
		_context.Users.Update(user);
	}
}

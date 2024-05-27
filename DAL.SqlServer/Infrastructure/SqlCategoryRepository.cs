using DAL.SqlServer.Context;
using Dapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCategoryRepository : BaseSqlRepository, ICategoryRepository
{
	private readonly AppDbContext _context;

	public SqlCategoryRepository(string connectionString, AppDbContext context) : base(connectionString)
	{
		_context = context;
	}

	public async Task AddAsync(Category category)
	{
		await _context.AddAsync(category);

	}

	public void Delete(int id)
	{
		var currentCategory = _context.Categories.FirstOrDefault(c => c.Id == id) ?? throw new Exception();//Not found atilmalidi 404
		currentCategory.IsDeleted = true;
		currentCategory.DeletedDate = DateTime.Now;
	}

	public IQueryable<Category> GetAll()
	{
		return _context.Categories.Where(p => p.IsDeleted == false).AsNoTracking();
	}

	public async Task<Category> GetByIdAsync(int id)
	{
		var sql = $@"SELECT *FROM CATEGORIES WHERE Id = @id AND IsDeleted= 0";
		using var con = OpenConnection();
		return await con.QueryFirstOrDefaultAsync<Category>(sql, new { id }) ?? throw new Exception();
	}

	public void Update(Category category)
	{
		category.UpdatedDate = DateTime.Now;
		_context.Categories.Update(category);
	}
}

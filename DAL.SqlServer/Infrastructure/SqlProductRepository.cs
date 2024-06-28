using DAL.SqlServer.Context;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Dapper;

namespace DAL.SqlServer.Infrastructure;

public class SqlProductRepository : BaseSqlRepository, IProductRepository
{
	private readonly AppDbContext _context;

	public SqlProductRepository(string connectionString, AppDbContext context) : base(connectionString)
	{
		_context = context;
	}

	public async Task AddAsync(Product product)
	{
		await _context.AddAsync(product);
	}

	public void Delete(int id)
	{
		var currentProduct = _context.Products.FirstOrDefault(p => p.Id == id);
		currentProduct.IsDeleted = true;
		currentProduct.DeletedDate = DateTime.Now;
	}

	public IQueryable<Product> GetAll()
	{
		#region dapper
		//var sql = $@"SELECT * FROM PRODUCTS WHERE  IsDeleted = 0";
		//using var conn = OpenConnection();
		//return await conn.QueryAsync<Product>(sql, conn);
		#endregion
		return _context.Products.Where(p => p.IsDeleted == false).AsNoTracking();
	}


	public async Task<Product> GetByBarcodeAync(int barcode)
	{
		var sql = $@"SELECT *FROM PRODUCTS  WHERE Barcode = @barcode AND IsDeleted= 0";
		using var con = OpenConnection();
		return await con.QueryFirstOrDefaultAsync<Product>(sql, new { barcode });
	}

	public async Task<Product> GetByIdAsync(int id)
	{
		var sql = $@"SELECT *FROM PRODUCTS  WHERE Id = @id AND IsDeleted= 0";
		using var con = OpenConnection();
		return await con.QueryFirstOrDefaultAsync<Product>(sql, new { id });

	}

	public void Update(Product product)
	{
		product.UpdatedDate = DateTime.Now;
		_context.Products.Update(product);
	}
}

using Domain.Entities;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
	private readonly AppDbContext _context;

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Invoice> Invoices { get; set; }


}

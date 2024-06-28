using DAL.SqlServer.Context;
using DAL.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork : IUnitOfWork
{
	private readonly string _connectionString;
	private readonly AppDbContext _context;

	public SqlUnitOfWork(string connectionString, AppDbContext context)
	{
		_connectionString = connectionString;
		_context = context;
	}
	public SqlProductRepository _productRepository;
	public SqlCategoryRepository _categoryRepository;
	public SqlUserRepository _userRepository;
	public SqlInvoiceRepository _invoiceRepository;
	public SqlSendEmailRepository _sendEmailRepository;

	public IProductRepository ProductRepository => _productRepository ??= new SqlProductRepository(_connectionString, _context);

	public ICategoryRepository CategoryRepository => _categoryRepository ??= new SqlCategoryRepository(_connectionString, _context);

	public IUserRepository UserRepository => _userRepository ??= new SqlUserRepository(_connectionString, _context);

	public IInvoiceRepository InvoiceRepository => _invoiceRepository ??= new SqlInvoiceRepository(_connectionString, _context);

	public ISentEmailRepository SentEmailRepository => _sendEmailRepository ??= new SqlSendEmailRepository(_connectionString, _context);
	public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}
}

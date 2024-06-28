using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
	public IProductRepository ProductRepository { get; }
	public ICategoryRepository CategoryRepository { get; }
	public IUserRepository UserRepository { get; }
	public IInvoiceRepository InvoiceRepository { get; }
	public ISentEmailRepository SentEmailRepository { get; }
	public Task SaveChangesAsync();
}

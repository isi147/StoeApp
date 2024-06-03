using Domain.Entities;

namespace Repository.Repositories;

public interface IInvoiceRepository
{
	Task AddAsync(Invoice invoice);
	void Update(Invoice invoice);
	void Delete(int id);
	Task<Invoice> GetByIdAsync(int id);
	IQueryable<Invoice> GetBySellInvoiceIdAsync(int id);
	IQueryable<Invoice> GetAll();

}

using Domain.Entity;

namespace Repository.Repositories;

public interface IProductRepository
{
	Task AddAsync(Product product);
	void Update(Product product);
	void Delete(int id);
	Task<Product> GetByIdAsync(int id);
	Task<Product> GetByBarcodeAync(int barcode);
	IQueryable<Product> GetAll();


}
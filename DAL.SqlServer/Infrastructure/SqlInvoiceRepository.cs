using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System.Data;

namespace DAL.SqlServer.Infrastructure;

public class SqlInvoiceRepository : BaseSqlRepository, IInvoiceRepository
{
	private readonly AppDbContext _context;

	public SqlInvoiceRepository(string connectionString, AppDbContext context) : base(connectionString)
	{
		_context = context;

	}

	public async Task AddAsync(Invoice invoice)
	{
		using var cnn = OpenConnection();
		var nextBarcode = await cnn.ExecuteScalarAsync<string>("EXEC GenerateBarcode");

		invoice.InvoiceBarcode = nextBarcode;

		await _context.AddAsync(invoice);

	}

	public void Delete(int id)
	{
		var currentInvoice = _context.Invoices.FirstOrDefault(x => x.Id == id); //burda sizin reklamizninz ola bilerdi exception!
		currentInvoice!.IsDeleted = true;
		currentInvoice.DeletedDate = DateTime.Now;
	}

	public IQueryable<Invoice> GetAll()
	{
		return _context.Invoices.Where(i => i.IsDeleted == false).AsNoTracking();
	}

	public async Task<Invoice> GetByIdAsync(int id)
	{
		using var con = OpenConnection();

		var sqlGetInvoice = $@"SELECT * FROM Invoices WHERE Id = {id}";


		var invoice = await con.QueryFirstOrDefaultAsync<Invoice>(sqlGetInvoice);

		if (invoice is not null)
		{
			var sqlGetDetails = $"SELECT * FROM InvoiceItem WHERE InvoiceId = {id}";
			invoice.InvoiceItems = con.Query<InvoiceItem>(sqlGetDetails);
		}

		return invoice;
	}

	public IQueryable<Invoice> GetBySellInvoiceIdAsync(int id)
	{
		return _context.Invoices.Where(i => i.IsDeleted == false && i.SellInvoiceId == id).AsNoTracking();
	}

	public void Update(Invoice invoice)
	{
		invoice.UpdatedDate = DateTime.Now;
		_context.Update(invoice);
	}
}

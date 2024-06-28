using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlSendEmailRepository : BaseSqlRepository, ISentEmailRepository
{
	private readonly AppDbContext _context;

	public SqlSendEmailRepository(string connectionString, AppDbContext context) : base(connectionString)
	{
		_context = context;
	}

	public async Task CreateAsync(SentEmail sentEmail)
	{
		await _context.Emails.AddAsync(sentEmail);
	}

	public async Task<SentEmail> GetAsync(string securityKey)
	{
		return (await _context.Emails.FirstOrDefaultAsync(x => x.SecurityKey == securityKey && !x.IsUsed))!;
	}
}

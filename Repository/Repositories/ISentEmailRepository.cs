using Domain.Entities;

namespace Repository.Repositories;

public interface ISentEmailRepository
{
	Task CreateAsync(SentEmail sentEmail);
	Task<SentEmail> GetAsync(string securityKey);
}

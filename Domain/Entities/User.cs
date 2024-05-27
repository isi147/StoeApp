using Domain.BaseEntities;
using Domain.Enums;

namespace Domain.Entity;

public class User : BaseEntity
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }
	public UserType UserType { get; set; }
}

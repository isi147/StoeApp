using Domain.BaseEntities;

namespace Domain.Entities;

public class UserToken
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string TokenName { get; set; }
	public string Token { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime ExpiredDate { get; set; }
}

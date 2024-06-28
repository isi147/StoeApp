using Domain.Enums;

namespace Domain.Entities;

public class SentEmail
{
	public int Id { get; set; }
	public string Email { get; set; }
	public string SecurityKey { get; set; }
	public bool IsUsed { get; set; }
	public DateTime CreatedDate { get; set; }
	public OtpPurposes Purpose { get; set; }

	public SentEmail()
	{
		CreatedDate = DateTime.Now;
	}
}

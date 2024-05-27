namespace A.StoreApp.Infrastructure.Security;

public class JwtSettings
{
	public string? Secret { get; set; }
	public string? ValidAudince { get; set; }
	public string? ValidIssuer { get; set; }
	public int TokenValidityInMinutes { get; set; }
}

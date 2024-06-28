using Aplication.Security;
using Common.Exceptions;
using System.Security.Claims;

namespace A.StoreApp.Infrastructure;

public class HttpUserContext : IUserContext
{
	private readonly int? _userId;
	public HttpUserContext(IHttpContextAccessor httpContextAccessor)
	{
		var id = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

		bool isOkay = int.TryParse(id, out var parsedInt);
		_userId = isOkay ? parsedInt : null;
	}
	public int? UserId => _userId;

	public int MustGetUserId()
	{
		if (_userId is null)
			throw new InvalidClientException("User has to login!");

		return _userId.Value;
	}
}

using Aplication.Security;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace A.StoreApp.Infrastructure.Middlewares
{
	public class RateLimitMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly int _requestLimit;
		private readonly TimeSpan _timeWindow;
		private readonly ConcurrentDictionary<string, List<DateTime>> _requestTimes = new ConcurrentDictionary<string, List<DateTime>>();
		private readonly IHttpContextAccessor _userContext;

		public RateLimitMiddleware(RequestDelegate next, int requestLimit, TimeSpan timeWindow, IHttpContextAccessor userContext)
		{
			_next = next;
			_requestLimit = requestLimit;
			_timeWindow = timeWindow;
			_userContext = userContext;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			var isAuthenticated = _userContext.HttpContext.User.Identity.IsAuthenticated;
			if (!isAuthenticated)
			{
				var clientId = _userContext.HttpContext?.Connection.RemoteIpAddress.ToString();
				var now = DateTime.UtcNow;
				var requestLog = _requestTimes.GetOrAdd(clientId, new List<DateTime>());
				lock (requestLog)
				{
					// Remove expired requests
					requestLog.RemoveAll(timestamp => timestamp <= now - _timeWindow);
					if (requestLog.Count >= _requestLimit)
					{
						context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
						context.Response.Headers["Retry-After"] = _timeWindow.TotalSeconds.ToString();
						return;
					}
					requestLog.Add(now);
				}
				await _next(context);
			}
			else
			{
				await _next(context);
			}

		}
	}
}
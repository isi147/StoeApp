using Common.Exceptions;
using Common.GlobalExceptionsResponses;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace A.StoreApp.Infrastructure.Middlewares;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (BadRequestException error)
		{
			var message = new List<string>() { error.Message };
			await WriteError(context, HttpStatusCode.BadRequest, message);
		}
		catch (ValidationException error)
		{
			var message = new List<string>() { error.Message };
			await WriteError(context, HttpStatusCode.BadRequest, message);
		}

		static async Task WriteError(HttpContext context, HttpStatusCode statusCode, List<string> messages)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)statusCode;
			context.Response.ContentType = "application/json; charset=utf-8";

			var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
			var json = JsonSerializer.Serialize(new ResponseModel(messages), options);
			await context.Response.WriteAsync(json);
		}
	}
}

using Aplication.Abstractions;
using Aplication.PipelineBehaviors;
using Aplication.Security;
using Aplication.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplication;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(c => c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<ISentEmailService, SentEmailService>();

		return services;
	}
}

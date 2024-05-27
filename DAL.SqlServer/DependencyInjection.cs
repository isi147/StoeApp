﻿using DAL.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace DAL.SqlServer;

public static class DependencyInjection
{
	public static IServiceCollection AddSqlServerServices(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
		return services;
	}
}

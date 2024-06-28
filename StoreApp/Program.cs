using DAL.SqlServer;
using Aplication;
using DAL.SqlServer.Context;
using DAL.SqlServer.UnitOfWork;
using Repository.Common;
using A.StoreApp.Infrastructure.Security;
using A.StoreApp.Infrastructure.Middlewares;
using System.Reflection;
using Aplication.Security;
using A.StoreApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSqlServerServices(connectionString);

// Register SqlUnitOfWork with both connection string and AppDbContext
builder.Services.AddScoped<IUnitOfWork, SqlUnitOfWork>((provider) =>
{
	var dbContext = provider.GetRequiredService<AppDbContext>();
	return new SqlUnitOfWork(connectionString!, dbContext);
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthenticationDependency(builder.Configuration);
builder.Services.AddScoped<IUserContext, HttpUserContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,$"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<RateLimitMiddleware>(3, TimeSpan.FromMinutes(1));
app.UseMiddleware<CorsMiddleware>();
app.UseCors();
app.MapControllers();

app.Run();

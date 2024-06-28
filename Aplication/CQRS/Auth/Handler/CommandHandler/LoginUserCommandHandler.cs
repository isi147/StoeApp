using Aplication.CQRS.Auth.Command.Request;
using Aplication.CQRS.Auth.Command.Response;
using Common.Exceptions;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Extensions;
using Domain.Helper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Repository.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Aplication.CQRS.Auth.Handler.CommandHandler;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, ResponseModel<LoginUserCommandResponse>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IConfiguration _configuration;

	public LoginUserCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
	{
		_unitOfWork = unitOfWork;
		_configuration = configuration;
	}


	public async Task<ResponseModel<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
	{
		var loginProvider = Guid.NewGuid().ToString(); //Muxtelif brovzerleri ferqlendirmek ucun
		var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);


		var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

		//if ( (hashedPassword == null)) // loginde validator ile yoxla 
		//	throw new BadRequestException();
		if (user.PasswordHash != hashedPassword)
		{
			throw new BadRequestException();//bu yoxlam aucndur eslinde bu olmalidi InvalideCredentialsException
		}

		var authClaims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier,user.Id.ToString()),
			new(ClaimTypes.Name,user.Name),
			new Claim("loginProvider",loginProvider),
			new Claim(ClaimTypes.Email,user.Email),
			new Claim(ClaimTypes.Role,user.UserType.ToString())
		};

		var token = TokenService.CreateToken(authClaims, _configuration);

		var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

		var loginCommandResponse = new LoginUserCommandResponse()
		{
			Token = tokenString,
			Expiration = token.ValidTo
		};
		return new ResponseModel<LoginUserCommandResponse>(loginCommandResponse);
	}
}

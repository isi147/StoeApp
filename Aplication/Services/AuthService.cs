using Aplication.Abstractions;
using Aplication.DTOs;
using Common.Exceptions;
using Common.GlobalExceptionsResponses;
using Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Repository.Common;

namespace Aplication.Services;

public class AuthService(IUnitOfWork unitOfWork, IConfiguration configuration) : IAuthService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IConfiguration _configuration = configuration;
	public async Task<ErrorResponse> ResetPassword(ResetPasswordDto request)
	{
		var otp = await _unitOfWork.SentEmailRepository.GetAsync(request.OtpCode) ?? throw new BadRequestException();

		if (DateTime.Now - otp.CreatedDate > TimeSpan.FromMinutes(15))
			throw new BadRequestException();

		var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(otp.Email);

		if (user == null)
			throw new BadRequestException();

		string hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.NewPassword);

		user.PasswordHash = hashedPassword;

		otp.IsUsed = true;

		await _unitOfWork.SaveChangesAsync();

		return new ErrorResponse();
	}

	public async Task<ErrorResponse> ValidationEmail(ValidateEmailDto validateEmailDto)
	{
		var email = await _unitOfWork.SentEmailRepository.GetAsync(validateEmailDto.SecurityKey);

		if (email.Email != validateEmailDto.Email)
			throw new BadRequestException();

		if (DateTime.Now - email.CreatedDate > new TimeSpan(0, 15, 0))
			throw new BadRequestException();

		if (email.IsUsed)
			throw new BadRequestException();

		return new ErrorResponse();
	}
}

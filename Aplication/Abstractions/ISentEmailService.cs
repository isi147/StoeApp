using Aplication.DTOs;
using Common.GlobalExceptionsResponses;

namespace Aplication.Abstractions;

public interface ISentEmailService
{
	Task<ErrorResponse> SendEmailForForgetPassword(ForgetPasswordDto forgetPasswordDto);
	Task<ErrorResponse> SendEmailForUpdateProfile(int userId);
	Task<ErrorResponse> SendEmailForRegistration(string email);
}

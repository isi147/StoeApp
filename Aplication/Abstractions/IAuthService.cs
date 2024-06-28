using Aplication.DTOs;
using Common.GlobalExceptionsResponses;

namespace Aplication.Abstractions;

public interface IAuthService
{
	Task<ErrorResponse> ValidationEmail(ValidateEmailDto validateEmailDto);
	Task<ErrorResponse> ResetPassword(ResetPasswordDto request);
}

using A.StoreApp.Constants;
using Aplication.Abstractions;
using Aplication.CQRS.Auth.Command.Request;
using Aplication.DTOs;
using Aplication.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{UserRoles.Admin}")]

public class AuthController(IAuthService authService, ISentEmailService sentEmailService, IUserContext userContext) : BaseController
{
	private readonly IAuthService _authService = authService;
	private readonly ISentEmailService _sentEmailService = sentEmailService;
	private readonly IUserContext _userContext = userContext;

	[HttpPost("Login")]
	[AllowAnonymous]
	[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier},{UserRoles.Customer}")]
	public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest userLogin)
	{
		return Ok(await Sender.Send(userLogin));
	}

	[HttpPost("Registration")]
	[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier},{UserRoles.Customer}")]
	[AllowAnonymous]
	public async Task<IActionResult> Registration([FromBody] RegistrationUserCommandRequest registration)
	{
		return Ok(await Sender.Send(registration));
	}


	[HttpPost]
	[Route("ForgetPassword/otp")]
	[AllowAnonymous]
	public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
	{
		return Ok(await _sentEmailService.SendEmailForForgetPassword(forgetPasswordDto));
	}

	[HttpPost]
	[Route("ValidationEmail")]
	[AllowAnonymous]
	public async Task<IActionResult> ValidationEmail(ValidateEmailDto validationDto)
	{
		return Ok(await _authService.ValidationEmail(validationDto));
	}

	[HttpPost]
	[Route("Register/Otp")]
	[AllowAnonymous]
	public async Task<IActionResult> SendRegisterOtp(SendRegisterOtpDto request)
	{
		var result = await _sentEmailService.SendEmailForRegistration(request.Email);
		return Ok(result);
	}

	[HttpPut]
	[Route("ResetPassword")]
	public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
	{
		return Ok(await _authService.ResetPassword(request));
	}

}

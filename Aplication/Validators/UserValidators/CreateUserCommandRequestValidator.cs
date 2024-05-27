using Aplication.CQRS.Auth.Command.Response;
using FluentValidation;

namespace Aplication.Validators.UserValidators;

public class CreateUserCommandRequestValidator : AbstractValidator<RegistrationUserCommandResponse>
{
	public CreateUserCommandRequestValidator()
	{
		RuleFor(request => request.Name)
			.NotEmpty()
			.MaximumLength(50)
			.MinimumLength(0);
		RuleFor(request => request.Surname)
			.NotEmpty()
			.MaximumLength(50)
			.MinimumLength(0);
		RuleFor(request => request.Email)
			.NotEmpty()
			.MaximumLength(50)
			.MinimumLength(0);

	}
}

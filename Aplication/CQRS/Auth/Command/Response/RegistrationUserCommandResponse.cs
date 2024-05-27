using Domain.Enums;

namespace Aplication.CQRS.Auth.Command.Response;

public class RegistrationUserCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
}

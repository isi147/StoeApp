namespace Aplication.CQRS.Auth.Command.Response;

public class LoginUserCommandResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}

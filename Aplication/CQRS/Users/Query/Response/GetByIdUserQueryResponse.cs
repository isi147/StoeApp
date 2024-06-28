namespace Aplication.CQRS.Users.Query.Response;

public class GetByIdUserQueryResponse
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
}

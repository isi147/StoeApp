using Aplication.CQRS.Categories.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Categories.Command.Request;

public class DeleteCategoryCommandRequest:IRequest
{
    public int Id { get; set; }

	public DeleteCategoryCommandRequest(int id)
	{
		Id = id;
	}
}

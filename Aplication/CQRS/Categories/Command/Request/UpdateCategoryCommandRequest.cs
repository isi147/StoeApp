using Aplication.CQRS.Categories.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Categories.Command.Request;

public class UpdateCategoryCommandRequest:IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}

using Aplication.CQRS.Categories.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Categories.Command.Request;

public class CreateCategoryCommandRequest:IRequest<ResponseModel<CreateCategoryCommandResponse>>
{
    public string Name { get; set; }
}

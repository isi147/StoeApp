using Aplication.CQRS.Invoices.Query.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Invoices.Query.Reqeust;

public class GetByIdInvoiceQueryRequest : IRequest<ResponseModel<GetByIdInvoiceQueryResponse>>
{
	public int Id { get; set; }

	public GetByIdInvoiceQueryRequest(int id)
	{
		Id = id;
	}
}

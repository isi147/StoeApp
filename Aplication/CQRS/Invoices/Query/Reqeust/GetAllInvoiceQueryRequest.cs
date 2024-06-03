using Aplication.CQRS.Invoices.Query.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Invoices.Query.Reqeust;

public class GetAllInvoiceQueryRequest : IRequest<Pagination<GetAllInvoiceQueryResponse>>
{
	public int Limit { get; set; } = 1;
	public int Page { get; set; } = 10;
}

using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Purchases.Queries;
public record GetInvoicesListQuery()
    : IRequest<Result<List<PurchacesSuppInvDto>>>
{
    public int PurchaseId { get; set; }
    public int SuppInvID { get; set; }
}
internal class GetInvoicesListQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetInvoicesListQuery, Result<List<PurchacesSuppInvDto>>>
{
    public async Task<Result<List<PurchacesSuppInvDto>>> Handle(GetInvoicesListQuery request, CancellationToken cancellationToken)
    {
        List<PurchacesSuppInvDto>? list = [.. unitOfWork.Repository<PurchaseOrder>().Entities
            .Where(x=> x.IsDeleted == false)
            .Select(n => new PurchacesSuppInvDto {PurchaseId = n.Id,SuppInvID = n.InvoiceNumber}).ToList()
            ];

        return await Result<List<PurchacesSuppInvDto>>.SuccessAsync(list);
    }
}



using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Queries.GetAllCustomer
{
    public record GetAllCustomerQuery : IRequest<Result<List<GetAllCustmerDto>>>;

    internal class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, Result<List<GetAllCustmerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllCustmerDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var cust = await _unitOfWork.Repository<Customer>().Entities
                .ProjectTo<GetAllCustmerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return await Result<List<GetAllCustmerDto>>.SuccessAsync(cust);
        }
    }
}

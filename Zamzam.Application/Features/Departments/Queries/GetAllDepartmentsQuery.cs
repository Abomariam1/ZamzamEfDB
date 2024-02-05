using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Departments.Queries
{
    public record GetAllDepartmentsQuery : IRequest<Result<List<DepartmentDTO>>>;
    internal class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, Result<List<DepartmentDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<DepartmentDTO>>> Handle(GetAllDepartmentsQuery query, CancellationToken cancellationToken)
        {
            var departments = await _unitOfWork.Repository<Department>().Entities
                .Where(x => x.IsDeleted != true)
            .ProjectTo<DepartmentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
            return await Result<List<DepartmentDTO>>.SuccessAsync(departments);

        }

    }
}

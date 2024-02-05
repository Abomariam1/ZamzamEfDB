using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Employees.Queries;

public record GetAllEmployeesQuery : IRequest<Result<List<EmployeeDTO>>>;
internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Result<List<EmployeeDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<EmployeeDTO>>> Handle(GetAllEmployeesQuery query, CancellationToken cancellationToken)
    {
        var employees = await _unitOfWork.Repository<Employee>().Entities
            .Where(x => x.IsDeleted == false).Include(x => x.Department)
            .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        var emp = employees;

        return await Result<List<EmployeeDTO>>.SuccessAsync(employees);
    }
}

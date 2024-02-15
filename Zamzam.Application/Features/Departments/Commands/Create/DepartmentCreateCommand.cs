using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Departments.Commands.Create;

public record DepartmentCreateCommand : IRequest<Result<DepartmentDTO>>, IMapFrom<Department>
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
}
internal class DepartmentCreatedCommandHandler : IRequestHandler<DepartmentCreateCommand, Result<DepartmentDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartmentCreatedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<DepartmentDTO>> Handle(DepartmentCreateCommand request, CancellationToken cancellationToken)
    {
        //_unitOfWork.RecreateCleanDatabase();
        if (request == null) throw new ArgumentNullException(nameof(request));
        Department dep = new()
        {
            DepName = request.DepartmentName,
            CreatedBy = request.CreatedBy
        };
        Department? result = await _unitOfWork.Repository<Department>().AddAsync(dep);
        if (result != null)
        {
            result.AddDomainEvent(new DepartmentCreatedEvent(result));
            await _unitOfWork.Save(cancellationToken);
            return await Result<DepartmentDTO>.SuccessAsync((DepartmentDTO)result, $"تم اضافة القسم {result.DepName} بنجاح");
        }
        return Result<DepartmentDTO>.Failure("فشل في انشاء القسم");
    }
}

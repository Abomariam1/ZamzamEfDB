using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Departments.Commands.Create
{
    public record DepartmentCreateCommand : IRequest<Result<int>>, IMapFrom<Department>
    {
        public string DepartmentName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    internal class DepartmentCreatedCommandHandler : IRequestHandler<DepartmentCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentCreatedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DepartmentCreateCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            Department dep = new()
            {
                DepName = request.DepartmentName,
                CreatedBy = request.CreatedBy
            };
            Department? result = await _unitOfWork.Repository<Department>().AddAsync(dep);
            dep.AddDomainEvent(new DepartmentCreatedEvent(dep));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(dep.Id);
        }
    }
}

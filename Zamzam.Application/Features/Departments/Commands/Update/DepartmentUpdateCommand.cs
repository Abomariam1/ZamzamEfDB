using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Features.Departments.Commands.Create;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Departments.Commands.Update
{
    public record DepartmentUpdateCommand : IRequest<Result<int>>, IMapFrom<Department>
    {
        public int DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    internal class DepartmentUpdateCommandHandler : IRequestHandler<DepartmentUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DepartmentUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            Department? dep = await _unitOfWork.Repository<Department>().GetByIdAsync(request.DepartmentId);

            var oldValue =
            dep.DepName = request.DepartmentName;
            dep.UpdatedBy = request.UpdatedBy;
            dep.UpdatedDate = DateTime.Now;

            await _unitOfWork.Repository<Department>().UpdateAsync(dep);
            dep.AddDomainEvent(new DepartmentCreatedEvent(dep));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(dep.Id, $"تم تعديل القسم");
        }
    }
}

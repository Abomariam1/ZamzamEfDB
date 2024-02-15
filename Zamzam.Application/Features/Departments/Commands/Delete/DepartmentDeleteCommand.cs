using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Departments.Commands.Delete
{
    public record DepartmentDeleteCommand : IRequest<Result<int>>, IMapFrom<Department>
    {
        public int Id { get; set; }
        public DepartmentDeleteCommand()
        {

        }

        public DepartmentDeleteCommand(int id)
        {
            Id = id;
        }
    }
    public class DepartmentDeleteCommandHandler : IRequestHandler<DepartmentDeleteCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DepartmentDeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) { return await Result<int>.FailureAsync("Pleaze choose department for deleting"); }

            Department? dep = await _unitOfWork.Repository<Department>().DeleteAsync(request.Id);
            dep.AddDomainEvent(new DepartmentDeletedEvent(dep));
            var count = await _unitOfWork.Save(cancellationToken);
            return count > 0 ? Result<int>.Success(dep.Id, $"تم حذف القسم {dep.DepName} بنجاح")
                : Result<int>.Failure(0, $"فشل في حذف القسم {dep.DepName}.");
        }
    }

}

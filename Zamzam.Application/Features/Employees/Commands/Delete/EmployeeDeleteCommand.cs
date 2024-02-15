using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Employees.Commands.Delete
{
    public record EmployeeDeleteCommand : IRequest<Result<int>>, IMapFrom<Employee>
    {
        public int Id { get; }
        public EmployeeDeleteCommand()
        {

        }
        public EmployeeDeleteCommand(int id)
        {
            Id = id;
        }
    }

    internal class EmployeeDeleteCommandHandler : IRequestHandler<EmployeeDeleteCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id <= 0)
                return await Result<int>.FailureAsync(0, "لم يتم العثور على الموظف" +
                    "");
            var result = await _unitOfWork.Repository<Employee>().DeleteAsync(request.Id);
            result.AddDomainEvent(new EmployeeDeleteEvent(result));
            int count = await _unitOfWork.Save(cancellationToken);
            return count > 0 ? await Result<int>.SuccessAsync(count, $"تم حدف الموظف {result.Name}.") :
                await Result<int>.FailureAsync("فشل في حذف الموظف {result.Name}. ");
        }
    }
}

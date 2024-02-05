using AutoMapper;
using Zamzam.Application.DTOs;
using Zamzam.Domain;

namespace Zamzam.Application.Common.Mappings;

public class MappingDepartments : Profile
{
    public MappingDepartments()
    {
        CreateMap<Department, DepartmentDTO>()
            .ForMember(dst => dst.DepartmentName,
            src => src.MapFrom(x => x.DepName));
    }
}

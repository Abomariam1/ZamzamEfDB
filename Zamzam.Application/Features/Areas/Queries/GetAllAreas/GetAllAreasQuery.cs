﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Areas.Queries.GetAllAreas
{
    public record GetAllAreasQuery : IRequest<Result<List<AreaDto>>>;

    internal class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, Result<List<AreaDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAreasQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AreaDto>>> Handle(GetAllAreasQuery query, CancellationToken cancellationToken)
        {
            List<Area>? areas = await _unitOfWork.Repository<Area>().Entities
                .Where(x => x.IsDeleted != true).Include(x => x.Employee)
                .ToListAsync(cancellationToken);
            List<AreaDto>? result = [];
            result = [.. areas.ConvertAll(x => (AreaDto)x)];
            //foreach (var area in areas)
            //{
            //    result.Add((AreaDto)area);
            //}
            return await Result<List<AreaDto>>.SuccessAsync(result);
        }
    }
}

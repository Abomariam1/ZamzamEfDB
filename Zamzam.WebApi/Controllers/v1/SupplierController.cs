using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Suppliers.Commands.Create;
using Zamzam.Application.Features.Suppliers.Commands.Delete;
using Zamzam.Application.Features.Suppliers.Commands.Update;
using Zamzam.Application.Features.Suppliers.Queries;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1;

[Authorize]
public class SupplierController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public SupplierController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Result<SupplierDto>>> AddSupplier(SupplierCreateCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        command.CreatedBy = HttpContext.User.Identity!.Name ?? "Anonymous";
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<ActionResult<Result<SupplierDto>>> UpdateSupplier(SupplierUpdateCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        command.UpdatedBy = HttpContext.User.Identity!.Name ?? "Anonymous";
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<int>>> DeleteSupplier(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new SupplierDeleteCommand(id);
        return await _mediator.Send(command);
    }
    [HttpGet]
    public async Task<ActionResult<Result<List<SupplierDto>>>> GetAll() =>
        await _mediator.Send(new GetAllSupliiersQuery());

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<SupplierDto>>> GetSupplierById(int id)
    {
        if (ModelState.IsValid)
            return BadRequest();
        return await _mediator.Send(new GetSupplierByIdQuery(id));
    }
}

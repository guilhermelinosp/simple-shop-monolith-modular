using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Commands;
using Orders.Application.Queries;

namespace Order.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController(IMediator mediator) : ControllerBase
{
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> Get(Guid id)
	{
		var query = new GetOrderByIdQuery(id);

		return Ok(await mediator.Send(query));
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody] AddOrderCommand command)
	{
		return CreatedAtAction(nameof(Get), new { id = await mediator.Send(command) }, command);
	}
}
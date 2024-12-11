using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Orders.Commands;
using OrderManagementSystem.Application.Orders.Queries;

namespace OrderManagementSystem.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        return Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok(await _mediator.Send(new GetOrdersQuery()));
    }
}

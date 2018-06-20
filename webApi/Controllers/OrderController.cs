using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using application.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
	    readonly IMediator _mediator;

	    public OrderController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }


	    [HttpPost("{id}/approve", Name = "approve3")]
	    public async Task<IActionResult> Approve3(Guid id,CancellationToken token)
	    {
		    var result = await _mediator.Send(new ApproveOrderRequest(id), token);

		    return Ok(result);
	    }
	}
}

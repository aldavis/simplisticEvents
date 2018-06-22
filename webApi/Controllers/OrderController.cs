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


		//TODO figure out why the route wont hit when i change it to just /approve
	    [HttpPost("{id}/approve", Name = "approve3")]
	    public async Task<IActionResult> Approve3([FromBody]ApproveOrderRequest request,CancellationToken token)
	    {
		    var result = await _mediator.Send(request, token);

		    return Ok(result);
	    }
	}
}

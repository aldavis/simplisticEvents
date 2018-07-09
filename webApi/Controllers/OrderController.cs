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


	    [HttpPost("/approve", Name = "approve")]
	    public async Task<IActionResult> Approve([FromBody]ApproveOrderRequest request,CancellationToken token)
	    {
		    var result = await _mediator.Send(request, token);

		    return Ok(result);
	    }
	}
}

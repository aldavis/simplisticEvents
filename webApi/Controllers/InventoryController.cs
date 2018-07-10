using System.Threading;
using System.Threading.Tasks;
using application.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webApi.Controllers
{
    [Produces("application/json")]
	[Route("api/[controller]")]
	public class InventoryController : Controller
    {
	    readonly IMediator _mediator;

	    public InventoryController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }


	    [HttpPost("/receive", Name = "receive")]
	    public async Task<IActionResult> Approve([FromBody]ReceiveInventoryRequest request, CancellationToken token)
	    {
		    var result = await _mediator.Send(request, token);

		    return Ok(result);
	    }
	}
}
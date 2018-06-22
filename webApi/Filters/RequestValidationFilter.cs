using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace webApi.Filters
{
    public class RequestValidationFilter:ActionFilterAttribute
    {
	    public override void OnActionExecuting(ActionExecutingContext context)
	    {
		    if (!context.ModelState.IsValid)
		    {
			    context.Result = new BadRequestObjectResult(context.ModelState);
		    }

		    base.OnActionExecuting(context);
	    }
	}
}

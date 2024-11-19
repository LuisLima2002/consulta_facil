
using Microsoft.AspNetCore.Mvc.Filters;

namespace VozAmiga.Api.Utils;

public class FilterAttributer: ActionFilterAttribute
{
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        return next();
    }
}

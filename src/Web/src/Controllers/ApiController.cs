// lang imports
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace VozAmiga.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    public ApiController() : base() {}
    [Inject]
    private IHostEnvironment? HostEnvironment { get; }
    [Inject]
    private ILogger<ApiController>? Logger { get; }

    protected IActionResult InternalServerError(object? response)
    {
        try {
            var logger = Request.HttpContext.RequestServices.GetService<ILoggerFactory>()?.CreateLogger(this.GetType());
            logger?.LogError("Execution resulted in failure: {}", response);
        }
        catch {}
        Logger?.LogError("Execution resulted in failure: {}", response);
        if (HostEnvironment?.IsDevelopment() == true && response is not Exception )
        {
            return new ObjectResult(response ?? "Don't know")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return new ObjectResult("Sorry! My bad, could you try again? Tell me if it continue to happen 😅")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }

}

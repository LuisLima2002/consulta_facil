

using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Api.DTO.ViewModel;
using VozAmiga.Core.Services.Interface.Auth;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Controllers;


public class AuthController : ApiController
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">When login and password are valids</response>
    /// <response code="401">When login and password are invalids</response>

    [HttpPost]
    [ProducesResponseType(typeof(ApiCredentials), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status401Unauthorized, "application/json")]
    public async Task<IActionResult> Login([FromBody] SignInCmd cmd, CancellationToken cancellationToken = default)
    {
        var result = await _service.HandleAsync(cmd, cancellationToken);
        return result.Match(
            Ok,
            e => Unauthorized(e),
            InternalServerError
        );
    }

}

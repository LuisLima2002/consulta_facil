// lib imports
using Microsoft.AspNetCore.Mvc;

// internal imports
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Core.Services.Interface.Profissionals;
using VozAmiga.Api.Services.V1;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Controllers;

public class ProfissionalController : ApiController
{
    private readonly ILogger<ProfissionalController> _logger;
    private readonly ICreateProfissionalService _createProfessionalService;
    private readonly IQueryProfissionalService _queryProfessionalService;
    // [ActivatorUtilitiesConstructor]
    public ProfissionalController(
        ILogger<ProfissionalController> logger,
        ICreateProfissionalService createProfessionalService,
        IQueryProfissionalService queryProfessionalService
    ) : base()
    {
        _logger = logger;
        _createProfessionalService = createProfessionalService;
        _queryProfessionalService = queryProfessionalService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProfissionalCmd professional)
    {
        var result = await _createProfessionalService.HandleAsync(professional);
        return result.Match(
            value => CreatedAtAction(nameof(Get), new { id = value }, value),
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProfissionalQR profissional)
    {
        var result = await _createProfessionalService.UpdateAsync(profissional);
        return result.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCmd changePasswordCmd)
    {
        var result = await _createProfessionalService.ChangePasswordAsync(changePasswordCmd);
        return result.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpPut("resetPassword/{id}")]
    public async Task<IActionResult> ResetPassword(string id)
    {
        var result = await _createProfessionalService.ResetPasswordAsync(id);
        return result.Match(
            Ok,
            _ => NotFound(),
            InternalServerError
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        _logger.LogInformation("Call {id}", id);
        var professional = await _queryProfessionalService.GetProfissionalAsync(id);
        return professional.Match(
            Ok,
            _ => NotFound(),
            InternalServerError
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var res = await _createProfessionalService.RemoveAsync(id);
        return res.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PersonQuery query)
    {
        _logger.LogInformation("Call {query}", query);
        var professional = await _queryProfessionalService.GetProfissionalsAsync(query);
        return professional.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

}

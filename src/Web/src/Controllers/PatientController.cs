// lib imports
using Microsoft.AspNetCore.Mvc;

// internal imports
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Api.Services.V1;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.DTO.ViewModels;

namespace VozAmiga.Api.Controllers;

public class PatientController : ApiController
{
    private readonly ILogger<PatientController> _logger;
    private readonly ICreatePatientService _createPatientService;
    private readonly IQueryPatientService _queryPatientService;

    public PatientController(
        ILogger<PatientController> logger,
        ICreatePatientService createPatientService,
        IQueryPatientService queryPatientService
    ) : base()
    {
        _logger = logger;
        _createPatientService = createPatientService;
        _queryPatientService = queryPatientService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePatientCmd cmd)
    {
        var result = await _createPatientService.HandleAsync(cmd);
        return result.Match(
            value => CreatedAtAction(nameof(Get), new { id = value }, value),
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PatientQR patient)
    {
        var result = await _createPatientService.UpdateAsync(patient);
        return result.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var patient = await _queryPatientService.GetPatientAsync(id);
        return patient.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var res = await _createPatientService.RemoveAsync(id);
        return res.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PersonQuery query)
    {
        _logger.LogInformation("Executing {filter}", query.filter);
        var patient = await _queryPatientService.GetPatientsAsync(query);
        return patient.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }
}

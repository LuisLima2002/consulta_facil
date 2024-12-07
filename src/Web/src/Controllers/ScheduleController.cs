// lib imports
using Microsoft.AspNetCore.Mvc;

// internal imports
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Api.Services.V1;
using VozAmiga.Api.Utils;
using VozAmiga.Core.Services.Interface.Profissionals;
using VozAmiga.Core.Data.Model;

namespace VozAmiga.Api.Controllers;

public class ScheduleController : ApiController
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly ICreateScheduleService _createScheduleService;
    private readonly IQueryScheduleService _queryScheduleService;
    // [ActivatorUtilitiesConstructor]
    public ScheduleController(
        ILogger<ScheduleController> logger,
        ICreateScheduleService createScheduleService,
        IQueryScheduleService queryScheduleService
    ) : base()
    {
        _logger = logger;
        _createScheduleService = createScheduleService;
        _queryScheduleService = queryScheduleService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateScheduleCmd schedule)
    {
        var result = await _createScheduleService.HandleAsync(schedule);
        return result.Match(
            value => CreatedAtAction(nameof(Get), new { id = value }, value),
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ScheduleQR schedule)
    {
        var result = await _createScheduleService.UpdateAsync(schedule);
        return result.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        _logger.LogInformation("Call {id}", id);
        var Schedule = await _queryScheduleService.GetScheduleAsync(id);
        return Schedule.Match(
            Ok,
            _ => NotFound(),
            InternalServerError
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var res = await _createScheduleService.RemoveAsync(id);
        return res.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Query query)
    {
        _logger.LogInformation("Call {query}", query);
        var Schedule = await _queryScheduleService.GetScheduleAsync(query);
        return Schedule.Match(
            Ok,
            e => BadRequest(e.Messages),
            InternalServerError
        );
    }

}

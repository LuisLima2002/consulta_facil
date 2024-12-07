
// internal importsIQueryProfissionalService
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Core.DTO.Query;
using VozAmiga.Api.Utils;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.Data.Model;
using VozAmiga.Api.Utils.Enums;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VozAmiga.Api.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace VozAmiga.Api.Services.V1;

public class QueryScheduleService : IQueryScheduleService
{
    private readonly ILogger<QueryScheduleService> _logger;
    private readonly IScheduleRepository _repository;

    public QueryScheduleService(
        ILogger<QueryScheduleService> logger,
        IScheduleRepository repository
    )
    {
        _logger = logger;
        _repository = repository;
    }
    public async Task<Result<ScheduleQR?>> GetScheduleAsync(string scheduleId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(scheduleId, out var id))
            return new Error("Invalid Id");

        Schedule? schedule = await _repository.FindByIdAsyc(id, cancellationToken);
        if (schedule == null)
            return new ArgumentOutOfRangeException(nameof(scheduleId), "Não foi encontrado nenhum profissional com esse id");
        return ToQR(schedule);

    }

    public async Task<Result<Paginated<ScheduleQR>>> GetScheduleAsync(Query query, CancellationToken cancellationToken = default)
    {
        var found = _repository.FindAsyc(query.Filter ?? "", cancellationToken);

        //if (query.OrderBy != null)
        //{
        //    switch (query.OrderBy)
        //    {
        //        case "name":
        //            found = found.OrderBy(person => person.Name.ToLower()); break;
        //        case "jobposition":
        //            found = found.OrderBy(person => person.JobPosition!.ToLower()); break;
        //        default:
        //            found = found.OrderBy(person => person.Name.ToLower()); break;
        //    }
        //}

        var total = await found.CountAsync(cancellationToken: cancellationToken);

        IEnumerable<ScheduleQR> res = found
            .Skip(query.Page * query.ItemsPerPage)
            .Take(query.ItemsPerPage)
            .Select(ToQR);

        var result = new Paginated<ScheduleQR>(res)
        {
            Page = query.Page,
            ItemsPerPage = query.ItemsPerPage,
            Total = total,
        };

        return result;
    }

    private static ScheduleQR ToQR(Schedule schedule)
    {
        var res = new ScheduleQR
        {
            Id = schedule.Id.ToString(),
            Date = schedule.Date,
            Reason = schedule.Reason,
            ScheduleType = schedule.ScheduleType,
            PatientId = schedule.PatientId.ToString(),
            PatientName = "Luís",
        };

        return res;
    }

}

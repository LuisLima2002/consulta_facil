

using System;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Core.DTO.ViewModels;
using VozAmiga.Api.Infra.Database;
using VozAmiga.Api.Infra.Repositories;
using VozAmiga.Core.Services.Interface.Auth;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Core.Services.Interface.Profissionals;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.Services.V1;


public class CreateScheduleService : ICreateScheduleService
{
    private readonly IScheduleRepository _repository;
    private readonly IDbContext _context;
    private readonly IUnityOfWork _unityOfWork;

    public CreateScheduleService(
        IScheduleRepository repository,
        IDbContext context,
        IUnityOfWork unityOfWork
    )
    {
        _repository = repository;
        _context = context;
        _unityOfWork = unityOfWork;
    }

    /// <inheritdoc />
    public async Task<Result<string>> HandleAsync(CreateScheduleCmd cmd, CancellationToken cancellationToken = default)
    {
        //Find patient
        //
        //
        Guid patientId = Guid.NewGuid();

        var newSchedule = new Schedule { Id = Guid.NewGuid(),
        PatientId = patientId,
        PatientName = "Luís",
        Date = cmd.Date,
        ScheduleType= cmd.ScheduleType,
        Reason= cmd.Reason};

        await _repository.AddAsync(newSchedule, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newSchedule.Id.ToString();
    }

    public async Task<Result> UpdateAsync(ScheduleQR scheduleQR, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(scheduleQR.Id, out var id))
            {
                var schedule = await _repository.FindByIdAsyc(id);
                schedule!.Date = scheduleQR.Date;
                schedule!.ScheduleType = scheduleQR.ScheduleType;
                schedule!.Reason = scheduleQR.Reason;
                _repository.Update(schedule);
                await _unityOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            return new Error("Invalid Id");
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
    public async Task<Result> RemoveAsync(string scheduleId, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(scheduleId, out var id))
            {
                await _repository.DeleteByIdAsync(id, cancellationToken);
                await _unityOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            return new Error("Invalid Id");
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

}

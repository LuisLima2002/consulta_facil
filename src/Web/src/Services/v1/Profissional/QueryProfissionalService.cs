
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

public class QueryProfissionalService : IQueryProfissionalService
{
    private readonly ILogger<QueryProfissionalService> _logger;
    private readonly IProfissionalRepository _profissionalRepository;

    public QueryProfissionalService(
        ILogger<QueryProfissionalService> logger,
        IProfissionalRepository profissionalRepository
    )
    {
        _logger = logger;
        _profissionalRepository = profissionalRepository;
    }
    public async Task<Result<ProfissionalQR?>> GetProfissionalAsync(string profissionalId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(profissionalId, out var id))
            return new Error("Invalid Id");

        Profissional? profissional = await _profissionalRepository.FindByIdAsyc(id, cancellationToken);
        if (profissional == null)
            return new ArgumentOutOfRangeException(nameof(profissionalId), "Não foi encontrado nenhum profissional com esse id");
        return ToQR(profissional);

    }

    public async Task<Result<Paginated<ProfissionalQR>>> GetProfissionalsAsync(PersonQuery query, CancellationToken cancellationToken = default)
    {
        var found = _profissionalRepository.FindAsyc(query.filter ?? "", cancellationToken);

        if (query.orderBy != null)
        {
            switch (query.orderBy)
            {
                case "name":
                    found = found.OrderBy(person => person.Name.ToLower()); break;
                case "jobposition":
                    found = found.OrderBy(person => person.JobPosition!.ToLower()); break;
                default:
                    found = found.OrderBy(person => person.Name.ToLower()); break;
            }
        }

        var total = await found.CountAsync(cancellationToken: cancellationToken);

        IEnumerable<ProfissionalQR> res = found
            .Skip(query.page * query.itensPerpage)
            .Take(query.itensPerpage)
            .Select(ToQR);

        var result = new Paginated<ProfissionalQR>(res)
        {
            Page = query.page,
            ItemsPerPage = query.itensPerpage,
            Total = total,
        };

        return result;
    }

    private static ProfissionalQR ToQR(Profissional profissional)
    {
        var res = new ProfissionalQR
        {
            Id = profissional.Id.ToString(),
            Name = profissional.Name,
            UserName = profissional.UserName,
            JobPosition = profissional.JobPosition,
            Permission = profissional.Permission
        };

        return res;
    }

}

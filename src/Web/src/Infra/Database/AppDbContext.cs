
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Model;
using VozAmiga.Core.Data.Repositories;

namespace VozAmiga.Api.Infra.Database;

public class AppDbContext : DbContext, IDbContext
{
    private const string DB_NAME = "database.sqlite";

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DB_NAME}");
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfiguration(new ProfissionalEntityMap())
            .ApplyConfiguration(new PatientEntityMap())
            .ApplyConfiguration(new ScheduleEntityMap());

    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }

    public DbSet<Profissional> Profissionals { get; set;}
    public DbSet<Patient> Patients { get; set;}
    public DbSet<Schedule> Schedules { get; set; }
}

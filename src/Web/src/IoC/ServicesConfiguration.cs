
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VozAmiga.Api.Data.Database;
using VozAmiga.Core.Data.Repositories;
using VozAmiga.Api.Data.Validators;
using VozAmiga.Core.DTO.Commands;
using VozAmiga.Api.Infra.Database;
using VozAmiga.Api.Infra.Repositories;
using VozAmiga.Core.Services.Interface.Auth;
using VozAmiga.Core.Services.Interface.Patient;
using VozAmiga.Core.Services.Interface.Profissionals;
using VozAmiga.Api.Services.V1;
using VozAmiga.Api.Services.V1.Auth;
using VozAmiga.Api.Utils;

namespace VozAmiga.Api.IoC;



public static class ServicesConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<AppConfiguration>(configuration.GetSection("AppConfiguration"));

        services.AddEndpointsApiExplorer();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new ExceptionConverter());
        });

        services.AddSignalR();
        services.AddRouting(c => c.LowercaseUrls = true);

        services.AddSwaggerGen();
        //services.AddSwaggerGen(opts =>
        //{
        //    opts.SwaggerDoc("v1", new OpenApiInfo
        //    {
        //        Title = "Voz Amiga",
        //        Version = "v1.0.0",
        //        Description = "A Web API to aid speech development",
        //        Contact = new OpenApiContact
        //        {
        //            Name = "Example Contact",
        //            Email = "otavionegrizolli@hotmail.com",
        //        },
        //        License = new OpenApiLicense
        //        {
        //            Name = "MIT",
        //            Url = new Uri("https://opensource.org/license/mit")
        //        }
        //    });

        //    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //    {
        //        Name = "Authorization",
        //        In = ParameterLocation.Header,
        //        Type = SecuritySchemeType.ApiKey,
        //        Scheme = "Bearer",
        //    });

        //    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        {
        //            new OpenApiSecurityScheme
        //            {
        //                Reference = new OpenApiReference
        //                {
        //                    Type = ReferenceType.SecurityScheme,
        //                    Id = "Bearer",
        //                },
        //                Scheme = "oauth2",
        //                Name ="Bearer",
        //                In = ParameterLocation.Header
        //            },
        //            new List<string>()
        //        }
        //    });


        //    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        //});

        services.AddApplicationRepositories();
        services.AddApplicationServices();
        services.AddApplicationAuth(configuration);
        services.AddValidationProviders();

        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreatePatientService, CreatePatientService>();
        services.AddScoped<ICreateProfissionalService, CreateProfissionalService>();
        services.AddScoped<ICreateScheduleService, CreateScheduleService>();
        services.AddScoped<IQueryPatientService, QueryPatientService>();
        services.AddScoped<IQueryProfissionalService, QueryProfissionalService>();
        services.AddScoped<IQueryScheduleService, QueryScheduleService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IAuthService, AuthService>();
        // TODO: criar interface, se sobrar tempo
        return services;
    }

    private static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddDbContext<IDbContext, AppDbContext>(d =>
            {
                d.UseSqlite();
            });

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        return services;
    }


    private static IServiceCollection AddApplicationAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var tmp = configuration.GetValue<string>("Keys:PrivateRSA") ?? "sndfgsdfjkgskdfgjksdfgsdkjfgkjsdjkfgjdfgjk";
        var privateKey = Encoding.ASCII.GetBytes(tmp);

        services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opts =>
        {
            opts.RequireHttpsMetadata = false;
            opts.SaveToken = true;
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }

    private static IServiceCollection AddValidationProviders(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreatePatientCmd>, CreatePatientValidator>();
        return services;
    }
}


public class ExceptionConverter : JsonConverter<Exception>
{
    public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Reading exceptions is not supported.
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Message", value.Message);
        writer.WriteString("StackTrace", value.StackTrace);
        writer.WriteString("Source", value.Source);
        // Exclude TargetSite and other non-serializable properties
        writer.WriteEndObject();
    }
}

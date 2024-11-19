using Microsoft.Extensions.FileProviders;
using VozAmiga.Api.IoC;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Logging.AddConsole();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwagger();
    //app.UseSwaggerUI(opts =>
    //{
    //    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //    opts.RoutePrefix = "";
    //});
}

// app.UseHttpsRedirection();
app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();
app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Media")),
        RequestPath = "/activity/{activityId}/media"
    });
await app.RunAsync();

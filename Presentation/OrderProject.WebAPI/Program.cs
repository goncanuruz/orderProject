using OrderProject.Application;
using OrderProject.Application.Validators.Products;
using OrderProject.Infrastructure.Filters;
using OrderProject.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OrderProject.Application.Middlewares;
using OrderProject.Infrastructure;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration["Seq:Url"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
//fluent validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<SerilogMiddleware>();

app.Run();

using OrderProject.Application;
using OrderProject.Application.Validators.Products;
using OrderProject.Infrastructure.Filters;
using OrderProject.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

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

//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//    policy
//    .AllowAnyOrigin()// Her kaynaða izin ver
//     //.WithOrigins("AllowOrigin")
//    .AllowAnyHeader()// Her türlü HTTP baþlýðýna izin ver
//    .AllowAnyMethod()// Her türlü HTTP metoduna izin ver
//    .AllowCredentials()// Her türlü HTTP metoduna izin ver
//));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

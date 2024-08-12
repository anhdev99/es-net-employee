using System.Reflection;
using EmployeesESNET.Configuraitons;
using EmployeesESNET.Data;
using EmployeesESNET.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DataContext")));
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ESService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddESService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

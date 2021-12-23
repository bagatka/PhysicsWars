using System.Reflection;
using MediatR;
using PhysicsWars.Application;
using PhysicsWars.Application.Configuration.Options;
using PhysicsWars.Infrastructure;
using PhysicsWars.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add options
builder.Services.Configure<EmailNotificationsOptions>(
    builder.Configuration.GetSection(EmailNotificationsOptions.SectionName)
);
builder.Services.Configure<RegistrationOptions>(
    builder.Configuration.GetSection(RegistrationOptions.SectionName)
);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add(new HttpGlobalExceptionFilter());
    }
);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRequestLocalization();

app.Run();
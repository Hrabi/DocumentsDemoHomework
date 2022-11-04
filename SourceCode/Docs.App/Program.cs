using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(Docs.Presentation.AssemblyReference.Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Scrutor - https://github.com/khellang/Scrutor
//builder.Services.Scan(selector => selector.FromAssemblies(Docs.Application.AssemblyReference.Assembly, Docs.Domain.AssemblyReference.Assembly)
builder.Services.Scan(selector => selector.FromAssemblies(Docs.Domain.AssemblyReference.Assembly)
  .AddClasses(false)
  .AsImplementedInterfaces()
  .WithScopedLifetime());


builder.Services.AddMediatR(Docs.Application.AssemblyReference.Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

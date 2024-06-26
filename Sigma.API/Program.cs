using Sigma.API.Configurations;
using Sigma.Repository.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(CandidateMapper));
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

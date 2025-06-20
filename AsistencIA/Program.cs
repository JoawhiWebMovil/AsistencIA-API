using Microsoft.EntityFrameworkCore;
using AsistencIA_DOMAIN.Data;
using AsistencIA_DOMAIN.Core.Concrete;
using AsistencIA_DOMAIN.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
var cnx = _config.GetConnectionString("DevConnection");
builder.Services
    .AddDbContext<DbAsistencIaDbContext>
    (options => options.UseSqlServer(cnx));

// Add services to the container.
builder.Services.AddTransient<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddTransient<IUsuariosService, UsuariosService>();
builder.Services.AddTransient<IJWTService, JWTService>();

builder.Services.AddSharedInfrastructure(_config);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin()   // Permite cualquier origen
               .AllowAnyMethod()   // Permite cualquier método HTTP (GET, POST, etc.)
               .AllowAnyHeader()); // Permite cualquier cabecera
});

builder.Services.AddControllers();
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

// Aplicar CORS
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

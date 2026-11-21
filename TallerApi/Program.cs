using Microsoft.EntityFrameworkCore;
using TallerApi.Data;
using TallerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext con SQL Server
// Configurar DbContext con MySQL (Pomelo)
builder.Services.AddDbContext<TallerDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
// Registrar Servicios (Inyección de Dependencias)
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaServicioService, CategoriaServicioService>();

// Configurar CORS para Vue.js
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirVue",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors("PermitirVue");

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using ChessLib.Infrastructure.Persistence;
using ChessLib.Application.Interfaces;
using ChessLib.Infrastructure.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

// 1. Минимум для работы контроллеров и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Стандартная настройка без доп. параметров

// 2. CORS (разрешаем всё для локальной разработки)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3. Инфраструктура (БД и Репозитории)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IOpeningRepository, OpeningRepository>();
builder.Services.AddScoped<IStatisticService, StatisticsService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



var app = builder.Build();

// 4. Включаем Swagger только в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
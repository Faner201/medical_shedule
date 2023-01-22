using Microsoft.EntityFrameworkCore;

using Entity;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ServiceContext>(options =>
options.UseNpgsql($"Host=localhost;Port=5432;Database=medical_base;Username=postgres;Password=Bkmz2309865"));

builder.Services.AddTransient<IUserRepository, UserModelService>();
builder.Services.AddTransient<UserService>();

builder.Services.AddTransient<IDoctorRepository, DoctorModelService>();
builder.Services.AddTransient<DoctorService>();

builder.Services.AddTransient<IReceptionRepository, ReceptionModelService>();
builder.Services.AddTransient<ReceptionService>();

builder.Services.AddTransient<IScheduleRepository, ScheduleModelService>();
builder.Services.AddTransient<ScheduleService>();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

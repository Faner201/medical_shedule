using Microsoft.EntityFrameworkCore;

using Entity;

namespace Database;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> User { get; set; }

    public DbSet<DoctorModel> Doctor { get; set; }

    public DbSet<ReceptionModel> Reception { get; set; }

    public DbSet<SpecializationModel> Specialization { get; set; }

    public DbSet<ScheduleModel> Schedule {get; set; }

    public DbSet<AccountRoleModel> AccountRole {get; set; }

    public ApplicationContext() 
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlServer("Host=localhost;Port=5432;Database=medical_base;Username=postgres;Password=");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Database;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> User { get; set; }

    public DbSet<DoctorModel> Doctor { get; set; }

    public DbSet<ReceptionModel> Reception { get; set; }

    public DbSet<SpecializationModel> Specialization { get; set; }

    public DbSet<ScheduleModel> Schedule {get; set; }

    public DbSet<AccountRoleModel> AccountRole {get; set; }

    private string ConfigurationJSON()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsetings.json");

        var config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");
        
        return connectionString;
    } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseNpgsql(ConfigurationJSON()).Options;
    }
}
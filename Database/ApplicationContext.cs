using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> User { get; set; }

    public DbSet<DoctorModel> Doctor { get; set; }

    public DbSet<ReceptionModel> Reception { get; set; }

    public DbSet<SpecializationModel> Specialization { get; set; }

    public DbSet<ScheduleModel> Schedule {get; set; }

    public DbSet<AccountRoleModel> AccountRole {get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsetings.json");
        IConfigurationRoot config = builder.Build();

        string connection = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connection);
    }
}
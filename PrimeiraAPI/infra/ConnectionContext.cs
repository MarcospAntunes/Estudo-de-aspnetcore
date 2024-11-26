using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Infra {
  public class ConnectionContext: DbContext {
    public DbSet<Colaboradores> Colaboradores { get; set; }
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseMySql(
            "Server=localhost;" +
            "Port=3306;Database=cadastro_de_terceiros;" +
            "User=root;" +
            "Password=",
            new MySqlServerVersion(new Version(9, 1, 0))
          );
  }
}
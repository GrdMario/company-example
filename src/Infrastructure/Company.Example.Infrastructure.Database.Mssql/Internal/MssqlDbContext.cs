namespace Company.Example.Infrastructure.Database.Mssql.Internal
{
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    internal sealed class MssqlDbContext : DbContext
    {
        public MssqlDbContext(DbContextOptions<MssqlDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            optionsBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

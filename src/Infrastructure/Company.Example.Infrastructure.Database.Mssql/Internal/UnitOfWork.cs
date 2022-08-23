namespace Company.Example.Infrastructure.Database.Mssql.Internal
{
    using Company.Example.Application.Contracts.Database;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly MssqlDbContext context;

        public UnitOfWork(ICarRepository cars, MssqlDbContext context)
        {
            this.context = context;
            this.Cars = cars;
        }

        public ICarRepository Cars { get; }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}

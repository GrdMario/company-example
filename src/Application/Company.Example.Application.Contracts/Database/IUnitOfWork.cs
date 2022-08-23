namespace Company.Example.Application.Contracts.Database
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);

        ICarRepository Cars { get; }
    }
}

namespace Company.Example.Application.Contracts.Database
{
    using Company.Example.Application.Contracts.Database.Models;
    using Company.Example.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarRepository
    {
        void Create(Car car);

        void Update(Car car);

        void Delete(Car car);

        Task<Car> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Car>> GetAsync(CarFilter filter, CancellationToken cancellationToken);
    }
}

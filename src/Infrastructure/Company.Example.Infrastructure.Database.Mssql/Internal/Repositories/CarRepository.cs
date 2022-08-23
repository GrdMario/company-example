namespace Company.Example.Infrastructure.Database.Mssql.Internal.Repositories
{
    using Company.Example.Application.Contracts.Database;
    using Company.Example.Application.Contracts.Database.Models;
    using Company.Example.Domain;
    using Company.Example.Infrastructure.Database.Mssql.Internal.Extensions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CarRepository : ICarRepository
    {
        private readonly DbSet<Car> cars;

        public CarRepository(MssqlDbContext context)
        {
            this.cars = context.Set<Car>();
        }

        public void Create(Car car)
        {
            this.cars.Add(car);
        }

        public void Delete(Car car)
        {
            this.cars.Remove(car);
        }

        public async Task<List<Car>> GetAsync(CarFilter filter, CancellationToken cancellationToken)
        {
            return await this.cars
                .WhereIf(filter.Name is not null, p => p.Name.StartsWith(filter.Name!))
                .WhereIf(filter.Make is not null, p => p.Make.StartsWith(filter.Make!))
                .Skip(filter.Skip)
                .Take(filter.Take)
                .ToListAsync(cancellationToken);
        }

        public async Task<Car> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await this.cars.FindAsync(new object[] { id }, cancellationToken) ?? throw new ApplicationException("Unable to find car.");
        }

        public void Update(Car car)
        {
            this.cars.Update(car);
        }
    }
}

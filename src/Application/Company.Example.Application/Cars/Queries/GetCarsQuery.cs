namespace Company.Example.Application.Cars.Queries
{
    using AutoMapper;
    using Company.Example.Application.Cars.Common;
    using Company.Example.Application.Contracts.Database;
    using Company.Example.Application.Contracts.Database.Models;
    using Company.Example.Domain;
    using FluentValidation;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCarsQuery : IRequest<List<CarResponse>>
    {
        public string? Name { get; set; }

        public string? Make { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }

    internal sealed class GetCarsQueryValidator : AbstractValidator<GetCarsQuery>
    {
        public GetCarsQueryValidator()
        {
            this.RuleFor(c => c.Skip)
                .GreaterThanOrEqualTo(0);

            this.RuleFor(c => c.Take)
                .GreaterThan(0)
                .LessThanOrEqualTo(50);
        }
    }

    internal sealed class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<CarResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCarsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<CarResponse>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            CarFilter filter = new (
                request.Skip,
                request.Take,
                request.Name,
                request.Make);

            List<Car> cars = await this.unitOfWork.Cars.GetAsync(filter, cancellationToken);

            return this.mapper.Map<List<CarResponse>>(cars);
        }
    }
}

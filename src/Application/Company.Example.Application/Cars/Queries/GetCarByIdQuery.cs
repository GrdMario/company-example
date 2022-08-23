namespace Company.Example.Application.Cars.Queries
{
    using AutoMapper;
    using Company.Example.Application.Cars.Common;
    using Company.Example.Application.Contracts.Database;
    using Company.Example.Domain;
    using FluentValidation;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record GetCarByIdQuery(Guid Id) : IRequest<CarResponse>;

    internal sealed class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
    {
        public GetCarByIdQueryValidator()
        {
            this.RuleFor(query => query.Id)
                .NotEmpty();
        }
    }

    internal sealed class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCarByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CarResponse> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {

            Car car = await this.unitOfWork.Cars.GetByIdAsync(request.Id, cancellationToken);

            return this.mapper.Map<CarResponse>(car);
        }
    }
}

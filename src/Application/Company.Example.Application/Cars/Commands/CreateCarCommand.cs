namespace Company.Example.Application.Cars.Commands
{
    using Company.Example.Application.Contracts.Database;
    using Company.Example.Domain;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateCarCommand : IRequest
    {
        public string Name { get; set; } = default!;

        public string Make { get; set; } = default!;

        public int Weight { get; set; }

        public int MaxSpeed { get; set; }
    }

    internal sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            this.RuleFor(c => c.Name).NotEmpty();
            this.RuleFor(c => c.Make).NotEmpty();
            this.RuleFor(c => c.Weight).GreaterThan(0);
            this.RuleFor(c => c.MaxSpeed).GreaterThan(0);
        }
    }

    internal sealed class CreateCarCommandHandler : AsyncRequestHandler<CreateCarCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateCarCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = new (
                Guid.NewGuid(),
                request.Name,
                request.Make,
                request.Weight,
                request.MaxSpeed);

            this.unitOfWork.Cars.Create(car);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

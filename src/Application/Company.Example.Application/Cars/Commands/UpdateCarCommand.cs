namespace Company.Example.Application.Cars.Commands
{
    using Company.Example.Application.Contracts.Database;
    using Company.Example.Domain;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateCarCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Make { get; set; } = default!;

        public int Weight { get; set; }

        public int MaxSpeed { get; set; }
    }

    internal sealed class UpdateCarCommandValidator: AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            this.RuleFor(c => c.Id).NotEmpty();
            this.RuleFor(c => c.Name).NotEmpty();
            this.RuleFor(c => c.Make).NotEmpty();
            this.RuleFor(c => c.Weight).GreaterThan(0);
            this.RuleFor(c => c.MaxSpeed).GreaterThan(0);
        }
    }

    internal sealed class UpdateCarCommandHandler : AsyncRequestHandler<UpdateCarCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateCarCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = await this.unitOfWork.Cars.GetByIdAsync(request.Id, cancellationToken);

            car.Update(request.Name, request.Make, request.Weight, request.MaxSpeed);

            this.unitOfWork.Cars.Update(car);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

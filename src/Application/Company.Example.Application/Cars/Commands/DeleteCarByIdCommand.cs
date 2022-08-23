namespace Company.Example.Application.Cars.Commands
{
    using Company.Example.Application.Contracts.Database;
    using Company.Example.Domain;
    using FluentValidation;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteCarByIdCommand : IRequest
    {
        public DeleteCarByIdCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }

    internal sealed class DeleteCarByIdCommandValidator : AbstractValidator<DeleteCarByIdCommand>
    {
        public DeleteCarByIdCommandValidator()
        {
            this.RuleFor(c => c.Id).NotEmpty();
        }
    }

    internal sealed class DeleteCarByIdCommandHandler : AsyncRequestHandler<DeleteCarByIdCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCarByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteCarByIdCommand request, CancellationToken cancellationToken)
        {
            Car car = await this.unitOfWork.Cars.GetByIdAsync(request.Id, cancellationToken);

            this.unitOfWork.Cars.Delete(car);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

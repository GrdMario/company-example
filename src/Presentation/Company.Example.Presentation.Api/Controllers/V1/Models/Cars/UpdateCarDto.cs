namespace Company.Example.Presentation.Api.Controllers.V1.Models.Cars
{
    using System;

    public record UpdateCarDto(
        Guid Id,
        string Name,
        string Make,
        int Weight,
        int MaxSpeed);
}

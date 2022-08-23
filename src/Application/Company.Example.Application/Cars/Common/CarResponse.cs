namespace Company.Example.Application.Cars.Common
{
    using System;

    public record CarResponse(
        Guid Id,
        string Name,
        string Make,
        int Weight,
        int MaxSpeed);
}

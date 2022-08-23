namespace Company.Example.Presentation.Api.Controllers.V1.Models.Cars
{
    public record CreateCarDto(
        string Name,
        string Make,
        int Weight,
        int MaxSpeed);
}

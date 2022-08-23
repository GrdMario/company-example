namespace Company.Example.Presentation.Api.Controllers.V1.Models.Cars
{
    public record GetCarsQueryDto(
        string? Name,
        int Skip = 0,
        int Take = 20);
}

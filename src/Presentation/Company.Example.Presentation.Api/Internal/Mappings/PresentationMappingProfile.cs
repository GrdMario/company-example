namespace Company.Example.Presentation.Api.Internal.Mappings
{
    using AutoMapper;
    using Company.Example.Application.Cars.Commands;
    using Company.Example.Application.Cars.Queries;
    using Company.Example.Presentation.Api.Controllers.V1.Models.Cars;

    internal sealed class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            this.CreateMap<CreateCarDto, CreateCarCommand>();
            this.CreateMap<UpdateCarDto, UpdateCarCommand>();
            this.CreateMap<GetCarsQueryDto, GetCarsQuery>();
        }
    }
}

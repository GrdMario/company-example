namespace Company.Example.Application.Internal.Mappings
{
    using AutoMapper;
    using Company.Example.Application.Cars.Common;
    using Company.Example.Domain;

    internal sealed class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            this.CreateMap<Car, CarResponse>();
        }
    }
}

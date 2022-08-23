namespace Company.Example.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;

    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(
            IMediator mediator,
            IMapper mapper)
        {
            this.Mediator = mediator;
            this.Mapper = mapper;
        }
        protected IMediator Mediator { get; }

        protected IMapper Mapper { get; }
    }
}

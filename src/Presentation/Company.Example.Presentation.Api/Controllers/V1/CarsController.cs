namespace Company.Example.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using Company.Example.Application.Cars.Commands;
    using Company.Example.Application.Cars.Common;
    using Company.Example.Application.Cars.Queries;
    using Company.Example.Presentation.Api.Controllers.V1.Models.Cars;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : ApiControllerBase
    {
        public CarsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets cars.
        /// </summary>
        /// <param name="query">Model for quering cars.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<CarResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetCarsQueryDto request)
        {
            var query = this.Mapper.Map<GetCarsQuery>(request);

            var response = await this.Mediator.Send(query);

            return this.Ok(response);
        }

        /// <summary>
        /// Creates car.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateCarDto request)
        {
            var command = this.Mapper.Map<CreateCarCommand>(request);

            await this.Mediator.Send(command);

            return this.NoContent();
        }

        /// <summary>
        /// Updates car.
        /// </summary>
        [HttpPut("{id}/fuzBiz")]
        [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCarDto request)
        {
            var command = this.Mapper.Map<UpdateCarCommand>(request, opt => opt.AfterMap((_, command) =>
            {
                command.Id = id;
            }));

            await this.Mediator.Send(command);

            return this.NoContent();
        }

        /// <summary>
        /// Gets car by id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await this.Mediator.Send(new GetCarByIdQuery(id));

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes car by id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await this.Mediator.Send(new DeleteCarByIdCommand(id));

            return this.NoContent();
        }
    }
}

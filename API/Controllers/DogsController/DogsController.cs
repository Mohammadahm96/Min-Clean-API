using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDogs;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
            //return Ok("GET ALL DOGS");
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
        }

        // Create a new dog
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            if (newDog == null)
            {
                return BadRequest("newDog field is required");
            }
            return Ok(await _mediator.Send(new AddDogCommand(newDog)));
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            return Ok(await _mediator.Send(new UpdateDogByIdCommand(updatedDog, updatedDogId)));
        }

        // Delete a dog
        [HttpDelete]
        [Route("deleteDog/{dogId}")]
        public async Task<IActionResult> DeleteDogById(Guid dogId)
        {
            var result = await _mediator.Send(new DeleteDogCommand { DogId = dogId });

            if (result.IsSuccess)
            {
                return Ok("Dog deleted successfully.");
            }

            return NotFound("Dog not found or deletion failed.");
        }
    }
}

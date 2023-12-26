using Application.Commands.Birds;
using Application.Commands.Birds.UpdateBird;
using Application.Commands.Birds.DeleteBirds;
using Application.Dtos;
using Application.Queries.Birds.GetAllBirds;
using Application.Queries.Birds.GetBirdById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public BirdsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all birds from the database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
        }

        // Get a bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }

        // Create a new bird
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird, [FromQuery] Guid userId)
        {
            if (newBird == null)
            {
                return BadRequest("newBird field is required");
            }

            var command = new AddBirdCommand(newBird, userId);
            return Ok(await _mediator.Send(command));
        }


        // Update a specific bird
        [HttpPut]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            if (updatedBird == null)
            {
                return BadRequest("the updatedBird field is required");
            }

            return Ok(await _mediator.Send(new UpdateBirdCommand(updatedBird, updatedBirdId)));
        }

        // Delete a bird
        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        public async Task<IActionResult> DeleteBirdById(Guid birdId)
        {
            var deleteCommand = new DeleteBirdCommand { BirdId = birdId };

            var result = await _mediator.Send(deleteCommand);

            if (result.IsSuccess)
            {
                return Ok("Bird deleted successfully.");
            }

            return NotFound("Bird not found or deletion failed.");
        }
    }
}


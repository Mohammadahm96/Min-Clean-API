using Application.Commands.Birds;
using Application.Commands.Birds.UpdateBird;
using Application.Commands.Birds.DeleteBirds;
using Application.Dtos;
using Application.Queries.Birds.GetAllBirds;
using Application.Queries.Birds.GetBirdById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Validators.Bird;
using Application.Queries.Dogs.GetAll;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly BirdValidator _birdValidator;

        public BirdsController(IMediator mediator, BirdValidator birdValidator)
        {
            _mediator = mediator;
            _birdValidator = birdValidator;
        }

        // Get all birds from the database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            {
                try
                {
                    // Försök hämta alla hundar från databasen
                    var birds = await _mediator.Send(new GetAllBirdsQuery());

                    // Returnera hundarna om hämtningen lyckades
                    return Ok(birds);
                }
                catch (Exception ex)
                {
                    // Om det uppstår ett fel, returnera ett felmeddelande
                    return BadRequest($"Error getting birds: {ex.Message}");
                }
            }
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
            // Validate Bird
            var validatedBird = _birdValidator.Validate(newBird);

            // Error handling
            if (!validatedBird.IsValid)
            {
                return BadRequest(validatedBird.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                // Add Bird with UserId
                return Ok(await _mediator.Send(new AddBirdCommand(newBird, userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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


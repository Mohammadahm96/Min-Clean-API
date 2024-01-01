using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDogs;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using Application.Validators.Dog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly DogValidator _dogValidator;
        public DogsController(IMediator mediator, DogValidator dogValidator)
        {
            _mediator = mediator;
            _dogValidator = dogValidator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            try
            {
                // Försök hämta alla hundar från databasen
                var dogs = await _mediator.Send(new GetAllDogsQuery());

                // Returnera hundarna om hämtningen lyckades
                return Ok(dogs);
            }
            catch (Exception ex)
            {
                // Om det uppstår ett fel, returnera ett felmeddelande
                return BadRequest($"Error getting dogs: {ex.Message}");
            }
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
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog, [FromQuery] Guid userId)
        {
            // Validate Dog
            var validatedDog = _dogValidator.Validate(newDog);

            // Error handling
            if (!validatedDog.IsValid)
            {
                return BadRequest(validatedDog.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                // Add Dog with UserId
                return Ok(await _mediator.Send(new AddDogCommand(newDog, userId)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{dogId}")]
        public async Task<IActionResult> UpdateDog(Guid dogId, [FromBody] DogDto updatedDog)
        {
            // Validate Dog ID
            var validatedDogId = new GuidValidator().Validate(dogId);
            if (!validatedDogId.IsValid)
            {
                return BadRequest(validatedDogId.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            // Validate Updated Dog
            var validatedUpdatedDog = _dogValidator.Validate(updatedDog);
            if (!validatedUpdatedDog.IsValid)
            {
                return BadRequest(validatedUpdatedDog.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            // Try Catch
            try
            {
                var command = new UpdateDogByIdCommand(dogId, updatedDog);
                var result = await _mediator.Send(command);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Dog not found.");
                }
            }
            catch (Exception ex)
            {
                // Här kan du använda ditt eget felmeddelande för exceptions om du vill
                return BadRequest($"Error updating dog: {ex.Message}");
            }
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

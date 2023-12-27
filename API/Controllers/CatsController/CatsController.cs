using Application.Commands.Cats;
using Application.Commands.Cats.DeleteCats;
using Application.Commands.Cats.UpdateCats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetCatById;
using Application.Queries.Dogs.GetAll;
using Application.Validators.Cat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CatValidator _catValidator;

        public CatsController(IMediator mediator, CatValidator catValidator)
        {
            _mediator = mediator;
            _catValidator = catValidator;
        }

        // Get all cats from the database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            {
                try
                {
                    var cats = await _mediator.Send(new GetAllCatsQuery());

                    return Ok(cats);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error getting cats: {ex.Message}");
                }
            }
        }
       

        // Get a cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        // Create a new cat
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat, [FromQuery] Guid userId)
        {
            // Validate Cat
            var validatedCat = _catValidator.Validate(newCat);

            // Error handling
            if (!validatedCat.IsValid)
            {
                return BadRequest(validatedCat.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                // Add Cat with UserId
                return Ok(await _mediator.Send(new AddCatCommand(newCat, userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Update a specific cat

        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            if (updatedCat == null)
            {
                return BadRequest("The updatedCat field is required.");
            }

            return Ok(await _mediator.Send(new UpdateCatCommand(updatedCat, updatedCatId)));
        }

        // Delete a specific cat by Id
        [HttpDelete]
        [Route("deleteCat/{catId}")]
        public async Task<IActionResult> DeleteCat(Guid catId)
        {
            var result = await _mediator.Send(new DeleteCatCommand { CatId = catId });

            if (result.IsSuccess)
            {
                return Ok("Cat deleted successfully.");
            }

            return NotFound("Cat not found or deletion failed.");
        }
    }
}

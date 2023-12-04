using Application.Commands.Cats;
using Application.Commands.Cats.DeleteCats;
using Application.Commands.Cats.UpdateCats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetCatById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all cats from the database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
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
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            if (newCat == null)
            {
                return BadRequest("The newCat field is required");
            }
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));
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

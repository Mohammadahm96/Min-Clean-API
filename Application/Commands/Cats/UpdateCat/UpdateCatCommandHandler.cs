using Application.Commands.Cats.UpdateCats;
using Application.Exceptions;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class UpdateCatCommandHandler : IRequestHandler<UpdateCatCommand, Cat>
{
    private readonly ICatRepository _catRepository;
    private readonly ILogger<UpdateCatCommandHandler> _logger;

    public UpdateCatCommandHandler(ICatRepository catRepository, ILogger<UpdateCatCommandHandler> logger)
    {
        _catRepository = catRepository;
        _logger = logger;
    }

    public async Task<Cat> Handle(UpdateCatCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var catToUpdate = await _catRepository.GetCatById(request.Id);

            if (catToUpdate == null)
            {
                // Cat not found
                throw new CatNotFoundException(request.Id);
            }

            // Update cat properties
            catToUpdate.Name = request.UpdatedCat.Name;
            catToUpdate.Breed = request.UpdatedCat.Breed;
            catToUpdate.Weight = request.UpdatedCat.Weight;

            await _catRepository.UpdateCat(catToUpdate);

            return catToUpdate;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cat");
            throw new CatUpdateException("Error updating cat.", ex);
        }
    }
}

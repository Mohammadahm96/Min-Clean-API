using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cats
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly ICatRepository _catRepository;
        private readonly ILogger<AddCatCommandHandler> _logger;

        public AddCatCommandHandler(ICatRepository catRepository, ILogger<AddCatCommandHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newCat = new Cat
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewCat.Name,
                    LikesToPlay = request.NewCat.LikesToPlay,
                    Breed = request.NewCat.Breed,
                    Weight = request.NewCat.Weight
                };

                await _catRepository.AddCat(newCat, request.UserId);

                return newCat;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding cat");
                throw new CatCreationException("Error adding cat.", ex);
            }
        }
    }
}
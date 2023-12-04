using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdCommand : IRequest<Bird>
    {
        public UpdateBirdCommand(BirdDto updatedBird, Guid id)
        {
            UpdatedBird = updatedBird;
            Id = id;
        }
        public Guid Id { get; }
        public BirdDto UpdatedBird { get; }
    }
}


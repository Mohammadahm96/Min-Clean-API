using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Birds
{
    public class AddBirdCommand : IRequest<Bird>
    {
        public BirdDto NewBird { get; }
        public Guid UserId { get; internal set; }

        public AddBirdCommand(BirdDto newBird)
        {
            NewBird = newBird;
        }
    }
}


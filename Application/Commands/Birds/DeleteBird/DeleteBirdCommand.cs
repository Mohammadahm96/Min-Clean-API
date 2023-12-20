using MediatR;

namespace Application.Commands.Birds.DeleteBirds
{
    public class DeleteBirdCommand : IRequest<DeleteBirdResult>
    {
        public Guid BirdId { get; set; }
        public Guid UserId { get; internal set; }
    }
}


using MediatR;

namespace Application.Commands.Dogs.DeleteDogs;
public class DeleteDogCommand : IRequest<DeleteDogResult>
{
    public Guid DogId { get; set; }
}



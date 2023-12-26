using Application.Dtos;
using Domain.Models;
using MediatR;

public class AddDogCommand : IRequest<Dog>
{
    public DogDto NewDog { get; }
    public Guid UserId { get; }

    public AddDogCommand(DogDto newDog, Guid userId)
    {
        NewDog = newDog;
        UserId = userId;
    }
}
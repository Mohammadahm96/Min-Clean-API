using Application.Dtos;
using Domain.Models;
using MediatR;
using System;

public class UpdateDogByIdCommand : IRequest<Dog>
{
    public UpdateDogByIdCommand(Guid id, DogDto updatedDog)
    {
        Id = id;
        UpdatedDog = updatedDog;
    }

    public Guid Id { get; set; }
    public DogDto UpdatedDog { get; }
}

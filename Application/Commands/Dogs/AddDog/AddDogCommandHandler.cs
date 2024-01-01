﻿using Application.Dtos;
using Domain.Models;
using MediatR;
using Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
{
    private readonly IDogRepository _dogRepository;

    public AddDogCommandHandler(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
    }

    public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newDog = new Dog
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name,
                Weight = request.NewDog.Weight,
                Breed = request.NewDog.Breed
            };

            // Use the repository to add the dog
            await _dogRepository.AddDog(newDog, request.UserId);

            return newDog;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating dog: {ex.Message}");

            // Throw a custom exception for better error handling
            throw new DogCreationException("Error creating dog.", ex);
        }
    }
}


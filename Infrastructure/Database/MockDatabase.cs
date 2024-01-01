using System;
using System.Collections.Generic;
using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        private static List<Dog> allDogs = new List<Dog>
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn", Breed = "Golden Retriever", Weight = 25 },
            new Dog { Id = Guid.NewGuid(), Name = "Patrik", Breed = "Labrador", Weight = 30 },
            new Dog { Id = Guid.NewGuid(), Name = "Alfred", Breed = "German Shepherd", Weight = 28 },
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests", Breed = "TestBreed", Weight = 20 }
        };

        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        private static List<Cat> allCats = new List<Cat>
        {
            new Cat { Id = Guid.NewGuid(), Name = "Bulle", LikesToPlay = true, Breed = "Siamese", Weight = 10 },
            new Cat { Id = Guid.NewGuid(), Name = "Mulle", LikesToPlay = false, Breed = "Persian", Weight = 8 },
            new Cat { Id = Guid.NewGuid(), Name = "Hans", LikesToPlay= false, Breed = "Maine Coon", Weight = 12 },
            new Cat { Id = Guid.NewGuid(), Name = "Musse", LikesToPlay = true, Breed = "Bengal", Weight = 9 },
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345679"), Name = "TestCatForUnitTests", LikesToPlay = true, Breed = "TestBreed", Weight = 7 },
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345675"), Name = "TestCatForUnitTests", LikesToPlay = true, Breed = "TestBreed", Weight = 8 }
        };

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Bird> allBirds = new List<Bird>
        {
            new Bird { Id = Guid.NewGuid(), Name = "Tweety", CanFly = true, Color = "Yellow" },
            new Bird { Id = Guid.NewGuid(), Name = "Polly", CanFly = false, Color = "Green" },
            new Bird { Id = Guid.NewGuid(), Name = "Mattias", CanFly = true, Color = "Blue"},
            new Bird { Id = Guid.NewGuid(), Name = "Fly", CanFly= true, Color = "Red" },
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345680"), Name = "TestBirdForUnitTests", CanFly = true, Color = "TestColor" },
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345681"), Name = "TestBirdForUnitTests", CanFly = true, Color = "TestColor" }
        };

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }
    }
}

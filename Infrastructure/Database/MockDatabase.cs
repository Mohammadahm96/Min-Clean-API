using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        private static List<Dog> allDogs = new List<Dog>
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };

        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        private static List<Cat> allCats = new List<Cat>
        {
            new Cat { Id = Guid.NewGuid(), Name = "Bulle", LikesToPlay = true },
            new Cat { Id = Guid.NewGuid(), Name = "Mulle", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Hans", LikesToPlay= false },
            new Cat { Id = Guid.NewGuid(), Name = "Musse", LikesToPlay = true },
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345679"), Name = "TestCatForUnitTests", LikesToPlay = true },
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345675"), Name = "TestCatForUnitTests", LikesToPlay = true }


        };

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Bird> allBirds = new List<Bird>
        {
            new Bird { Id = Guid.NewGuid(), Name = "Tweety", CanFly = true },
            new Bird { Id = Guid.NewGuid(), Name = "Polly", CanFly = false },
            new Bird { Id = Guid.NewGuid(), Name = "Mattias", CanFly = true},
            new Bird { Id = Guid.NewGuid(), Name = "Fly", CanFly= true },
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345680"), Name = "TestBirdForUnitTests", CanFly = true },
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345681"), Name = "TestBirdForUnitTests", CanFly = true }
        };

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }
    }
}


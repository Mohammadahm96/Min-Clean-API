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



    }
}
   


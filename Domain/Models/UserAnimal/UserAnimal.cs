using Domain.Models.Animal;
namespace Domain.Models.User
{
    public class UserAnimal
    {
        public Guid UserId { get; set; }
        public UserModel User { get; set; }

        public Guid AnimalId { get; set; }
        public AnimalModel Animal { get; set; }
        public Guid BirdId { get; set; }
        public Guid CatId { get; set; }
        public Guid DogId { get; set; }
    }
}



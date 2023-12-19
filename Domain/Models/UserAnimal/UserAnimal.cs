using Domain.Models.Animal;
namespace Domain.Models.User
{
    public class UserAnimal
    {
        public Guid UserId { get; set; }
        public required UserModel User { get; set; }

        public Guid AnimalId { get; set; }
        public required AnimalModel Animal { get; set; }
    }
}



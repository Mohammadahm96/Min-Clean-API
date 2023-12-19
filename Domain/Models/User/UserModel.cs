namespace Domain.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Userpassword { get; set; } = string.Empty;

        public ICollection<UserAnimal> UserAnimals { get; set; } = new List<UserAnimal>();
    }
}
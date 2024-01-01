using Domain.Models;

public interface IDogRepository
{
    Task<List<Dog>> GetAllDogs();
    Task<Dog?> GetDogById(Guid id);
    Task AddDog(Dog dog, Guid userId);
    Task UpdateDog(Dog dog);
    Task DeleteDog(Dog dog);
}
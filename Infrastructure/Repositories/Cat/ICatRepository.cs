using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

public interface ICatRepository
{
    Task<Cat> GetCatById(Guid catId);
    Task<List<Cat>> GetAllCats();
    Task AddCat(Cat cat, Guid userId);
    Task UpdateCat(Cat cat);
    Task DeleteCat(Cat cat);
}

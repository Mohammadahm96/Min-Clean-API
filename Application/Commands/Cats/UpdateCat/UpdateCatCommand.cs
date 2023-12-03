using Application.Dtos;
using MediatR;
using Domain.Models;  


namespace Application.Commands.Cats.UpdateCats
{
    public class UpdateCatCommand : IRequest<Cat>
    {
        public UpdateCatCommand(CatDto updatedCat, Guid id)
        {
            UpdatedCat = updatedCat;
            Id = id;
        }

        public Guid Id { get; }
        public CatDto UpdatedCat { get; }
        
    }
   
}

using MediatR;
using Domain.Models;

namespace Application.Queries.Cats.GetCatById
{
    public class GetCatByIdQuery : IRequest<Cat>
    {
        public Guid Id { get; }

        public GetCatByIdQuery(Guid catId)
        {
            Id = catId;       
        }
    }
}



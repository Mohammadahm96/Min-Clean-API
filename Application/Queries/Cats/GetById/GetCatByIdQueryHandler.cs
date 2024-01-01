using Application.Queries.Cats.GetCatById;
using Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat>
{
    private readonly ICatRepository _catRepository;

    public GetCatByIdQueryHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    public async Task<Cat> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
    {
        Cat wantedCat = await _catRepository.GetCatById(request.Id);
        return wantedCat;
    }
}


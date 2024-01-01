using Application.Queries.Cats.GetAll;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
{
    private readonly ICatRepository _catRepository;

    public GetAllCatsQueryHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
    {
        List<Cat> allCatsFromRepository = await _catRepository.GetAllCats();
        return allCatsFromRepository;
    }
}



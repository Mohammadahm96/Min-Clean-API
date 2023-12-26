using Application.Dtos;
using Domain.Models;
using MediatR;

public class AddCatCommand : IRequest<Cat>
{
    public CatDto NewCat { get; }
    public Guid UserId { get; }

    public AddCatCommand(CatDto newCat, Guid userId)
    {
        NewCat = newCat;
        UserId = userId;
    }
}


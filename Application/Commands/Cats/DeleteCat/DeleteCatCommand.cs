using MediatR;


namespace Application.Commands.Cats.DeleteCats
{
    public class DeleteCatCommand : IRequest<DeleteCatResult>
    {
        public Guid CatId { get; set; }
    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Cats.DeleteCats
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, DeleteCatResult>
    {
        private readonly ICatRepository _catRepository;
        private readonly ILogger<DeleteCatCommandHandler> _logger;

        public DeleteCatCommandHandler(ICatRepository catRepository, ILogger<DeleteCatCommandHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }

        public async Task<DeleteCatResult> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = await _catRepository.GetCatById(request.CatId);

            if (catToDelete != null)
            {
                await _catRepository.DeleteCat(catToDelete);
                return new DeleteCatResult { IsSuccess = true };
            }

            return new DeleteCatResult { IsSuccess = false };
        }
    }
}
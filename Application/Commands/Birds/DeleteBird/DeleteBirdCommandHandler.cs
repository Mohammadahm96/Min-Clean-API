﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Birds.DeleteBirds
{
    public class DeleteBirdCommandHandler : IRequestHandler<DeleteBirdCommand, DeleteBirdResult>
    {
        private readonly CleanApiMainContext _dbContext;

        public DeleteBirdCommandHandler(CleanApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteBirdResult> Handle(DeleteBirdCommand request, CancellationToken cancellationToken)
        {
            var birdToDelete = _dbContext.Birds.FirstOrDefault(b => b.Id == request.BirdId);

            if (birdToDelete != null)
            {
                // Remove bird from the database context
                _dbContext.Birds.Remove(birdToDelete);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return new DeleteBirdResult { IsSuccess = true };
            }

            return new DeleteBirdResult { IsSuccess = false };
        }
    }
}

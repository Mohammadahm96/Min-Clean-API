using Application.Commands.Users.RegisterUser;
using Application.Exceptions;
using Application.Validators.User;
using Domain.Models.User;
using Infrastructure.Database;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Users.Register

{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserModel>
    {
        private readonly CleanApiMainContext _dbContext;
        private readonly RegisterUserCommandValidator _validator;

        public RegisterUserCommandHandler(CleanApiMainContext dbContext, RegisterUserCommandValidator validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<UserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var registerCommandValidation = _validator.Validate(request);

            if (!registerCommandValidation.IsValid)
            {
                var allErrors = registerCommandValidation.Errors.ConvertAll(errors => errors.ErrorMessage);
                throw new ArgumentException("Registration error: " + string.Join("; ", allErrors));
            }

            // Check if the username already exists
            if (_dbContext.Users.Any(u => u.Username == request.NewUser.UserName))
            {
                throw new DuplicateUserException(request.NewUser.UserName);
            }

            // Here, you can use AutoMapper or manual mapping to convert Dto to Domain Model
            var userToCreate = new UserModel
            {
                Id = Guid.NewGuid(),
                Username = request.NewUser.UserName,
                Userpassword = request.NewUser.Password,
            };

            // Add user to the database
            _dbContext.Users.Add(userToCreate);
            await _dbContext.SaveChangesAsync();

            return userToCreate;
        }
    }
}





using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Specifications;
using Humanity.Domain.Entities;
using Humanity.Domain.Enums;
using Humanity.Domain.Exceptions;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Interfaces;
using Humanity.Application.Core.Services;
using Humanity.Domain.Core.Repositories;

namespace Humanity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;

        public UserService(IUnitOfWork unitOfWork, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
        }

        public async Task<CreateUserRes> CreateUser(CreateUserReq req)
        {
            var user = await _unitOfWork.Repository<User>().AddAsync(new User
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                EmailId = req.EmailId,
                Password = req.Password,
                Status = req.Status
            });

            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("New user created");

            return new CreateUserRes() { Data = new UserDTO(user) };
        }

        public async Task<ValidateUserRes> ValidateUser(ValidateUserReq req)
        {
            var validateUserSpec = UserSpecifications.GetUserByEmailAndPasswordSpec(req.EmailId, req.Password);

            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(validateUserSpec);

            if (user == null)
            {
                _loggerService.LogInfo("User not found");

                throw new UserNotFoundException();
            }

            if (user.Status != UserStatus.Active)
            {
                _loggerService.LogInfo("User not active");

                throw new UserIsNotActiveException();
            }

            return new ValidateUserRes()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<GetAllActiveUsersRes> GetAllActiveUsers()
        {
            var activeUsersSpec = UserSpecifications.GetAllActiveUsersSpec();

            var users = await _unitOfWork.Repository<User>().ListAsync(activeUsersSpec);

            return new GetAllActiveUsersRes()
            {
                Data = users.Select(x => new UserDTO(x)).ToList()
            };
        }
    }
}
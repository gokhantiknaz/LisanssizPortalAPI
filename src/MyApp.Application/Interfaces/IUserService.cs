using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;

namespace Humanity.Application.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserRes> CreateUser(CreateUserReq req);

        Task<ValidateUserRes> ValidateUser(ValidateUserReq req);

        Task<GetAllActiveUsersRes> GetAllActiveUsers();
    }
}
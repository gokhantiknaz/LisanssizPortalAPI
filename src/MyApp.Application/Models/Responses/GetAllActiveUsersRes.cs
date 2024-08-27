using Humanity.Application.Models.DTOs;

namespace Humanity.Application.Models.Responses
{
    public class GetAllActiveUsersRes
    {
        public IList<UserDTO> Data { get; set; }
    }
}

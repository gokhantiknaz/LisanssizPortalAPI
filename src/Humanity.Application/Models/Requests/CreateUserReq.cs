using System.ComponentModel.DataAnnotations;
using Humanity.Domain.Enums;

namespace Humanity.Application.Models.Requests
{
    public class CreateUserReq
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public UserStatus Status { get; set; }
    }

    public class CreateUserRes
    {

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string EmailId { get; set; }


        public string Password { get; set; }


        public UserStatus Status { get; set; }
    }

    public class AuthResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }

    public class RegisterResponse
    {
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }



}

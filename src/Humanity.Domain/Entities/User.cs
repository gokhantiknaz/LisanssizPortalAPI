using Humanity.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Humanity.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpirationDate { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }

    public static class RoleNames
    {
        public static string Administrator { get; set; } = nameof(Administrator);

        public static string Member { get; set; } = nameof(Member);

        public static  string User = nameof(User);

        public static string[] All => new[] { Administrator, Member };
    }
}

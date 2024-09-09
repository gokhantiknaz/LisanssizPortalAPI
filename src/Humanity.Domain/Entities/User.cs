using Humanity.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Humanity.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public User(string userName) : this()
        {
            UserName = userName;
        }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        [NotMapped]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset LastActiveAt { get; set; }

        public bool PasswordConfigured { get; set; }
    }

    public class Role : IdentityRole<string>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public Role(string roleName) : this()
        {
            Name = roleName;
        }


        [NotMapped]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }

    public static class RoleNames
    {
        public static string Administrator { get; set; } = nameof(Administrator);

        public static string Member { get; set; } = nameof(Member);

        public static string[] All => new[] { Administrator, Member };
    }
}

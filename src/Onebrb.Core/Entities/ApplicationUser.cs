using Microsoft.AspNetCore.Identity;

namespace Onebrb.Core.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public string About { get; set; }
    }
}

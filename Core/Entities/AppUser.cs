using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public int SurtaxId { get; set; }
        public Surtax Surtax { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;

namespace ProjectEvidence.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; } = default!;
        public string CellPhone { get; set; } = default!;
        public string Country { get; set; } = default!;

    }
}

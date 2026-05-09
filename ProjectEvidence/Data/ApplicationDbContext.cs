using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectEvidence.Models;

namespace ProjectEvidence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Cours> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}

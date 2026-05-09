using System.ComponentModel.DataAnnotations;

namespace ProjectEvidence.Models
{
    public class Cours
    {
        public int CoursID { get; set; }
        [Required, StringLength(80)]
        public string? Title { get; set; }
        public int Credits { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}

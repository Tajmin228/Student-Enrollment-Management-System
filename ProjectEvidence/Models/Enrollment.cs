using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectEvidence.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        [ForeignKey("Cours")]
        public int CoursID { get; set; }
    
        public virtual Cours? Cours { get; set; }
        public virtual Student? Student { get; set; }
    }
}

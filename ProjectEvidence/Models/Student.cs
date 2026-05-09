using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectEvidence.Models
{
    public class Student
    {

        public int StudentID { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; } = default;
        [Display(Name = "Date of Birth"), Required, Column(TypeName = "date"), DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; } = default;
        [Display(Name = "Marital Status")]
        public bool MaritalStatus { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}

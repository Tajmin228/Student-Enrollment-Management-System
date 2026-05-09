using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectEvidence.Models.ViewModels
{
    public class StudentVM
    {
        public int StudentID { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; } = default;
        [Display(Name = "Date of Birth"), Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; } = default;
        public string? Picture { get; set; } = default;
        [Display(Name = "Image")]
        public IFormFile? PictureFile { get; set; }
        [Display(Name = "Marital Status")]
        public bool MaritalStatus { get; set; }

        public List<int> CourseList { get; set; } = new List<int>();
    }
}
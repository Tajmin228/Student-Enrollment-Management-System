using ProjectEvidence.Data;
using ProjectEvidence.Models;
using ProjectEvidence.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectEvidence.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;
        private Student student;

        public StudentsController(ApplicationDbContext _context, IWebHostEnvironment _he)
        {
            this._context = _context;
            this._he = _he;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.Include(x=>x.Enrollments).ThenInclude(y=>y.Cours).ToListAsync());
        }
        public IActionResult AddNewCourse(int? id)
        {
            ViewBag.cours = new SelectList(_context.Courses, "CoursID", "Title", id.ToString() ?? "");
            return PartialView("_addNewCourse");
        }
       [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(StudentVM studentVM, int[] coursID)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentName = studentVM.StudentName,
                    BirthDate = studentVM.BirthDate,
                    Age = studentVM.Age,
                    MaritalStatus = studentVM.MaritalStatus
                };

                //For Image
                var file = studentVM.PictureFile;
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string ext = Path.GetExtension(file.FileName);
                string imgFileName = Path.GetRandomFileName() + ext;
                string fileSave = Path.Combine(webroot, folder, imgFileName);

                if (file != null)
                {
                    using (var stream = new FileStream(fileSave, FileMode.Create))
                    {
                        studentVM.PictureFile.CopyToAsync(stream);
                        student.Picture = "/" + folder + "/" + imgFileName;
                    }
                }

                //For Course
                foreach (var item in coursID)
                {
                    Enrollment enrollment = new Enrollment()
                    {
                        Student = student,
                        StudentID = student.StudentID,
                        CoursID = item
                    };
                    _context.Enrollments.Add(enrollment);
                }
                await _context.SaveChangesAsync();
                return PartialView("_success");
            }
            return PartialView("_error");
        }
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentID == id);
            StudentVM studentVM = new StudentVM()
            {
                StudentID= student.StudentID,
                StudentName = student.StudentName,
                BirthDate = student.BirthDate,
                Age = student.Age,
                Picture = student.Picture,
                MaritalStatus = student.MaritalStatus
            };

            //course
            var existingCourse = _context.Enrollments.Where(x => x.StudentID == id).ToList();
            foreach (var item in existingCourse)
            {
                studentVM.CourseList.Add(item.CoursID);
            }
            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentVM studentVM, int[] coursID)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentID= studentVM.StudentID,
                    StudentName = studentVM.StudentName,
                    BirthDate= studentVM.BirthDate,
                    Age= studentVM.Age,
                    MaritalStatus= studentVM.MaritalStatus
                };

                //Image
                var file = studentVM.PictureFile;
                var oldPic = studentVM.Picture;
                if (file != null)
                {
                    string webroot = _he.WebRootPath;
                    string folder = "Images";
                    string ext = Path.GetExtension(file.FileName);
                    string imgFileName = Path.GetRandomFileName() + ext;
                    string fileSave = Path.Combine(webroot, folder, imgFileName);

                    using (var stream=new FileStream(fileSave, FileMode.Create))
                    {
                        studentVM.PictureFile.CopyTo(stream);
                        student.Picture= "/" + folder + "/" + imgFileName;
                    }
                }
                else
                {
                    student.Picture = oldPic;
                }

                //course
                var existCourse = _context.Enrollments.Where(x => x.StudentID == student.StudentID).ToList();
                foreach (var item in existCourse)
                {
                    _context.Enrollments.Remove(item);
                }

                //add new Course
                foreach (var item in coursID)
                {
                    Enrollment enrollment = new Enrollment()
                    {
                        StudentID = student.StudentID,
                        CoursID = item
                    };
                    _context.Enrollments.Add(enrollment);
                }
                _context.Update(student);
                await _context.SaveChangesAsync();
                return PartialView("_success");
            }
            return PartialView("_error");
        }

        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            var st= await _context.Students.FirstOrDefaultAsync(x => x.StudentID == id);
            var exitCourse = _context.Enrollments.Where(x => x.StudentID == id).ToList();
            foreach(var item in exitCourse)
            {
                _context.Enrollments.Remove(item);
            }
            _context.Remove(st);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

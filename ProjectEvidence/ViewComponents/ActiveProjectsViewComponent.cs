using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEvidence.Data;


namespace ProjectEvidence.ViewComponents
{
    public class ActiveProjectsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ActiveProjectsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeCount = await _context.Students.CountAsync(p => !string.IsNullOrEmpty(p.StudentName));

            return View(activeCount);
        }
    }
}


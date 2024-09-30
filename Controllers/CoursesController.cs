using Contoso_University.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contoso_University.Controllers
{
	public class CoursesController : Controller
	{
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
		{
			return View(await _context.Courses.ToListAsync());
        }
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var course = await _context.Courses
				.FirstOrDefaultAsync(c => c.CourseID == id);
			if (course == null)
			{
				return NotFound();
			}
			return View(course);
		}
	}
}

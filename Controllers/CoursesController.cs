using Contoso_University.Data;
using Contoso_University.Models;
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

		public async Task<IActionResult> Clone(int id)
		{
			var course = await _context.Courses
				.FirstOrDefaultAsync(c => c.CourseID == id);

			if (course == null)
			{
				return NotFound();
			}

			var maxCourseID = await _context.Courses.MaxAsync(c => c.CourseID);
			var newCourseID = maxCourseID + 1;

			var clonedCourse = new Course
			{
				CourseID = newCourseID,
				Title = course.Title,
				Credits = course.Credits
			};

			_context.Courses.Add(clonedCourse);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
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

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var course = await _context.Courses.FindAsync(id);

			_context.Courses.Remove(course);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}

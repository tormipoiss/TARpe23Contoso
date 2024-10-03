﻿using Contoso_University.Data;
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

		[HttpGet]
		public async Task<IActionResult> DetailsDelete(int? id, string mode)
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

			ViewBag.Mode = mode;

			return View(course);
		}

		[HttpPost, ActionName("DetailsDelete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DetailsDeleteConfirmed(int id)
		{
			var course = await _context.Courses.FindAsync(id);

			if (course == null)
			{
				return NotFound();
			}

			_context.Courses.Remove(course);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
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
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("CourseID,Title,Credits")] Course course)
		{
			if (ModelState.IsValid)
			{
				if (_context.Courses.Any(c => c.CourseID == course.CourseID))
				{
					// Add a validation error to the ModelState
					ModelState.AddModelError("CourseID", "CourseID already exists. Please enter an unique CourseID.");
					return View(course); // Return the same view with the error message
				}
				if (course.CourseID < 0)
				{
					ModelState.AddModelError("CourseID", "CourseID is negative. Please enter a positive CourseID.");
					return View(course);
				}
				_context.Courses.Add(course);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(course);
		}
	}
}

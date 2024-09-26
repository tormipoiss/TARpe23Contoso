using Contoso_University.Data;
using Contoso_University.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Contoso_University.Controllers
{
	public class DepartmentsController : Controller
	{
		private readonly SchoolContext _context;

        public DepartmentsController(SchoolContext context)
        {
			_context = context;
        }

        public async Task<IActionResult> Index()
		{
			var schoolContext = _context.Departments.Include(d => d.Administrator);
			return View(await schoolContext.ToListAsync());
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			string query = "SELECT * FROM Departments WHERE DepartmentID = {0}";
			var department = await _context.Departments
				.FromSqlRaw(query, id)
				.Include(d => d.Administrator)
				.AsNoTracking()
				.FirstOrDefaultAsync();
			if (department == null) 
			{ 
				return NotFound(); 
			}
			return View(department);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Budget,StartDate,RowVersion,InstructorID,EmployeeAmount,FavoriteFood")] Department department)
		{
			if (ModelState.IsValid)
			{
				_context.Add(department);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
			return View(department);
		}
	}
}
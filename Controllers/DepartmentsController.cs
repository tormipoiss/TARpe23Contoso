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

		//Delete GET meetod, otsib andmebaasist kaasaantud id järgi osakonda.

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) // Kui id on tühi/null, siis osakonda ei leia
			{
				return NotFound();
			}

			string query = "SELECT * FROM Departments WHERE DepartmentID = {0}"; // Tehakse osakonna objekt, andmebaasis oleva ID järgi
			var department = await _context.Departments
				.FromSqlRaw(query, id)
				.Include(d => d.Administrator)
				.AsNoTracking()
				.FirstOrDefaultAsync();

			if (department == null) // Kui õpetaja objekt on tühi/null, siis ka õpetajat ei leita
			{
				return NotFound();
			}

			return View(department);
		}

		// Delete POST meetod, teostab andmebaasis vajaliku muudatuse. Ehk kustutab andme ära

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var department = await _context.Departments.FindAsync(id); //Otsime andmebaasist osakonda id järgi, ja paneme ta "osakond" nimelisse muutujasse.

			_context.Departments.Remove(department);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		// Edit GET

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName");
			return View();
		}

		// Edit POST

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("DepartmentID, Name, Budget, StartDate, RowVersion, InstructorID, EmployeeAmount, FavoriteFood")] Department department)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Fetch the existing department from the database
					var existingDepartment = await _context.Departments
						.FirstOrDefaultAsync(d => d.DepartmentID == id);

					if (existingDepartment == null)
					{
						return NotFound();
					}

					// Update the department fields manually
					existingDepartment.Name = department.Name;
					existingDepartment.Budget = department.Budget;
					existingDepartment.StartDate = department.StartDate;
					existingDepartment.EmployeeAmount = department.EmployeeAmount;
					existingDepartment.FavoriteFood = department.FavoriteFood;

					// Update the InstructorID (foreign key)
					existingDepartment.InstructorID = department.InstructorID;

					// Save the changes
					await _context.SaveChangesAsync();

					return RedirectToAction("Index");
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.Departments.Any(d => d.DepartmentID == department.DepartmentID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}

			// Reload the dropdown list and return to the view if model state is invalid
			ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
			return View(department);
		}

		// BaseOn GET

		[HttpGet]
		public async Task<IActionResult> BaseOn(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			// Fetch the department by id including its related data if necessary
			var department = await _context.Departments
				.Include(d => d.Administrator) // Include related data if needed
				.FirstOrDefaultAsync(m => m.DepartmentID == id);
			

			if (department == null)
			{
				return NotFound();
			}
			
			// Pass the Instructor list for dropdown
			ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
			return View(department);
		}

		// BaseOn POST

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> BaseOn(int id, [Bind("DepartmentID, Name, Budget, StartDate, RowVersion, InstructorID, EmployeeAmount, FavoriteFood")] Department department, string actionType)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Fetch the existing department from the database
					var existingDepartment = await _context.Departments
						.FirstOrDefaultAsync(d => d.DepartmentID == id);

					if (existingDepartment == null)
					{
						return NotFound();
					}

					if (actionType == "Make")
					{
						_context.Add(department);

						// Save the changes
						await _context.SaveChangesAsync();
					}

					else if (actionType == "Make & Delete Old")
					{
						_context.Departments.Remove(existingDepartment);

						_context.Add(department);

						// Save changes to delete the old department
						await _context.SaveChangesAsync();
					}

					return RedirectToAction("Index");
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.Departments.Any(d => d.DepartmentID == department.DepartmentID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}

			// Reload the dropdown list and return to the view if model state is invalid
			ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
			return View(department);
		}
	}
}
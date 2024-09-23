using Contoso_University.Data;
using Contoso_University.Models;
using Microsoft.AspNetCore.Mvc;
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
	}
}
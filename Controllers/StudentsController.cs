using Contoso_University.Data;
using Contoso_University.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
	public class StudentsController : Controller
	{
		private readonly SchoolContext _context;

		public StudentsController(SchoolContext context)
		{
			_context = context;
		}

        // get all for index, retrieve all students
		public async Task<IActionResult> Index()
		{
			return View(await _context.Students.ToListAsync());
		}

        /*
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber
            )
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null) 
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["currentFilter"] = searchString;

            var students = from student in _context.Students
                           select student;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(student => 
                student.LastName.Contains(searchString) || 
                student.FirstMidName.Contains(searchString));
            }
            switch (sortOrder) 
            {
                case "name_desc":
                    students = students.OrderByDescending(student => student.LastName);
                    break;
                case "firstname_desc":
                    students = students.OrderByDescending(student => student.FirstMidName);
                    break;
                case "Date":
                    students = students.OrderBy(student => student.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(student => student.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(student => student.LastName);
                    break;
            }

            int pageSize = 3;
            return View(await _context.Students.ToListAsync());
        }
        */

        // Create get, haarab vaatest andmed, mida create meetod vajab.
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        // Create meetod, sisestab andmebaasi uue õpilase. Insert new student into database

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        /// <summary>
        /// Asünkroonne Details GET meetod. 
        /// Leiab andmebaasist päringus oleva ID järgi õpilase
        /// ning tagastab vaate koos selle õpilase infoga.
        /// </summary>
        /// <param name="id">Otsitava õpilase ID</param>
        /// <returns>Tagastab kasutajale vaate, koos õpilase andmetega</returns>
        //Details GET meetod, kuvab ühe õpilase andmed eraldi lehel
        public async Task<IActionResult> Details(int? id) // id on optional, kuid vajalik eduka 
        {
            if (id == null) // kui id on tühi/null, siis õpilast ei leita
            {
                return NotFound();
            }

            var student = await _context.Students // Tehakse õpilase objekt, andmebaasis oleva ID järgi
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null) // kui student objekt on tühi/null, siis ka õpilast ei leita
            {
                return NotFound();
            }
            return View(student); // tagastam kasutajale vaate koos õpilastega
        }

        //Delete GET meetod, otsib andmebaasist kaasaantud id järgi õpilast.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // Kui id on tühi/null, siis õpilast ei leia
            {
                return NotFound();
            }

            var student = await _context.Students // Tehakse õpilase objekt, andmebaasis oleva ID järgi
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null) // Kui student objekt on tühi/null, siis ka õpilast ei leita
            {
                return NotFound();
            }

            return View(student);
        }

        // Delete POST meetod, teostab andmebaasis vajaliku muudatuse. Ehk kustutab andme ära

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id); //Otsime andmebaasist õpilast id järgi, ja paneme ta "student" nimelisse muutujasse.

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Clone meetod

        public async Task<IActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students // Tehakse õpilase objekt, andmebaasis oleva ID järgi
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            var clonedStudent = new Student
            {
                LastName = student.LastName,
                FirstMidName = student.FirstMidName,
                EnrollmentDate = student.EnrollmentDate
            };

            _context.Students.Add(clonedStudent);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
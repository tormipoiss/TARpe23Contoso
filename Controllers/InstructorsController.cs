using System.Reflection;
using Contoso_University.Data;
using Contoso_University.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contoso_University.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;

        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? id, int? courseId)
        {
            var vm = new InstructorIndexData();
            vm.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Enrollments)
                .ThenInclude(i => i.Student)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = vm.Instructors
                    .Where(i => i.ID == id.Value).Single();
                vm.Courses = instructor.CourseAssignments
                    .Select(i => i.Course);
            }
            if (courseId != null)
            {
                ViewData["CourseID"] = courseId.Value;
                vm.Enrollments = vm.Courses
                    .Where(x => x.CourseID == courseId)
                    .Single()
                    .Enrollments;
            }
			return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            //if (selectedCourses == null)
            //{
            //    instructor.CourseAssignments = new List<CourseAssignment>();
            //    foreach (var course in selectedCourses)
            //    {
            //        var courseToAdd = new CourseAssignment()
            //        {
            //            InstructorID = instructor.ID,
            //            CourseId = course
            //        };
            //        instructor.CourseAssignments.Add(courseToAdd);
            //    }
            //}
            //ModelState.Remove();
            //ModelState.Remove(selectedCourses);
            if (ModelState.IsValid) 
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(instructor); //uuendab instructori juures olevaid kursuseid
            return View (instructor);
        }

		private void PopulateAssignedCourseData(Instructor instructor)
		{
            var allCourses = _context.Courses; // leiame kõik kursused
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID)); //valime kursused kus courseid on õpetajal olemas
            var vm = new List<AssignedCourseData>(); // teeme viewmodeli jaoks uue nimekirja
            foreach (var course in allCourses) 
            {
                vm.Add(new AssignedCourseData
                {
					CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewData["Courses"] = vm;
		}

		//Delete GET meetod, otsib andmebaasist kaasaantud id järgi õpetajat.
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) // Kui id on tühi/null, siis õpilast ei leia
			{
				return NotFound();
			}

			var instructor = await _context.Instructors // Tehakse õpetaja objekt, andmebaasis oleva ID järgi
				.FirstOrDefaultAsync(m => m.ID == id);

			if (instructor == null) // Kui õpetaja objekt on tühi/null, siis ka õpetajat ei leita
			{
				return NotFound();
			}

			return View(instructor);
		}

		// Delete POST meetod, teostab andmebaasis vajaliku muudatuse. Ehk kustutab andme ära

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var instructor = await _context.Instructors.FindAsync(id); //Otsime andmebaasist õpetajat id järgi, ja paneme ta "õpetaja" nimelisse muutujasse.

			_context.Instructors.Remove(instructor);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		// Edit GET

		[HttpGet]
		public IActionResult Edit()
		{
			var instructor = new Instructor();
			return View();
		}

		// Edit POST

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Instructor instructor)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Get the original instructor entity from the database including the related OfficeAssignment
					var existingInstructor = await _context.Instructors
						.Include(i => i.OfficeAssignment)
						.FirstOrDefaultAsync(i => i.ID == instructor.ID);

					if (existingInstructor == null)
					{
						return NotFound();
					}

					// If OfficeAssignment exists in the form, update it
					if (instructor.OfficeAssignment != null)
					{
						// If there was no previous OfficeAssignment, add the new one
						if (existingInstructor.OfficeAssignment == null)
						{
							existingInstructor.OfficeAssignment = instructor.OfficeAssignment;
						}
						else
						{
							// Update the existing OfficeAssignment's properties without changing the key
							existingInstructor.OfficeAssignment.Location = instructor.OfficeAssignment.Location;
						}
					}
					else
					{
						// If the OfficeAssignment was removed (set to null), delete it
						if (existingInstructor.OfficeAssignment != null)
						{
							_context.OfficeAssignments.Remove(existingInstructor.OfficeAssignment);
						}
					}

					// Update the other properties of the Instructor
					existingInstructor.FirstMidName = instructor.FirstMidName;
					existingInstructor.LastName = instructor.LastName;
					existingInstructor.HireDate = instructor.HireDate;

					// Save the changes
					await _context.SaveChangesAsync();

					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.Instructors.Any(i => i.ID == instructor.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}

			return View(instructor);
		}

		// Clone meetod

		public async Task<IActionResult> Clone(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			// Retrieve the instructor with their OfficeAssignment
			var instructor = await _context.Instructors
				.Include(i => i.OfficeAssignment) // Ensure OfficeAssignment is included
				.FirstOrDefaultAsync(i => i.ID == id);

			if (instructor == null)
			{
				return NotFound();
			}

			// Create a new Instructor instance, copying the properties
			var clonedInstructor = new Instructor
			{
				LastName = instructor.LastName,
				FirstMidName = instructor.FirstMidName,
				HireDate = instructor.HireDate,
				// Clone OfficeAssignment if it exists
				OfficeAssignment = instructor.OfficeAssignment != null
					? new OfficeAssignment
					{
						Location = instructor.OfficeAssignment.Location
						// Add any other properties if needed
					}
					: null
			};

			_context.Instructors.Add(clonedInstructor);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}
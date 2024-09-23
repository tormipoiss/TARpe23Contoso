using Contoso_University.Controllers;
using Contoso_University.Models;

namespace Contoso_University.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {

            //teeb kindlaks et andmebaas tehakse, või oleks olemas
            //context.Database.EnsureCreated();

            //kui õpilaste tabelis juba on õpilasi, väljub funktsioonist
            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[] {
                    new Student { FirstMidName = "Skibidi", LastName = "Rizz", EnrollmentDate = DateTime.Parse("2069-04-20") },
                    new Student { FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2002-09-01") },
                    new Student { FirstMidName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2003-09-01") },
                    new Student { FirstMidName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01") },
                    new Student { FirstMidName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2002-09-01") },
                    new Student { FirstMidName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2001-09-01") },
                    new Student { FirstMidName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2003-09-01") },
                    new Student { FirstMidName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2005-09-01") },
                    new Student { FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") },
                    new Student { FirstMidName = "Tormi", LastName = "Laane", EnrollmentDate = DateTime.Parse("2023-08-20") }
                };
            context.Students.AddRange(students);
            context.SaveChanges();

			if (context.Courses.Any())
			{
				return;
			}
			var courses = new Course[]
			{
				new Course{CourseID=1050,Title="Keemia",Credits=3},
				new Course{CourseID=4022,Title="Mikroökonoomika",Credits=3},
				new Course{CourseID=4041,Title="Mikroökonoomika",Credits=3},
				new Course{CourseID=1045,Title="Calculus",Credits=4},
				new Course{CourseID=3141,Title="Trigonomeetria",Credits=4},
				new Course{CourseID=2021,Title="Kompositsioon",Credits=3},
				new Course{CourseID=2042,Title="Kirjandus",Credits=4},
				new Course{CourseID=1069,Title="Kehaline Kasvatus",Credits=1}
			};
			context.Courses.AddRange(courses);
			context.SaveChanges();

			/*
			if (context.Enrollments.Any()) { return; }
			var enrollments = new Enrollment[]
			{
				new Enrollment{StudentId=1,CourseID=1050,Grade=Grade.A},
				new Enrollment{StudentId=1,CourseID=4022,Grade=Grade.C},
				new Enrollment{StudentId=1,CourseID=4041,Grade=Grade.B},

				new Enrollment{StudentId=2,CourseID=1845,Grade=Grade.B},
				new Enrollment{StudentId=2,CourseID=3141,Grade=Grade.F},
				new Enrollment{StudentId=2,CourseID=2021,Grade=Grade.F},

				new Enrollment{StudentId=3,CourseID=1050},

				new Enrollment{StudentId=4,CourseID=1050},
				new Enrollment{StudentId=4,CourseID=4022,Grade=Grade.F},

				new Enrollment{StudentId=5,CourseID=4041,Grade=Grade.C},

				new Enrollment{StudentId=6,CourseID=1045},

				new Enrollment{StudentId=7,CourseID=3141,Grade=Grade.A},
				
				new Enrollment{StudentId=10,CourseID=1069,Grade=Grade.A},
			};
			context.Enrollments.AddRange(enrollments);
			context.SaveChanges();
			*/

			if (context.Instructors.Any() ) { return; }
			var instructor = new Instructor[]
			{
				new Instructor {
					LastName = "Toilet",
					FirstMidName = "Heheyes",
					HireDate = DateTime.Parse("2023-09-01"),
					Height = 160,
					PreviousWorkPlace = "TTHK",
					HairColor = "Black"
				},
				new Instructor {
					LastName = "Leyne",
					FirstMidName = "Hiines",
					HireDate = DateTime.Parse("2015-09-01"),
					Height = 195,
					PreviousWorkPlace = "Pirita kool",
					HairColor = "Brown"
				},
			};
			context.Instructors.AddRange(instructor);
			context.SaveChanges();

			if(context.Departments.Any()) { return; }
			var departments = new Department[]
			{
				new Department
				{
					Name = "McDonalds",
					Budget = 0,
					StartDate = DateTime.Parse("2024/09/01"),
					FavoriteFood = "bigmac",
					InstructorID = 1
				},
				new Department
				{
					Name = "Frog eaters",
					Budget = 0,
					StartDate = DateTime.Parse("2024/09/01"),
					FavoriteFood = "frog leg",
					InstructorID = 1
				},
				new Department
				{
					Name = "Big men 101",
					Budget = 0,
					StartDate = DateTime.Parse("2024/12/01"),
					FavoriteFood = "beta males",
				}
			};
			context.Departments.AddRange(departments);
			context.SaveChanges();

			//objekt õpilastega, mis lisatakse siis, kui õpilasi sisestatud ei ole
			//var students = new Student[]
			//{
			//    new Student{FirstMidName="Skibidi",LastName="Rizz",EnrollmentDate=DateTime.Parse("2069-04-20")},
			//    new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
			//    new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
			//    new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
			//    new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
			//    new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
			//    new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
			//    new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")},
			//    new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
			//    new Student{FirstMidName="Tormi",LastName="Laane",EnrollmentDate=DateTime.Parse("2023-08-20")},
			//};

			////iga õpilane lisatakse ükshaaval läbi foreach tsükli
			//foreach (Student student in students)
			//{
			//    context.Students.Add(student);
			//}
			////ja andmebaasi muudatused salvestatakse
			//context.SaveChanges();

			////eelnev struktuur, kuid kursustega: \/
			//var courses = new Course[]
			//{
			//    new Course{CourseID=1050,Title="Keemia",Credits=3},
			//    new Course{CourseID=4022,Title="Mikroökonoomika",Credits=3},
			//    new Course{CourseID=4041,Title="Mikroökonoomika",Credits=3},
			//    new Course{CourseID=1045,Title="Calculus",Credits=4},
			//    new Course{CourseID=3141,Title="Trigonomeetria",Credits=4},
			//    new Course{CourseID=2021,Title="Kompositsioon",Credits=3},
			//    new Course{CourseID=2042,Title="Kirjandus",Credits=4},
			//    new Course{CourseID=1069,Title="Kehaline Kasvatus",Credits=1}
			//};
			//foreach(Course course in courses)
			//{
			//    context.Courses.Add(course);
			//}
			//context.SaveChanges();

			//var enrollments = new Enrollment[]
			//{
			//    new Enrollment{StudentId=1,CourseID=1050,Grade=Grade.A},
			//    new Enrollment{StudentId=1,CourseID=4022,Grade=Grade.C},
			//    new Enrollment{StudentId=1,CourseID=4041,Grade=Grade.B},

			//    new Enrollment{StudentId=2,CourseID=1845,Grade=Grade.B},
			//    new Enrollment{StudentId=2,CourseID=3141,Grade=Grade.F},
			//    new Enrollment{StudentId=2,CourseID=2021,Grade=Grade.F},

			//    new Enrollment{StudentId=3,CourseID=1050},

			//    new Enrollment{StudentId=4,CourseID=1050},
			//    new Enrollment{StudentId=4,CourseID=4022,Grade=Grade.F},

			//    new Enrollment{StudentId=5,CourseID=4041,Grade=Grade.C},

			//    new Enrollment{StudentId=6,CourseID=1045},

			//    new Enrollment{StudentId=7,CourseID=3141,Grade=Grade.A},

			//    new Enrollment{StudentId=10,CourseID=1069,Grade=Grade.A},
			//};
			//foreach (Enrollment enrollment in enrollments)
			//{
			//    context.Enrollments.Add(enrollment);
			//}
			//context.SaveChanges();
		}
	}
}
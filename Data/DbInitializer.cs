using Contoso_University.Models;

namespace Contoso_University.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student{FirstMidName="Artjom",LastName="Škatškov",EnrollmentDate=DateTime.Parse
                    ("2069-04-20")}
            };
        }
    }
}

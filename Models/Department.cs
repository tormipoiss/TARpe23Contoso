using System.ComponentModel.DataAnnotations;

namespace Contoso_University.Models
{
	public class Department
	{
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        /*
         * Kaks oma andmetüüpi osakonna jaoks
         * 
         */
        public Student? StudentAge { get; set; } // minu isklik anne, Student vanus
        public string? FavoriteFood { get; set; } // minu isiklik anne, Student lemmik toit
        public int? InstructorID { get; set; }
        public byte? RowVersion { get; set; }
        public Instructor? Administrator { get; set; }
        public ICollection<Course>? Courses { get; set; }

    }
}

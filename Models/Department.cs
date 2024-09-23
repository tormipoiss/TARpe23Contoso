using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contoso_University.Models
{
	public class Department
	{
        [Key]
        public int DepartmentID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Budget { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        /*
         * Kaks oma andmetüüpi osakonna jaoks
         */
        public Student? StudentAge { get; set; } // minu isklik anne, Student vanus
		[Display(Name = "Favorite food:")]
		public string? FavoriteFood { get; set; } // minu isiklik anne, Student lemmik toit
        public int? InstructorID { get; set; }
        [Timestamp]
        public byte? RowVersion { get; set; }
        public Instructor? Administrator { get; set; }
        public ICollection<Course>? Courses { get; set; }

    }
}

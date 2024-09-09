using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Contoso_University.Models
{
    public class Instructor
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [Column("First Name")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName // uus andmeväli moodustatake olemasolevaist, mitte ei küsita kasutajalt korduvalt sama asja
        { get 
            { return LastName + ", " + FirstMidName; }
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Hired on:")]
        public DateTime HireDate { get; set; }

        public ICollection<CourseAssignment>? CourseAssignments { get; set; }
        public OfficeAssignment? OfficeAssignment { get; set; }
        // Igaühel on oma kolm unikaalset propertyt

        public int? Height { get; set; } // õpetaja pikkus
        [Display(Name = "Previous work place")]
        public string? PreviousWorkPlace { get; set; } //  eelmine töökoht
        [Display(Name = "Hair color")]
        public string? HairColor { get; set; } //  juukse värvus
    }
}

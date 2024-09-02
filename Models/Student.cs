using System.ComponentModel.DataAnnotations;

namespace Contoso_University.Models
{
    public class Student
    {
        [Key] //primaarvõti
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
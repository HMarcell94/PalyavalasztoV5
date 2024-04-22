
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Palyavalaszto.Data.Entitites
{
    [Table("applications")]
    public class applications
    {
        [Key]
        [Required]
        public int ApplicationID { get; set; }

        public int? JobID { get; set; }

        public job job { get; set; }

        public int? EmployeeID { get; set; }

        public employee employee { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string ApplicationStatus { get; set; }

    }
}
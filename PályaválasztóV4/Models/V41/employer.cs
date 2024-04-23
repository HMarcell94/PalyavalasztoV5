using PalyavalsztoV4.Models.v4_1;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PalyavalsztoV4.Models.v4_1
{
    [Table("employers")]
    public class employer
    {
        [Key]
        [Required]
        public int EmployerID { get; set; }

        public int? UserID { get; set; }

        public user user { get; set; }

        public string ContactNumber { get; set; }

        public string CompanyURL { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string? CompanyLogo { get; set; }

        public ICollection<job> jobs { get; set; }
        public string CompanyLocation { get; set; }

        public string size { get; set; }

        public string enterprisetype { get; set; }

    }
}
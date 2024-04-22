using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Palyavalaszto.Data.Entitites
{
    [Table("roles")]
    public  class role
    {
        [Key]
        [Required]
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public ICollection<user> users { get; set; }

    }
}

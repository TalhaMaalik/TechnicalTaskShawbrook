using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataModel
{
    [Table("Membership")]
    public class MembershipModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        public string? Type { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Days { get; set; }
        public Guid ItemId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataModel
{
    [Table("CustomerMembership")]
    public class CustomerMembershipModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerID { get; set; }
        public Guid MembershipID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

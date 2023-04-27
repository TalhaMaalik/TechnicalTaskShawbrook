using BusinessLayer.Enums;

namespace BusinessLayer.Models.Membership
{
    public class BookClubMembership : Membership
    {
        public override MembershipType MembershipType => MembershipType.BookMembership;
        public BookClubMembership(Guid id, string? name, decimal cost, int days) : base(id, name, cost, days) { }
    }
}

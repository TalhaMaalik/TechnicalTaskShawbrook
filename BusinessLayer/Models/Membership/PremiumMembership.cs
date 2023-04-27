using BusinessLayer.Enums;

namespace BusinessLayer.Models.Membership
{
    public class PremiumClubMembership : Membership
    {
        public override MembershipType MembershipType => MembershipType.PremiumMembership;
        public PremiumClubMembership(Guid id, string? name, decimal cost, int days) : base(id, name, cost, days) { }
    }
}

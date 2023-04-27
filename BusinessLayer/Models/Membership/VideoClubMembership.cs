using BusinessLayer.Enums;

namespace BusinessLayer.Models.Membership
{
    public class VideoClubMembership : Membership
    {
        public override MembershipType MembershipType => MembershipType.VideoMembership;
        public VideoClubMembership(Guid id, string? name, decimal cost, int days) : base(id, name, cost, days) { }
    }
}

namespace BusinessLayer.Models.Membership
{
    public class VideoClubMembership : Membership
    {
        public VideoClubMembership(Guid id, string? name, decimal cost) : base(id, name, cost){}
    }
}

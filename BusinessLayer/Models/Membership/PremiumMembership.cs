namespace BusinessLayer.Models.Membership
{
    public class PremiumClubMembership : Membership
    {
        public PremiumClubMembership(Guid id, string? name, decimal cost) : base(id, name, cost)
        {

        }
    }
}

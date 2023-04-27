namespace BusinessLayer.Models.Membership
{
    public class UnknownMembership : Membership
    {
        public UnknownMembership(Guid id, string? name, decimal cost, int days) : base(id, name, cost, days) {}
    }
}

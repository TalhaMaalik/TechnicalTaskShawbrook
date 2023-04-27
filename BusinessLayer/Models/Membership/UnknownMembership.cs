namespace BusinessLayer.Models.Membership
{
    public class UnknownMembership : Membership
    {
        public UnknownMembership(Guid id, string? name, decimal cost) : base(id, name, cost){}
    }
}

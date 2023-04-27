namespace BusinessLayer.Models.Membership
{
    public class BookClubMembership : Membership
    {
        public BookClubMembership(Guid id, string? name, decimal cost) : base(id, name, cost){}
    }
}

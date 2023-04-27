using BusinessLayer.Models.Base;

namespace BusinessLayer.Models.Membership
{
    public abstract class Membership : Item
    {
        public Membership(Guid id, string? name, decimal cost) : base(id, name, cost){}
    }
}   

using BusinessLayer.Models.Base;
using BusinessLayer.Processors.Visitor;

namespace BusinessLayer.Models.Membership
{
    public abstract class Membership : Item
    {
        public Membership(Guid id, string? name, decimal cost) : base(id, name, cost){}

        public override void Accept(IItemVisitor visitor)
        {
            visitor.VisitMembership(this);
        }

    }
}   

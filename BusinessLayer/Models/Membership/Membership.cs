using BusinessLayer.Enums;
using BusinessLayer.Models.Base;
using BusinessLayer.Processors.Visitor;

namespace BusinessLayer.Models.Membership
{
    public abstract class Membership : Item
    {
        public Membership(Guid id, string? name, decimal cost, int days) : base(id, name, cost)
        {
            Days = days;
        }

        public virtual MembershipType MembershipType => MembershipType.Unknown;

        public int Days { get; set; }

        public virtual DateTime CaculateEndTime(DateTime startTime)
        {
            return startTime.AddDays(Days);
        }

        public override void Accept(IItemVisitor visitor)
        {
            visitor.VisitMembership(this);
        }

    }
}   

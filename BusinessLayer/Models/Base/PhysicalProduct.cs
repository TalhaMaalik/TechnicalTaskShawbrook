using BusinessLayer.Processors.Strategies;
using BusinessLayer.Processors.Visitor;

namespace BusinessLayer.Models.Base
{
    public abstract class PhysicalProduct : Item
    {
        public PhysicalProduct(Guid id, string? name, decimal cost) : base(id, name, cost) { }

        public override void Accept(IItemVisitor visitor)
        {
            visitor.VisitPhysicalProduct(this);
        }

        public virtual string GetShippingSlip(IShippingSlipStrategy strategy)
        {
            return strategy.GenerateShippingSlip(this);
        }

    }
}

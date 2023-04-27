using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using System.Text;

namespace BusinessLayer.Processors.Visitor
{
    public interface IItemVisitor
    {
        public StringBuilder ShippingSlip { get; }
        public void VisitMembership(Membership membership);
        public void VisitPhysicalProduct(PhysicalProduct product);
        public void VisitItem(Item item);
    }
}

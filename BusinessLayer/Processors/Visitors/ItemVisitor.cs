using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Strategies;
using DataAccessLayer.Data;
using System.Text;

namespace BusinessLayer.Processors.Visitor
{
    public class ItemVisitor : IItemVisitor
    {
        private readonly ICustomerRepository _CustomerRepository;
        public StringBuilder ShippingSlip { get; set; } = new StringBuilder();

        public ItemVisitor(ICustomerRepository customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        public void ProcessPhysicalProduct(StringBuilder builder, PhysicalProduct product)
        {
            var shippingSlip = product.Process(new KeyValueShippingSlip());
            builder.Append(shippingSlip);
        }

        public void VisitItem(Item item)
        {
            item.Process();
        }

        public void VisitMembership(Membership membership)
        {
            membership.Process();
        }

        public void VisitPhysicalProduct(PhysicalProduct product)
        {
            var shippingSlip = product.Process(new KeyValueShippingSlip());
            ShippingSlip.Append(shippingSlip);
        }
    }
}

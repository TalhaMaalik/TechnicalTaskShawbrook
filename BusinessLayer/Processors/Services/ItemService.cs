using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Strategies;
using DataAccessLayer.Data;
using System.Text;

namespace BusinessLayer.Processors.Services
{
    public class ItemService : IItemService
    {
        private readonly ICustomerRepository _CustomerRepository;

        public ItemService(ICustomerRepository customerRepository) 
        {
            _CustomerRepository = customerRepository;
        }
        public void ProcessItem(Item item)
        {
            item.Process();
        }

        public void ProcessMembership(Membership membership)
        {
            membership.Process();
        }

        public void ProcessPhysicalProduct(StringBuilder builder, PhysicalProduct product)
        {
            var shippingSlip = product.Process(new KeyValueShippingSlip());
            builder.Append(shippingSlip);
        }
    }
}

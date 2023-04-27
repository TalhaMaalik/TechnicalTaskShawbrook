using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Strategies;
using DataAccessLayer.Data;
using DataAccessLayer.DataModel;
using System.Text;

namespace BusinessLayer.Processors.Visitor
{
    public class ItemProcessor : IItemVisitor
    {
        private readonly ICustomerRepository _CustomerRepository;
        public StringBuilder ShippingSlip { get; set; } = new StringBuilder();
        public Guid CustomerId { get; set; }

        public ItemProcessor(ICustomerRepository customerRepository)
        {
            _CustomerRepository = customerRepository;
        }
        public void VisitItem(Item item)
        {
            item.Process();
        }

        public void VisitMembership(Membership membership)
        {
            membership.Process();
            var customer = _CustomerRepository.GetCustomerById(CustomerId);
            StartNewMembership(customer, membership);
        }

        public void VisitPhysicalProduct(PhysicalProduct product)
        {
            product.Process();
            var shippingSlip = product.GetShippingSlip(new KeyValueShippingSlip());
            ShippingSlip.Append(shippingSlip);
        }

        private void StartNewMembership(CustomerModel customer, Membership membership)
        {
            var customerMembership = new CustomerMembershipModel()
            {
                Id = Guid.NewGuid(),
                CustomerID = customer.Id,
                MembershipID = membership.Id,
                StartDate = DateTime.Now,
                EndDate = membership.CaculateEndTime(DateTime.Now)
            };
            _CustomerRepository.AddNewMembership(customerMembership);
            _CustomerRepository.SaveChanges();
        }

    }
}

using BusinessLayer.Messages;
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
        private readonly IItemRepository _ItemRepository;

        public StringBuilder ShippingSlip { get; set; } = new StringBuilder();
        public decimal TotalCost { get; set; }
        public Guid CustomerId { get; set; }

        public ItemProcessor(ICustomerRepository customerRepository, IItemRepository itemRepository)
        {
            _CustomerRepository = customerRepository;
            _ItemRepository = itemRepository;
        }

        public void VisitItem(Item item)
        {
            item.Process();
        }

        public void VisitMembership(Membership membership)
        {
            membership.Process();
            DoesMembershipAlreadyExistsForCustomer(CustomerId, membership);
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
        }

        private void DoesMembershipAlreadyExistsForCustomer(Guid customerID, Membership membership)
        {
            var membershipType = _ItemRepository.GetMembershipById(membership.Id);
            if (_CustomerRepository.DoesCustomerHaveMembership(customerID, membershipType.Id))
                throw new ArgumentException(Message.CustomerAlreadyHaveThisMembership);
        }
        public void OnClose()
        {
            _CustomerRepository.SaveChanges();
        }
    }
}

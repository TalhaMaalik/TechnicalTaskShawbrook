using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext _Context;

        public CustomerRepository(AppDbContext context)
        {
            _Context = context;
        }

        public void CreateCustomer(CustomerModel customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            _Context.Customers.Add(customer);
        }

        public bool DoesCustomerHaveMembership(Guid customerID)
        {
            var membershipRecord = _Context.CustomerMemberships.FirstOrDefault(i => i.CustomerID == customerID);
            if(membershipRecord == null)
                return false;
            else if(membershipRecord.EndDate < DateTime.Now)
                return false;
            return true;
        }

        public CustomerModel GetCustomerById(Guid customerID)
        {
            var customer = _Context.Customers.FirstOrDefault(i => i.Id == customerID);
            if (customer == null)
                throw new ArgumentException(nameof(customer));
            return customer;
        }
        public void AddNewMembership(CustomerMembershipModel customerMembership)
        {
            if (customerMembership == null)
                throw new ArgumentException(nameof(customerMembership));
            _Context.CustomerMemberships.Add(customerMembership);
        }

        public bool SaveChanges()
        {
            return (_Context.SaveChanges() >=0);
        }
    }
}

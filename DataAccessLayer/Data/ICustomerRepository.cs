using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public interface ICustomerRepository
    {
        public CustomerModel GetCustomerById(Guid customerID);

        public bool DoesCustomerHaveMembership(Guid customerID);
        public bool CustomerExists(Guid customerID);

        public void CreateCustomer(CustomerModel customer);

        public void AddNewMembership(CustomerMembershipModel customerMembership);

        public bool SaveChanges();
    }
}

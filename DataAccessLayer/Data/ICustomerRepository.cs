using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public interface ICustomerRepository
    {
        public void CreateCustomer(CustomerModel customer);

        public bool SaveChanges();
    }
}

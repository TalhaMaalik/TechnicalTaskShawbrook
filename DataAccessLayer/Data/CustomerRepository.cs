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

        public bool SaveChanges()
        {
            return (_Context.SaveChanges() >=0);
        }
    }
}

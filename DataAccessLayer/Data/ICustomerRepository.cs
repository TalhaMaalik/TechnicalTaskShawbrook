using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Returns customer model when given valid customer Id, throws ArgumentException when ID is not found.  
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>CustomerModel</returns>
        public CustomerModel GetCustomerById(Guid customerID);
        /// <summary>
        /// Returns true if customer is the request is subscribed to membership Id.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="membershipId"></param>
        /// <returns></returns>
        public bool DoesCustomerHaveMembership(Guid customerID, Guid membershipId);
        /// <summary>
        /// Returns true if customer exists in the database.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool CustomerExists(Guid customerID);
        /// <summary>
        /// Returns true if customer email exists in the database.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CustomerExistsByEmail(string email);
        /// <summary>
        /// Creates new customer entry in the database.
        /// </summary>
        /// <param name="customer"></param>
        public void CreateCustomer(CustomerModel customer);
        /// <summary>
        /// Add new record of subscription in the database. 
        /// </summary>
        /// <param name="customerMembership"></param>
        public void AddNewMembership(CustomerMembershipModel customerMembership);
        /// <summary>
        /// Returns true when changes are successfully saved in the database.
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges();
    }
}

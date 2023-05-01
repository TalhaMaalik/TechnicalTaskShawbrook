using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public interface IItemRepository
    {
        /// <summary>
        /// Returns item model when given valid item Id, throws ArgumentException when ID is not found.  
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>CustomerModel</returns>
        public ItemModel GetItemById(Guid id);
        /// <summary>
        /// Returns membership model when given valid item Id, throws ArgumentException when ID is not found.  
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>ItemModel</returns>
        public MembershipModel GetMembershipByItemId(Guid id);
        /// <summary>
        /// Returns membership model when given valid membership Id, throws ArgumentException when ID is not found.  
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>MembershipModel</returns>
        public MembershipModel GetMembershipById(Guid id);
        /// <summary>
        /// Returns true if the item exists in the database.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>MembershipModel</returns>
        public bool ItemExists(Guid id);
        /// <summary>
        /// Returns true when changes are successfully saved in the database.
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges();
    }
}

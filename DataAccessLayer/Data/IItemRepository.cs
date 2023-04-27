using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public interface IItemRepository
    {
        public ItemModel GetItemById(Guid id);
        public MembershipModel GetMembershipByItemId(Guid id);
        public bool ItemExists(Guid id);
        public bool SaveChanges();
    }
}

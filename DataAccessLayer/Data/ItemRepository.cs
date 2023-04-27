using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public class ItemRepository : IItemRepository
    {
        private AppDbContext _Context;
        public ItemRepository(AppDbContext context)
        {
            _Context = context;
        }

        public ItemModel GetItemById(Guid id)
        {
            var item = _Context.Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                throw new ArgumentException(nameof(item));
            return item;
        }
        public MembershipModel GetMembershipByItemId(Guid id)
        {
            var item = _Context.Memberships.FirstOrDefault(i => i.ItemId == id);
            if (item == null)
                throw new ArgumentException(nameof(item));
            return item;
        }

        public bool SaveChanges()
        {
            return (_Context.SaveChanges() >= 0);
        }
    }
}

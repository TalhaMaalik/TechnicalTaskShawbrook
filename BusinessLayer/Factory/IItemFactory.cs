using BusinessLayer.Models.Base;
using DataAccessLayer.DataModel;

namespace BusinessLayer.Factory
{
    public interface IItemFactory
    {
        public Item CreateItem(Guid itemId);
    }
}

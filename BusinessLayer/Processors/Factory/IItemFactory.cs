using BusinessLayer.Models.Base;
using DataAccessLayer.DataModel;

namespace BusinessLayer.Processors.Factory
{
    public interface IItemFactory
    {
        public Item Create(Guid itemId);
    }
}

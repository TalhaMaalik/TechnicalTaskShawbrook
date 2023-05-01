using BusinessLayer.Models.Base;
using DataAccessLayer.DataModel;

namespace BusinessLayer.Processors.Factory
{
    public interface IItemFactory
    {
        /// <summary>
        /// Creates different subtypes of item based on item type.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>Item</returns>
        public Item Create(Guid itemId);
    }
}

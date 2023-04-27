using BusinessLayer.Models.Base;

namespace BusinessLayer.Models.Items
{
    public class UnknownItem : Item
    {
        public UnknownItem(Guid id, string? name, decimal cost) : base(id, name, cost)
        {

        }
    }
}

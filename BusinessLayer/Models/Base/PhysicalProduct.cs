using BusinessLayer.Processors.Strategies;

namespace BusinessLayer.Models.Base
{
    public abstract class PhysicalProduct : Item
    {
        public PhysicalProduct(Guid id, string? name, decimal cost) : base(id, name, cost)
        {

        }
        public abstract string Process(IShippingSlipStrategy strategy);
    }
}

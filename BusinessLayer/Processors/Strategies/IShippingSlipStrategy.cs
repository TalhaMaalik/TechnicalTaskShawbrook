using BusinessLayer.Models.Base;

namespace BusinessLayer.Processors.Strategies
{
    public interface IShippingSlipStrategy
    {
        public string GenerateShippingSlip(PhysicalProduct product);
    }
}

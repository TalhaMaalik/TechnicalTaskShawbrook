using BusinessLayer.Models.Base;
using Microsoft.Extensions.Hosting;

namespace BusinessLayer.Processors.Strategies
{
    public class KeyValueShippingSlip : IShippingSlipStrategy
    {
        public string GenerateShippingSlip(PhysicalProduct product)
        {
            return $"\"Name\" : {product.Name}, \"Cost\" : {product.Cost} \n";
        }
    }
}

using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using System.Text;

namespace BusinessLayer.Processors.Services
{
    public interface IItemService
    {
        public void ProcessMembership(Membership membership);
        public void ProcessPhysicalProduct(StringBuilder builder, PhysicalProduct product);
        public void ProcessItem(Item item);
    }
}

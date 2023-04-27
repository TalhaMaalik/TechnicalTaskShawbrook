using BusinessLayer.Models.Base;

namespace BusinessLayer.Models.Items
{
    public class Video : Item
    {
        public Video(Guid id, string? name, decimal cost) : base(id, name, cost){}
    }
}

using BusinessLayer.Enums;
using BusinessLayer.Processors.Visitor;

namespace BusinessLayer.Models.Base
{
    public abstract class Item : IElement
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Cost { get; set; }
        public Item(Guid id, string? name, decimal cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }
        public virtual void Accept(IItemVisitor visitor)
        {
            visitor.VisitItem(this);
        }
        public void Process()
        {
            TaskExecution();
        }
        protected virtual void TaskExecution()
        {

        }
    }
}

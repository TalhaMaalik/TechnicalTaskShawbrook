using BusinessLayer.Enums;

namespace BusinessLayer.Models.Base
{
    public abstract class Item
    {
        public Item(Guid id, string? name, decimal cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Cost { get; set; }

        public void Process()
        {
            TaskExecution();
        }

        protected virtual void TaskExecution()
        {

        }
    }
}

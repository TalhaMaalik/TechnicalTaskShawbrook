namespace BusinessLayer.Processors.Visitor
{
    public interface IElement
    {
        void Accept(IItemVisitor visitor);
    }
}

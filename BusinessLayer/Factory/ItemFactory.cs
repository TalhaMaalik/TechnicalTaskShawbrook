using BusinessLayer.Enums;
using BusinessLayer.Models.Base;
using BusinessLayer.Models.Items;
using BusinessLayer.Models.Membership;
using DataAccessLayer.Data;
using DataAccessLayer.DataModel;

namespace BusinessLayer.Factory
{
    public class ItemFactory : IItemFactory
    {
        private readonly IItemRepository _ItemRepository;

        public ItemFactory(IItemRepository repository) 
        { 
            _ItemRepository = repository;
        }
        public Item CreateItem(Guid itemId)
        {
            var item = _ItemRepository.GetItemById(itemId);
            Enum.TryParse(item.Type, out ItemType itemType);
            switch (itemType)
            {
                case ItemType.Membership:
                    return CreateMembership(item);
                case ItemType.Video:
                    return new Video(item.Id, item.Name, item.Cost);
                case ItemType.Book:
                    return new Book(item.Id, item.Name, item.Cost);
                default:
                    return new UnknownItem(item.Id, item.Name, item.Cost);
            }
        }

        protected virtual Membership CreateMembership(ItemModel item)
        {
            var membership = _ItemRepository.GetMembershipByItemId(item.Id);
            Enum.TryParse(membership.Type, out MembershipType membershipType);
            switch (membershipType)
            {
                case MembershipType.BookMembership:
                    return new BookClubMembership(item.Id, item.Name, item.Cost);
                case MembershipType.VideoMembership:
                    return new VideoClubMembership(item.Id, item.Name, item.Cost);
                case MembershipType.PremiumMembership:
                    return new PremiumClubMembership(item.Id, item.Name, item.Cost);
                default:
                    return new UnknownMembership(item.Id, item.Name, item.Cost);
            }
        }
    }
}

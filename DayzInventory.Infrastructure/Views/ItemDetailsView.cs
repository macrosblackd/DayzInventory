using DayzInventory.Infrastructure.Model.Item;

namespace DayzInventory.Infrastructure.Views
{
   public class ItemDetailsView
   {
      public ItemId Id { get; set; }
      public string ItemName { get; set; }
      public int ItemTypeId { get; set; }
   }
}
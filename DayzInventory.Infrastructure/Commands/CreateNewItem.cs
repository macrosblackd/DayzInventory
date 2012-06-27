using DayzInventory.Infrastructure.Model.Item;

namespace DayzInventory.Infrastructure.Commands
{
   public class CreateNewItem : ICommand<ItemId>
   {
      public ItemId Id { get; set; }
      public int ItemTypeId { get; set; }
      public string ItemName { get; set; }
   }
}
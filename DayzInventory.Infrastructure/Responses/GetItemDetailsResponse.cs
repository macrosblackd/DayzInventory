using DayzInventory.Infrastructure.Model.Item;

namespace DayzInventory.Infrastructure.Responses
{
   public class GetItemDetailsResponse
   {
      public string TaskName { get; set; }
      public ItemType ItemType { get; set; }
   }
}
using DayzInventory.Infrastructure.Model;

namespace DayzInventory.Infrastructure.Commands
{
   public class PlaceNewTent : ICommand<TentId>
   {
      public TentId Id { get; set; }
      public string Name { get; set; }
      public string Location { get; set; }
   }
}
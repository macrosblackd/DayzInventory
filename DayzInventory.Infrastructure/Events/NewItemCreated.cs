using System.Runtime.Serialization;
using DayzInventory.Infrastructure.Model.Item;

namespace DayzInventory.Infrastructure.Events
{
   [DataContract(Namespace = "DayzInv")]
   public class NewItemCreated : IEvent<ItemId>
   {
      [DataMember(Order = 1)]public ItemId Id { get; set; }
      [DataMember(Order = 2)]public string ItemName { get; set; }
      [DataMember(Order = 3)]public int ItemTypeId { get; set; }
   }
}
using System.Runtime.Serialization;
using DayzInventory.Infrastructure.Model;

namespace DayzInventory.Infrastructure.Events
{
   [DataContract(Namespace = "DayzInv")]
   public class NewTentPlaced : IEvent<TentId>
   {
      [DataMember(Order = 1)]public TentId Id { get; set; }
      [DataMember(Order = 2)]public string Name { get; set; }
      [DataMember(Order = 2)]public string Location { get; set; }
   }
}
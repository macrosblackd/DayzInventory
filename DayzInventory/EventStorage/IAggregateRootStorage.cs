using System.Collections.Generic;

namespace DayzInventory.EventStorage
{
   public interface IAggregateRootStorage<in TId>
   {
      void Append(TId id, IEnumerable<object> events);
      IEnumerable<object> this[TId id] { get; }
   }
}
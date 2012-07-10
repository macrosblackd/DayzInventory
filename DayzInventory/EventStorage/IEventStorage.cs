using System;

namespace DayzInventory.EventStorage
{
   public interface IEventStorage : IDisposable
   {
      IAggregateRootStorage<TId> GetAggregateRootStore<TAggregateRoot, TId>()
         where TAggregateRoot : AggregateRoot<TId>;
   }
}
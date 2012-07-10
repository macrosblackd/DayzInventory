using System;
using System.Collections.Generic;

namespace DayzInventory.EventStorage
{
   public class EventStorage : IEventStorage
   {
      private readonly Dictionary<Type, dynamic> stores = new Dictionary<Type, dynamic>();

      public IAggregateRootStorage<TId> GetAggregateRootStore<TAggregateRoot, TId>() where TAggregateRoot : AggregateRoot<TId>
      {
         dynamic store;
         if (!stores.TryGetValue(typeof(TAggregateRoot), out store))
         {
            store = new AggregateRootStorage<TId>();
            stores.Add(typeof (TAggregateRoot), store);
         }
         return store;
      }

      public void Dispose()
      {
         stores.Clear();
      }
   }
}
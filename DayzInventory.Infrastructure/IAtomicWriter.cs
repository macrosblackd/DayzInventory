using System;

namespace DayzInventory.Infrastructure
{
   public interface IAtomicWriter<TKey, TView>
   {
      TView AddOrUpdate(TKey key, Func<TView> addFactory, Func<TView, TView> updateFunction);
   }
}
using System;
using System.Collections.Generic;
using DayzInventory.EventStorage;
using DayzInventory.Repository;

namespace DayzInventory.Session
{
   public class SessionFactory : ISessionFactory
   {
      private readonly IEventStorage eventStorage;

      public SessionFactory(IEventStorage eventStorage)
      {
         this.eventStorage = eventStorage;
      }

      #region Implementation of IDisposable

      public void Dispose()
      {
         eventStorage.Dispose();
      }

      public ISession OpenSession()
      {
         return new Session(eventStorage);
      }

      #endregion
   }

   public class Session : ISession
   {
      private readonly IEventStorage eventStorage;
      private readonly HashSet<ISessionItem> enlistedItems = new HashSet<ISessionItem>();

      [ThreadStatic] private static Session current;

      internal Session(IEventStorage eventStorage)
      {
         this.eventStorage = eventStorage;

         if(current != null)
            throw new InvalidOperationException("Cannot nest unit of work");

         current = this;
      }

      private static Session Current
      {
         get { return current; }
      }

      internal static IAggregateRootStorage<TId> Enlist<TId, TAggregateRoot>
         (Repository<TId, TAggregateRoot> repository) where TAggregateRoot : AggregateRoot<TId>
      {
         var unitOfWork = Current;
         unitOfWork.enlistedItems.Add(repository);
         return unitOfWork.eventStorage.GetAggregateRootStore<TAggregateRoot, TId>();
      }

      #region Implementation of IDisposable

      public void Dispose()
      {
         current = null;
      }

      public void SubmitChanges()
      {
         foreach (var enlistedItem in enlistedItems)
            enlistedItem.SubmitChanges();

         enlistedItems.Clear();
      }

      #endregion
   }
}
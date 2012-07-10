using System.Collections.Generic;

namespace DayzInventory
{
   public abstract class AggregateRoot<TId> : IAggregateRoot<TId>
   {
      private readonly UncommittedEvents uncommittedEvents = new UncommittedEvents();

      protected void Replay(IEnumerable<object> events)
      {
         dynamic me = this;
         foreach (var @event in events)
            me.Apply(@event);
      }

      protected void Append(object @event)
      {
         uncommittedEvents.Append(@event);
      }

      #region Implementation of IAggregateRoot<out TId>

      public abstract TId Id { get; }
      IUncommittedEvents IAggregateRoot<TId>.UncommittedEvents { get { return uncommittedEvents; } }

      #endregion
   }
}
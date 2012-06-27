using System;
using System.Collections.Generic;
using DayzInventory.Infrastructure.Commands;
using DayzInventory.Infrastructure.Events;
using DayzInventory.Infrastructure.Helpers;
using DayzInventory.Infrastructure.Model.Item;

namespace DayzInventory.Infrastructure.Aggregates
{
   public class ItemAggregate
   {
      private ItemAggregateState state;
      private Action<IEvent<IIdentity>> observer;

      public ItemAggregate(ItemAggregateState state, Action<IEvent<IIdentity>> observer)
      {
         this.state = state;
         this.observer = observer;
      }

      public void When(CreateNewItem command)
      {
         if (state.Version > 0)
            throw new Exception("Cannot create the same item twice.");

         var @event = new NewItemCreated
            {
               Id = command.Id,
               ItemName = command.ItemName,
               ItemTypeId = command.ItemTypeId
            };

         Apply(@event);
      }

      private void Apply(IEvent<IIdentity> @event)
      {
         state.Apply(@event);
         observer(@event);
      }

      public void Execute(ICommand<IIdentity> command)
      {
         RedirectToWhen.Invoke(this, command);
      }
   }

   public class ItemAggregateState
   {
      public int Version { get; set; }

      public ItemAggregateState(IEnumerable<IEvent<IIdentity>> events)
      {
         foreach(var e in events)
            Apply(e);
      }

      public void Apply(object @event)
      {
         throw new NotImplementedException();
      }
   }
}
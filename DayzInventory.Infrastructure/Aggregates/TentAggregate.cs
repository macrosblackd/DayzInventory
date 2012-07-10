using System;
using System.Collections.Generic;
using DayzInventory.Infrastructure.Commands;
using DayzInventory.Infrastructure.Events;
using DayzInventory.Infrastructure.Helpers;

namespace DayzInventory.Infrastructure.Aggregates
{
   public class TentAggregate
   {
      private TentAggregateState state;
      private Action<IEvent<IIdentity>> observer;

      public TentAggregate(TentAggregateState state, Action<IEvent<IIdentity>> observer)
      {
         this.state = state;
         this.observer = observer;
      }

      public void When(PlaceNewTent command)
      {
         if (state.Version > 0)
            throw new Exception("Cannot create the same item twice.");

         var @event = new NewTentPlaced
            {
               Id = command.Id,
               Location = command.Location,
               Name = command.Name
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

   public class TentAggregateState
   {
      public int Version { get; set; }

      public TentAggregateState(IEnumerable<IEvent<IIdentity>> events)
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
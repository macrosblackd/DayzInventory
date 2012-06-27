using System;
using System.Collections.Generic;
using DayzInventory.Infrastructure.Aggregates;
using DayzInventory.Infrastructure.Model.Item;

namespace DayzInventory.Infrastructure.Factories
{
   public class AggregateFactory : IAggregateFactory
   {
      public void Dispatch(ICommand<IIdentity> command)
      {
         var id = command.Id;
         var events = LoadEventsFromEventStore(id);
         var observer = GetObserver();

         var itemId = id as ItemId;
         if(itemId != null)
         {
            var state = new ItemAggregateState(events);
            var agg = new ItemAggregate(state, observer);
            agg.Execute(command);
            return;
         }

         throw new Exception(
            string.Format("Unexpected aggregate id type '{0}'", id.GetType()));
      }

      private IEnumerable<IEvent<IIdentity>> LoadEventsFromEventStore(IIdentity id)
      {
         throw new NotImplementedException();
      }

      private Action<IEvent<IIdentity>> GetObserver()
      {
         throw new NotImplementedException();
      }
   }
}
using DayzInventory.Infrastructure.Commands;

namespace DayzInventory.Infrastructure.CommandActions
{
   public class PlaceNewTentCommandAction : ICommandAction<PlaceNewTent>
   {
      private IAggregateFactory aggregateFactory;

      public PlaceNewTentCommandAction(IAggregateFactory aggregateFactory)
      {
         this.aggregateFactory = aggregateFactory;
      }

      public void Handle(PlaceNewTent command)
      {
         aggregateFactory.Dispatch(command);
      }
   }
}
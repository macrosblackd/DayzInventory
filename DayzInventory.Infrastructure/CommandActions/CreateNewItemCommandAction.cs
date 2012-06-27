using DayzInventory.Infrastructure.Commands;

namespace DayzInventory.Infrastructure.CommandActions
{
   public class CreateNewItemCommandAction : ICommandAction<CreateNewItem>
   {
      private IAggregateFactory aggregateFactory;

      public CreateNewItemCommandAction(IAggregateFactory aggregateFactory)
      {
         this.aggregateFactory = aggregateFactory;
      }

      public void Handle(CreateNewItem command)
      {
         aggregateFactory.Dispatch(command);
      }
   }
}
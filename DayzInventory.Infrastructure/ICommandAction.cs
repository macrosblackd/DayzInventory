namespace DayzInventory.Infrastructure
{
   public interface ICommandAction<TCommand>
   {
      void Handle(TCommand command);
   }
}
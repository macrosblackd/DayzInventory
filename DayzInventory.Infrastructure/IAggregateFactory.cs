namespace DayzInventory.Infrastructure
{
   public interface IAggregateFactory
   {
      void Dispatch(ICommand<IIdentity> c);
   }
}
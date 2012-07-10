namespace DayzInventory
{
   public interface IAggregateRoot<out TId>
   {
      TId Id { get; }
      IUncommittedEvents UncommittedEvents { get; }
   }
}
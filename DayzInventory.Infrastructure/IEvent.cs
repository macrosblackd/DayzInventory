namespace DayzInventory.Infrastructure
{
   public interface IEvent<out TIdentity> : ICqrsMessage
      where TIdentity : IIdentity
   {
      TIdentity Id { get; }
   }
}
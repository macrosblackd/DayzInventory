namespace DayzInventory.Infrastructure
{
   public interface ICommand<out TIdentity> : ICqrsMessage
      where TIdentity : IIdentity
   {
      TIdentity Id { get; }
   }
}
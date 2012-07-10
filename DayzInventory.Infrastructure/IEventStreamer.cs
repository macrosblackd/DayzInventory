namespace DayzInventory.Infrastructure
{
   public interface IEventStreamer
   {
      byte[] SerializeEvent(IEvent<IIdentity> e);
      IEvent<IIdentity> DeserializeEvent(byte[] buffer);
   }
}
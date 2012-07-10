using System;

namespace DayzInventory.Session
{
   public interface ISession : IDisposable
   {
      void SubmitChanges();
   }
}
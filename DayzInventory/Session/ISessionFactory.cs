using System;
using DayzInventory.Repository;

namespace DayzInventory.Session
{
   public interface ISessionFactory : IDisposable
   {
      ISession OpenSession();
   }
}
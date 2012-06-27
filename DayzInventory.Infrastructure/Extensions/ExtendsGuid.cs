using System;

namespace DayzInventory.Infrastructure.Extensions
{
   public static class ExtendsGuid
   {
       public static string Substring(this Guid guid, int start, int length)
       {
          return guid.ToString().Substring(start, length);
       }
   }
}
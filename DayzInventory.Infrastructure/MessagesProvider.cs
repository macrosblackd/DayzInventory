using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace DayzInventory.Infrastructure
{
   public static class MessagesProvider
   {
      public static Type[] GetKnownEventTypes()
      {
         var types = Assembly
            .GetExecutingAssembly()
            .GetExportedTypes()
            .Where(t => typeof (IEvent<IIdentity>).IsAssignableFrom(t)
                        && t.IsAbstract == false)
            .Union(new[] {typeof (MessageContract)})
            .ToArray();

         return types;
      }
   }
}
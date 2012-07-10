using System;
using System.Runtime.Serialization;

namespace DayzInventory.Infrastructure.Helpers
{
   public static class ExtendsType
   {
       public static string GetContractName(this Type self)
       {
          var attr = (DataContractAttribute) self.GetCustomAttributes(
             typeof (DataContractAttribute), false)[0];
          return string.Format("{0}.{1}", attr.Namespace, self.Name);
       }
   }
}
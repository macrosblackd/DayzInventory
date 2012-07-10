using System;
using DayzInventory.Infrastructure.Extensions;

namespace DayzInventory.Infrastructure.Model
{
   public class TentId : IIdentity
   {
      public Guid Id { get; private set; }
      public string Tag { get { return "item"; } }

      public TentId(Guid id)
      {
         Id = id;
      }

      public override string ToString()
      {
         return string.Format("{0}-{1}", Tag, Id.Substring(0, 6));
      }

      public string GetId()
      {
         return Id.ToString();
      }
   }
}
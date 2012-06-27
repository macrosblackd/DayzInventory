using System;
using DayzInventory.Infrastructure.Extensions;

namespace DayzInventory.Infrastructure.Model.Item
{
   public class ItemId : IIdentity
   {
      public Guid Id { get; private set; }
      public string Tag { get { return "item"; } }

      public ItemId(Guid id)
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
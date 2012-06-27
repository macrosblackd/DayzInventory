using DayzInventory.Infrastructure.Events;
using DayzInventory.Infrastructure.Model.Item;
using DayzInventory.Infrastructure.Views;
using DayzInventory.Infrastructure.Extensions;

namespace DayzInventory.Infrastructure.Projections
{
   public class ItemDetailsProjection
   {
      private IAtomicWriter<ItemId, ItemDetailsView> writer;

      public ItemDetailsProjection(IAtomicWriter<ItemId, ItemDetailsView> writer)
      {
         this.writer = writer;
      }

      public void When(NewItemCreated @event)
      {
         writer.Add(@event.Id, new ItemDetailsView
            {
               Id = @event.Id,
               ItemName = @event.ItemName,
               ItemTypeId = @event.ItemTypeId
            });
      }
   }
}
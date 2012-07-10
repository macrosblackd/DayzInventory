using DayzInventory.Infrastructure.Events;
using DayzInventory.Infrastructure.Model;
using DayzInventory.Infrastructure.Views;
using DayzInventory.Infrastructure.Extensions;

namespace DayzInventory.Infrastructure.Projections
{
   public class TentDetailsProjection
   {
      private IAtomicWriter<TentId, TentDetailView> writer;

      public TentDetailsProjection(IAtomicWriter<TentId, TentDetailView> writer)
      {
         this.writer = writer;
      }

      public void When(NewTentPlaced @event)
      {
         writer.Add(@event.Id, new TentDetailView
            {
               Id = @event.Id,
               Location = @event.Location,
            });
      }
   }
}
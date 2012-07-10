using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DayzInventory
{
   public class UncommittedEvents : IUncommittedEvents
   {
      private readonly List<object> events = new List<object>(); 

      public void Append(object @event)
      {
         events.Add(@event);
      }

      IEnumerator<object> IEnumerable<object>.GetEnumerator()
      {
         return events.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return events.GetEnumerator();
      }

      public bool HasEvents
      {
         get { return events.Any(); }
      }

      public void Commit()
      {
         events.Clear();
      }
   }
}
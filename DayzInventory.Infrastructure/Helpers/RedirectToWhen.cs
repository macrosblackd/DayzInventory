using System.Linq;

namespace DayzInventory.Infrastructure.Helpers
{
   public static class RedirectToWhen
   {
       public static void Invoke(this object self, ICommand<IIdentity> command)
       {
          self.GetType()
             .GetMethods()
             .Where(m => m.Name == "When")
             .Single(m => m.GetParameters()[0].ParameterType == command.GetType())
             .Invoke(self, new object[] {command});
       }
   }
}
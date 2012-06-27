using System;
using System.IO;

namespace DayzInventory.Infrastructure.Persistance
{
   public class FileTapeStream
   {
      private FileInfo fileInfo;

      public FileTapeStream(string fileName)
      {
         fileInfo = new FileInfo(fileName);
      }

      public void Append(byte[] buffer)
      {
         using(var file = OpenForWrite())
         {
            file.Seek(0, SeekOrigin.End);
            using(var ms = new MemoryStream())
            using(var writer = new BinaryWriter(ms))
            {
               writer.Write(buffer);
               ms.Seek(0, SeekOrigin.Begin);
               ms.CopyTo(file);
            }
         }
      }

      private FileStream OpenForWrite()
      {
         return fileInfo.Open(FileMode.OpenOrCreate,
                              FileAccess.ReadWrite,
                              FileShare.Read);
      }
   }
}
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
            var versionToWrite = 1;
            TapeStreamSerializer.WriteRecord(file, buffer, versionToWrite);
         }
      }

      private FileStream OpenForWrite()
      {
         return fileInfo.Open(FileMode.OpenOrCreate,
                              FileAccess.ReadWrite,
                              FileShare.Read);
      }
   }

   public static class TapeStreamSerializer
   {
      public static void WriteRecord(Stream stream, byte[] data, long versionToWrite)
      {
         using(var ms = new MemoryStream())
         using(var writer = new BinaryWriter(ms))
         using(var managed = new SHA1Managed())
         {
            writer.Write(ReadableHeaderStart);
            WriteReadableInt64(writer, data.Length);
            writer.Write(ReadableHeaderEnd);

            writer.Write(data);
            writer.Write(ReadableFooterStart);
            WriteReadableInt64(writer, data.Length);
            WriteReadableInt64(writer, versionToWrite);
            WriteReadableHash(writer, managed.ComputeHash(data));
            writer.Write(ReadableFooterEnd);

            ms.Seek(0, SeekOrigin.Begin);
            ms.CopyTo(stream);
         }
      }

      static void WriteReadableHash(BinaryWriter writer, byte[] hash)
      {
         var buffer = Encoding.UTF8.GetBytes(Convert.ToBase64String(hash));
         writer.Write(buffer);
      }

      private static void WriteReadableInt64(BinaryWriter writer, long value)
      {
         var buffer = Encoding.UTF8.GetBytes(value.ToString("x16"));
         writer.Write(buffer);
      }
   }
}
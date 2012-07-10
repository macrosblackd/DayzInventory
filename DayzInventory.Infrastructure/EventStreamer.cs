using System;
using System.IO;
using System.Runtime.Serialization;

namespace DayzInventory.Infrastructure
{
   public class EventStreamer : IEventStreamer
   {
      private readonly EventSerializer serializer;

      public EventStreamer(EventSerializer serializer)
      {
         this.serializer = serializer;
      }

      public byte[] SerializeEvent(IEvent<IIdentity> e)
      {
         byte[] content;
         using(var ms = new MemoryStream())
         {
            serializer.Serialize(e, e.GetType(), ms);
            content = ms.ToArray();
         }

         byte[] messageContractBuffer;
         using(var ms = new MemoryStream())
         {
            var name = e.GetType().Name;
            var messageContract = new MessageContract(name, content.Length, 0);
            serializer.Serialize(messageContract, typeof (MessageContract), ms);
            messageContractBuffer = ms.ToArray();
         }

         using(var ms = new MemoryStream())
         {
            var headerContract = new MessageHeaderContract(messageContractBuffer.Length);
            headerContract.WriteHeader(ms);
            ms.Write(messageContractBuffer, 0, messageContractBuffer.Length);
            ms.Write(content, 0, content.Length);
            return ms.ToArray();
         }
      }

      public IEvent<IIdentity> DeserializeEvent(byte[] buffer)
      {
         using (var ms = new MemoryStream(buffer))
         {
            var header = MessageHeaderContract.ReadHeader(buffer);
            ms.Seek(MessageHeaderContract.FixedSize, SeekOrigin.Begin);

            var headerBuffer = new byte[header.HeaderBytes];
            ms.Read(headerBuffer, 0, (int) header.HeaderBytes);
            var contract = (MessageContract) serializer.Deserialize(
               new MemoryStream(headerBuffer), typeof (MessageContract));

            var contentBuffer = new byte[contract.ContentSize];
            ms.Read(contentBuffer, 0, (int) contract.ContentSize);
            var contentType = serializer.GetContentType(contract.ContractName);
            var @event = (IEvent<IIdentity>) serializer.Deserialize(
               new MemoryStream(contentBuffer), contentType);
            return @event;
         }
      }
   }

   public sealed class MessageContract
   {
      [DataMember(Order = 1)] public readonly string ContractName;
      [DataMember(Order = 2)] public readonly long ContentSize;
      [DataMember(Order = 3)] public readonly long ContentPosition;

      private MessageContract() { }

      public MessageContract(string contractName, long contentSize, long contentPosition)
      {
         ContractName = contractName;
         ContentSize = contentSize;
         ContentPosition = contentPosition;
      }
   }

   public sealed class MessageHeaderContract
   {
      public static int FixedSize = 8;
      public readonly long HeaderBytes;

      public MessageHeaderContract(long headerBytes)
      {
         HeaderBytes = headerBytes;
      }

      public static MessageHeaderContract ReadHeader(byte[] buffer)
      {
         var headerBytes = BitConverter.ToInt64(buffer, 0);
         return new MessageHeaderContract(headerBytes);
      }

      public void WriteHeader(Stream stream)
      {
         stream.Write(BitConverter.GetBytes(HeaderBytes), 0, 8);
      }
   }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DayzInventory.Infrastructure.Helpers;
using ProtoBuf.Meta;

namespace DayzInventory.Infrastructure
{
   public class EventSerializer
   {
      private readonly IDictionary<Type, Formatter> type2Contract = new Dictionary<Type, Formatter>();
      private readonly IDictionary<string, Type> contractName2Type = new Dictionary<string, Type>();

      protected sealed class Formatter
      {
         public Action<object, Stream> SerializeDelegate;
         public Func<Stream, object> DeserializerDelegate;
         public string ContractName;

         public Formatter(string name, Func<Stream, object> deserializerDelegate, Action<object, Stream> serializeDelegate)
         {
            ContractName = name;
            DeserializerDelegate = deserializerDelegate;
            SerializeDelegate = serializeDelegate;
         }
      }

      public EventSerializer(IEnumerable<Type> knownEventTypes)
      {
         type2Contract = knownEventTypes
            .ToDictionary(t => t,
                          t =>
                          {
                             var formatter = RuntimeTypeModel.Default.CreateFormatter(t);
                             return new Formatter(ExtendsType.GetContractName(t), formatter.Deserialize,
                                                  (o, stream) => formatter.Serialize(stream, o));
                          });
         contractName2Type = knownEventTypes
            .ToDictionary(t => t.GetContractName(),
                          t => t);
      }

      public void Serialize(object instance, Type type, Stream destinationStream)
      {
         Formatter formatter;
         if(!type2Contract.TryGetValue(type, out formatter))
         {
            var s = string.Format(
               "Can't find serializer for unknown object type '{0}'." +
               "Have you passed all known types to the contructor?",
               instance.GetType());
            throw new InvalidOperationException(s);
         }
         formatter.SerializeDelegate(instance, destinationStream);
      }

      public Type GetContentType(string contractName)
      {
         return contractName2Type[contractName];
      }

      public object Deserialize(Stream sourceStream, Type type)
      {
         Formatter value;
         if (!type2Contract.TryGetValue(type, out value))
         {
            var s = string.Format(
               "Can't find formatter for unknown object type '{0}'." +
               "Have you passed all known types to the contructor?",
               type);
            throw new InvalidOperationException(s);
         }
         return value.DeserializerDelegate(sourceStream);
      }
   }
}
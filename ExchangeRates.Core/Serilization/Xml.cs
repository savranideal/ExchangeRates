using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ExchangeRates.Core
{
    /// <summary>
    /// XmlSerializer için yardımcı metodlar.
    /// </summary>
    public static class Xml
    {
        private class XmlSerializerFactory<T>
        {
            public static readonly XmlSerializer Serializer = new XmlSerializer(typeof(T));
            private static readonly ConcurrentDictionary<string, Lazy<XmlSerializer>> _serializerByNamespace = new ConcurrentDictionary<string, Lazy<XmlSerializer>>();
            private static readonly ConcurrentDictionary<string, Lazy<XmlSerializer>> _serializerByXmlRootAttribute = new ConcurrentDictionary<string, Lazy<XmlSerializer>>();

            public static XmlSerializer GetSerializerByNamespace(string nameSpace)
            {
                return _serializerByNamespace.GetOrAdd($"{nameSpace}_{typeof(T).AssemblyQualifiedName}",
                    _ => new Lazy<XmlSerializer>(() => new XmlSerializer(typeof(T), nameSpace))).Value;
            }

            public static XmlSerializer GetSerializerByRootAttribute(string rootAttribute)
            {
                return _serializerByXmlRootAttribute.GetOrAdd($"{rootAttribute}_{typeof(T).AssemblyQualifiedName}",
                    _ => new Lazy<XmlSerializer>(() => new XmlSerializer(typeof(T), new XmlRootAttribute(rootAttribute)))).Value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <param name="indent"></param>
        /// <param name="suppressErrors"></param>
        /// <returns></returns>
        public static string Serialize<T>(T objectToSerialize, bool indent, bool suppressErrors = true)
        {
            return Serialize(objectToSerialize, new XmlWriterSettings { Indent = indent, Encoding = new UTF8Encoding(false) }, suppressErrors: suppressErrors);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <param name="xmlSettings"></param>
        /// <param name="suppressErrors"></param>
        /// <returns></returns>
        public static string Serialize<T>(T objectToSerialize, XmlWriterSettings xmlSettings = null, bool suppressErrors = true)
        {
            try
            {
                var serializer = XmlSerializerFactory<T>.Serializer;
                if (xmlSettings == null)
                {
                    xmlSettings = new XmlWriterSettings { Encoding = new UTF8Encoding(false) };
                }

                if (xmlSettings.Encoding == null)
                    xmlSettings.Encoding = new UTF8Encoding(false);

                using (var buffer = new MemoryStream())
                {
                    using (var stream = new StreamWriter(buffer, xmlSettings.Encoding))
                    {
                        using (var writer = XmlWriter.Create(stream, xmlSettings))
                        {
                            serializer.Serialize(writer, objectToSerialize);
                        }
                    }
                    return Encoding.UTF8.GetString(buffer.ToArray());
                }
            }
            catch (Exception e)
            {
                if (!suppressErrors)
                    ExceptionDispatchInfo.Capture(e).Throw();
                return "";
            }
        }
         
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="suppressErrors"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml, bool suppressErrors = true)
        {
            try
            {
                var serializer = XmlSerializerFactory<T>.Serializer;
                using (var reader = new StringReader(xml))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                if (!suppressErrors)
                    ExceptionDispatchInfo.Capture(e).Throw();
                return default(T);
            }
        }
         
    }
    }

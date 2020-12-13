using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace ExchangeRates.Core
{
    public static partial class Json
    {
        /// <summary>
        /// Json serialize işlemi için yardımcı metod.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <param name="preserveTypeInfo">Polimorfik objeler için kullanılır genelde. $type ile objenin tipini belirtir.</param>
        /// <param name="converters">Ekstra çeviriciler.</param>
        /// <param name="indent">Json görsel olarak düzgün çıkmasını sağlar.</param>
        /// <returns></returns>
        public static string Serialize<T>(T objectToSerialize,
            bool preserveTypeInfo = false,
            List<JsonConverter> converters = null,
            bool indent = false,
            ReferenceLoop referenceLoopHandling = ReferenceLoop.Ignore)
        {
            return JsonConvert.SerializeObject(objectToSerialize, PrepareSettings(preserveTypeInfo, converters, indent, referenceLoopHandling));
        }

        /// <summary>
        /// Json serialize işlemi için yardımcı metod.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string Serialize<T>(T objectToSerialize,
            JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(objectToSerialize, settings);
        }
        /// <summary>
        /// Json deserialize işlemi için yardımcı metod.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="suppressErrors">Deserialize işleminde çıkacak hatalar ezilir ve default değer döner. Eğer hatalar fırlatılsın isterseniz bunu false'a çekiniz.</param>
        /// <param name="preserveTypeInfo">Polimorfik objeler için kullanılır genelde. $type ile objenin tipini belirtir.</param>
        /// <param name="converters">Ekstra çeviriciler.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json,
            bool suppressErrors = true,
            bool preserveTypeInfo = false,
            List<JsonConverter> converters = null)
        {
            var settings = PrepareSettings(preserveTypeInfo, converters, false);

            try
            {
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception e)
            {
                if (!suppressErrors)
                    ExceptionDispatchInfo.Capture(e).Throw();
                return default(T);
            }
        }

        internal static JsonSerializerSettings PrepareSettings(bool preserveTypeInfo, List<JsonConverter> converters, bool indent = false,
          ReferenceLoop referenceLoopHandling = ReferenceLoop.Ignore)
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                Converters = converters ?? new List<JsonConverter>(),
                Formatting = indent ? Formatting.Indented : Formatting.None,
                ReferenceLoopHandling = (ReferenceLoopHandling)(int)referenceLoopHandling,
            };

            if (preserveTypeInfo)
            {
                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                settings.TypeNameHandling = TypeNameHandling.Auto;
                //bunun ile $type etiketi eklenip polimorfik objelere izin veriliyor.
                settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            }
            return settings;
        }

        public enum ReferenceLoop
        {
            Error = ReferenceLoopHandling.Error,
            Ignore = ReferenceLoopHandling.Ignore,
            Serialize = ReferenceLoopHandling.Serialize
        }
    }
}

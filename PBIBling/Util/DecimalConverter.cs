using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBIBling.Util
{
    public class DecimalConverter : JsonConverter
    {

        //public override bool CanConvert(Type objectType)
        //{
        //    return objectType == typeof(DateTime);
        //}

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    if (reader.Value != null && reader.Value.ToString().IndexOf("-").Equals(-1))
        //    {
        //        var t = long.Parse(reader.Value.ToString());
        //        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(t);
        //    }
        //    else
        //        return Convert.ToDateTime(reader.Value);
        //}

        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    throw new NotImplementedException();
        //}

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(decimal?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && string.IsNullOrEmpty(reader.Value.ToString()))
            {
                // customize this to suit your needs
                return 0;
            }
            else
            {
                decimal resultado = 0;
                decimal.TryParse(reader.Value.ToString(), out resultado);
                return resultado;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

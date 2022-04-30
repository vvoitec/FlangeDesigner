using System;
using FlangeDesigner.AbstractEngine;
using Newtonsoft.Json;

namespace FlangeDesigner.Main.Infrastructure
{
    public class LengthJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var value = (int)(long)reader.Value;

            return Length.of(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Length);
        }
    }
}
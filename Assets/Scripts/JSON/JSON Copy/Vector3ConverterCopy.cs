using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

public class Vector3ConverterCopy : JsonConverter<Vector3>
{
    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Vector3 v = Vector3.zero;

        JObject jobj = JObject.Load(reader);
        v.x = (float)jobj["X"];
        v.y = (float)jobj["Y"];
        v.z = (float)jobj["Z"];

        return v;
    }

    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("X");
        writer.WriteValue(value.x);

        writer.WritePropertyName("Y");
        writer.WriteValue(value.y);

        writer.WritePropertyName("Z");
        writer.WriteValue(value.z);

        writer.WriteEndObject();
    }
}
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GPTOverflow.Core.CrossCuttingConcerns.Utils;

internal class ApplicationCustomJsonConverter<T> : JsonConverter<T> where T : class
{

    public override void Write(Utf8JsonWriter writer, T item, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var prop in item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            writer.WriteString(prop.Name, prop.GetValue(item)?.ToString());
        }
        writer.WriteEndObject();
    }
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //Intentionally not implemented
        throw new NotImplementedException();
    }
}

public static class AppJsonUtils
{
    public static string Serialize<T>(T instance,bool useDefault = false) where T : class
    {
        // var options = new JsonSerializerOptions()
        // {
        //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //     DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        //     WriteIndented = true,
        //     DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        //     Converters =
        //     {
        //         new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
        //         new ApplicationCustomJsonConverter<T>()
        //     }
        // };

        // return JsonSerializer.Serialize(instance, useDefault? null :options);
        return JsonSerializer.Serialize(instance);
    }


    public static T? Deserialize<T>(string payload) where T : class
    {

        return JsonSerializer.Deserialize<T>(payload);
    }

    public static T? Deserialize<T>(byte[] payload) where T : class
    {
        return JsonSerializer.Deserialize<T>(payload);
    }
}
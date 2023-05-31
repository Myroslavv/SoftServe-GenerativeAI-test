using System.Text.Json;
using System.Text.Json.Serialization;

namespace CountriesProxy.API
{
    public class Country
    {
        [JsonPropertyName("name")]
        [JsonConverter(typeof(CountryNameConverter))]
        public string Name { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }
    }

    public class CountryNameConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);

            if (document.RootElement.TryGetProperty("name", out var nameProperty))
            {
                if (nameProperty.TryGetProperty("common", out var commonProperty))
                {
                    return commonProperty.GetString();
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

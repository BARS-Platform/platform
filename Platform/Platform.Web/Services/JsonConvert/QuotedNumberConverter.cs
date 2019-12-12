using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Platform.Web.Services.JsonConvert
{
    public class QuotedNumberConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out var result))
                {
                    return result;
                }
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(null, CultureInfo.InvariantCulture));
        }
    }
}
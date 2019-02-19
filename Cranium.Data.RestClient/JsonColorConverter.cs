﻿using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Globalization;

namespace Cranium.Data.RestClient
{
    public class JsonColorConverter : JsonConverter<Color>
    {
        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var i = int.Parse(((string)reader.Value).Replace("#", ""), NumberStyles.AllowHexSpecifier);
            return Color.FromArgb(i);
        }

        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            writer.WriteValue($"#{value.A:X2}{value.R:X2}{value.G:X2}{value.B:X2}");
        }
    }
}

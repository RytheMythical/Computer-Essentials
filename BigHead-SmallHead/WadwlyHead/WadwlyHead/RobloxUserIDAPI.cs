// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using RobloxUserIDAPI;
//
//    var robloxUserId = RobloxUserId.FromJson(jsonString);

namespace RobloxUserIDAPI
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RobloxUserId
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("requestedUsername")]
        public string RequestedUsername { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }

    public partial class RobloxUserId
    {
        public static RobloxUserId FromJson(string json) => JsonConvert.DeserializeObject<RobloxUserId>(json, RobloxUserIDAPI.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RobloxUserId self) => JsonConvert.SerializeObject(self, RobloxUserIDAPI.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
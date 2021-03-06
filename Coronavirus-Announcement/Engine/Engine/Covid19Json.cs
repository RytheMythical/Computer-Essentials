// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Covid19API;
//
//    var covid19 = Covid19.FromJson(jsonString);

namespace Covid19API
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Covid19
    {
        [JsonProperty("ConfirmedCases")]
        public string ConfirmedCases { get; set; }

        [JsonProperty("CriticalCases")]
        public string CriticalCases { get; set; }

        [JsonProperty("Deaths")]
        public string Deaths { get; set; }

        [JsonProperty("RecoveredCases")]
        public string RecoveredCases { get; set; }

        [JsonProperty("ActiveCases")]
        public string ActiveCases { get; set; }
    }

    public partial class Covid19
    {
        public static Covid19 FromJson(string json) => JsonConvert.DeserializeObject<Covid19>(json, Covid19API.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Covid19 self) => JsonConvert.SerializeObject(self, Covid19API.Converter.Settings);
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
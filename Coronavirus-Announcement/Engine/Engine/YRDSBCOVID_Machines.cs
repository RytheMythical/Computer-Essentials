﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using YRDSBCOVIDAPI;
//
//    var yrdsbcovid = Yrdsbcovid.FromJson(jsonString);

namespace YRDSBCOVIDAPI
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Yrdsbcovid
    {
        [JsonProperty("school")]
        public School[] School { get; set; }
    }

    public partial class School
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cases", NullValueHandling = NullValueHandling.Ignore)]
        public string Cases { get; set; }

        [JsonProperty("closure_status", NullValueHandling = NullValueHandling.Ignore)]
        public string ClosureStatus { get; set; }
    }

    public partial class Yrdsbcovid
    {
        public static Yrdsbcovid FromJson(string json) => JsonConvert.DeserializeObject<Yrdsbcovid>(json, YRDSBCOVIDAPI.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Yrdsbcovid self) => JsonConvert.SerializeObject(self, YRDSBCOVIDAPI.Converter.Settings);
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
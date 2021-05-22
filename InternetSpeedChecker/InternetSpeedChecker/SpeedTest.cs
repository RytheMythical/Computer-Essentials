﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using SpeedTestAPI;
//
//    var speedTest = SpeedTest.FromJson(jsonString);

namespace SpeedTestAPI
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Microsoft.Azure.Mobile.Server;

    public partial class SpeedTest : EntityData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("ping")]
        public Ping Ping { get; set; }

        [JsonProperty("download")]
        public Load Download { get; set; }

        [JsonProperty("upload")]
        public Load Upload { get; set; }

        [JsonProperty("packetLoss")]
        public long PacketLoss { get; set; }

        [JsonProperty("isp")]
        public string Isp { get; set; }

        [JsonProperty("interface")]
        public Interface Interface { get; set; }

        [JsonProperty("server")]
        public Server Server { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    public partial class Load : EntityData
    {
        [JsonProperty("bandwidth")]
        public long Bandwidth { get; set; }

        [JsonProperty("bytes")]
        public long Bytes { get; set; }

        [JsonProperty("elapsed")]
        public long Elapsed { get; set; }
    }

    public partial class Interface : EntityData
    {
        [JsonProperty("internalIp")]
        public string InternalIp { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("macAddr")]
        public string MacAddr { get; set; }

        [JsonProperty("isVpn")]
        public bool IsVpn { get; set; }

        [JsonProperty("externalIp")]
        public string ExternalIp { get; set; }
    }

    public partial class Ping : EntityData
    {
        [JsonProperty("jitter")]
        public double Jitter { get; set; }

        [JsonProperty("latency")]
        public double Latency { get; set; }
    }

    public partial class Result : EntityData
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Server : EntityData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("port")]
        public long Port { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }
    }

    public partial class SpeedTest : EntityData
    {
        public static SpeedTest FromJson(string json) => JsonConvert.DeserializeObject<SpeedTest>(json, SpeedTestAPI.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SpeedTest self) => JsonConvert.SerializeObject(self, SpeedTestAPI.Converter.Settings);
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
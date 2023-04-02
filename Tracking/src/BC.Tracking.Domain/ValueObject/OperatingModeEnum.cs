using System.Text.Json.Serialization;

namespace Connexity.BC.Tracking.Domain.ValueObject
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OperatingMode : byte
    {
        MidnightMidnight = 0,
        NoonNoon = 1,
    }
}
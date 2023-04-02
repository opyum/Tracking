using System.Text.Json.Serialization;

namespace Connexity.BC.Tracking.Domain.ValueObject
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ExceptionType
    {
        Fixed = 0,
        Calendar = 1,
    }
}
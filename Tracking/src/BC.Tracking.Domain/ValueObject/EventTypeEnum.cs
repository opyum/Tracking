using System.Text.Json.Serialization;

namespace Connexity.BC.Tracking.Domain.ValueObject
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventType
    {
        CREATED = 0,
        NOTCREATED = 1,
        UPDATED = 2,
        NOTUPDATED = 3,
        DELETED = 4,
        NOTDELETED = 5,
        CREATING = 6,
        UPDATING = 7,
        DELETING = 8,
    }
}
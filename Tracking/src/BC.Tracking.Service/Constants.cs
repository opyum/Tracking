namespace Connexity.BC.Tracking.Service
{
    internal static class Constants
    {
        internal const string AppId = "BC-Tracking";

        internal static class Routes
        {
            internal const string Api = "api";

            internal const string ApiV1 = Api + "/v1";

            internal const string Trackings = "dimming-calendars";

            internal const string TrackingsV1 = ApiV1 + "/" + Trackings;

            internal const string TrackingsEventHandler = ApiV1 + "/dimming-calendars-event-handler";

            internal const string Creating = "/creating";

            internal const string TrackingChangeRequestedCreating = "change-requested" + Creating;

            internal const string Errors = "/errors";
        }

        internal static class PubSubNames
        {
            internal const string PubSub1 = "pubsub1";
        }

        internal static class TopicNames
        {
            internal const string EnergyTrackingChanged = "energy-Trackingchanged";

            internal const string DimmingPrograms_TrackingChangeRequested = "dimmingprograms-Trackingchangerequested";

            internal const string Trackings_TrackingChangeRequested = "Trackings-Trackingchangerequested";
            internal const string Vch_HistorizeItemRequested = "historize-item-requested";
        }
    }
}
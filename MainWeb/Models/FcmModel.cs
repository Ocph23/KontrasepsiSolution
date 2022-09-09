using Newtonsoft.Json;

namespace MainWeb.Models
{
    public class FcmModel
    {
    }

    public class FcmNotificationSetting
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
    }

    public class NotificationModel
    {
        public string DeviceId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
    public class GoogleNotification
    {
        public class DataPayload
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }
        public string Priority { get; set; } = "high";
        public DataPayload Data { get; set; }
        public DataPayload Notification { get; set; }
    }

    public class ResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

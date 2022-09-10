using MainWeb.Models;
using Microsoft.Extensions.Options;
using Radzen;
using static MainWeb.Models.GoogleNotification;
using System.Net.Http.Headers;
using System.Runtime;
using CorePush.Google;
using CorePush.Utils;
using System.Text;

namespace MainWeb.Services
{
    public class FcmNotificationService : IFcmNotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public FcmNotificationService(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
        }
        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                FcmSettings settings = new FcmSettings()
                {
                    SenderId = _fcmNotificationSetting.SenderId,
                    ServerKey = _fcmNotificationSetting.ServerKey
                };
                HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("https://fcm.googleapis.com/fcm/send") };
                string authorizationKey = string.Format("key={0}", settings.ServerKey);
                string deviceToken = notificationModel.DeviceId;
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                DataPayload dataPayload = new DataPayload();
                dataPayload.Image = "https://jateng.bkkbn.go.id/wp-content/uploads/2020/09/cropped-LOGO-BKKBN-2020-KUPU2.png";
                dataPayload.IsScheduled = true;

                dataPayload.ScheduledTime = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:MM:ss");

                dataPayload.Title = notificationModel.Title;
                dataPayload.Body = notificationModel.Body;
                GoogleNotification notification = new GoogleNotification();
                notification.Notification = dataPayload;
                notification.To= deviceToken;


                string content = JsonHelper.Serialize(notification);
                var message = new StringContent(content, Encoding.UTF8, "application/json");
                var fcm = new FcmSender(settings, httpClient);
                var fcmSendResponse = await httpClient.PostAsync("",message);

                if (fcmSendResponse.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    response.Message = "Notification sent successfully";
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    //response.Message = fcmSendResponse.Results[0].Error;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }

    public interface IFcmNotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}

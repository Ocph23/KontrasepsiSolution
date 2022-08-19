using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


namespace MainWeb.Services;


public class EmailSender : IEmailSender
    {
        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // public Task SendEmailAsync(string email, string subject, string htmlMessage)
        // {
        //     try
        //     {
        //         IConfigurationSection emailConfiguration =Configuration.GetSection("MailSettings");
        //         var from = emailConfiguration["Mail"].ToString();
        //         MailMessage message = new MailMessage(from, email);
        //         message.Subject = subject;
        //         message.Body = htmlMessage;
        //         message.IsBodyHtml = true;

        //         message.From = new MailAddress(from, emailConfiguration["DisplayName"]);
        //         SmtpClient smtp = new SmtpClient(emailConfiguration["HOST"], Convert.ToInt32(emailConfiguration["Port"]));
        //         smtp.Credentials = new NetworkCredential(from, emailConfiguration["Password"]);
        //         smtp.EnableSsl = true;
        //         smtp.Send(message);



        //         return Task.FromResult(true);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
        //    ex.ToString());
        //         throw new SystemException(ex.Message);
        //     }
        // }


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                IConfigurationSection emailConfiguration = Configuration.GetSection("MailSettings");
                MailjetClient client = new(emailConfiguration["Mail"], emailConfiguration["Password"]);
                MailjetRequest request = new MailjetRequest
                {
                    Resource = SendV31.Resource,
                }
                 .Property(Send.Messages,
                    new JArray {
                         new JObject {
                          {
                           "From",
                           new JObject {
                            {"Email", emailConfiguration["Email"]},
                            {"Name", emailConfiguration["DisplayName"]}
                           }
                          }, {
                           "To",
                           new JArray {
                            new JObject {
                             {
                              "Email",
                              email
                             }, {
                              "Name",
                              email
                             }
                            }
                           }
                          }, {
                           "Subject",
                           subject
                          }, {
                           "TextPart",
                           "None"
                          }, {
                           "HTMLPart",
                           htmlMessage
                          }, {
                           "CustomID",
                           "AppGettingStartedTest"
                          }
                         }
                 });
                MailjetResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Debug.WriteLine(response.GetData());
                }
                else
                {
                    Debug.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                    Debug.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                    Debug.WriteLine(response.GetData());
                    Debug.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                }

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("Exception caught in CreateTestMessage2(): {0}",
           ex.ToString());
                throw new SystemException(ex.Message);
            }
        }
    }
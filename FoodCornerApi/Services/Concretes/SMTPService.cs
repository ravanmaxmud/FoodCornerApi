//using Microsoft.Extensions.Options;
//using MimeKit;
//using MailKit.Net.Smtp;
//using FoodCornerApi.Options;
//using FoodCornerApi.Services.Abstracts;
//using FoodCornerApi.Contracts.Email;

//namespace FoodCornerApi.Services.Concretes
//{
//    public class SMTPService : IEmailService
//    {
//        private EmailConfigOptions _emailConfig;

//        public SMTPService(IOptions<EmailConfigOptions> emailConfigOptions)
//        {
//            _emailConfig = emailConfigOptions.Value;
//        }

//        public void Send(MessageDto message)
//        {
//            var emailMessage = CreateEmailMessage(message);
//            Send(emailMessage);
//        }

//        private MimeMessage CreateEmailMessage(MessageDto message)
//        {
//            var bodyBuilder = new BodyBuilder();
//            bodyBuilder.HtmlBody = message.Content;


//            var emailMessage = new MimeMessage();
//            emailMessage.From.Add(new MailboxAddress(string.Empty, _emailConfig.From));
//            emailMessage.To.AddRange(message.To);
//            emailMessage.Subject = message.Subject;
//            emailMessage.Body = bodyBuilder.ToMessageBody();
//            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
//            //{
//            //    Text = message.Content
//            //};
//            return emailMessage;
//        }

//        private void Send(MimeMessage emailMessage)
//        {
//            using (var client = new SmtpClient())
//            {
//                try
//                {
//                    client.Connect(_emailConfig.SmptServer, _emailConfig.Port, true);
//                    client.AuthenticationMechanisms.Remove("XOAUTH2");
//                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

//                    client.Send(emailMessage);
//                }
//                catch
//                {

//                    throw;
//                }
//                finally
//                {
//                    client.Disconnect(true);
//                    client.Dispose();
//                }
//            }
//        }
//    }
//}

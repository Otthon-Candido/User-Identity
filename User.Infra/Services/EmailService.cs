using System.Text.RegularExpressions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Utils;
using User.Domain.Models;
using User.Infra.Exceptions;

namespace User.Infra.Services
{

    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string[] recipient, string about, string userId, string token, string type)
        {
            string? content = null;
            if (type == "create")
            {
                content = $"http://localhost:4200/#/active?userId={userId}&activateCode={token}";

            }
            else if (type == "resetPassword")
            {

                token = Regex.Replace(token, @"\+", "%2B");
                content = $"http://localhost:4200/#/resetPassword?userId={userId}&resetToken={token}";
            }
            if (content != null)
            {
                Message message = new(recipient, about, userId, token, content);
                var messageEmail = BodyEmail(message, type);
                SendEmailFinal(messageEmail);
            }
            else
            {
                throw new BadRequestException("Erro ao enviar email para resetar senha");
            }

        }

        private void SendEmailFinal(MimeMessage messageEmail)
        {
            using (var client = new SmtpClient())
            {

                try
                {
                    client.Connect(_configuration["EmailSettings:SmtpServer"],
                    int.Parse(_configuration["EmailSettings:Port"]), true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
                    client.Send(messageEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        public MimeMessage BodyEmail(Message message, string type)
        {

            var messageEmail = new MimeMessage();
            var builder = new BodyBuilder();
            var Recipient = new List<MailboxAddress>();
            messageEmail.From.Add(new MailboxAddress(_configuration["EmailSettings:From"]));
            Recipient.AddRange(message.Recipient.Select(d => new MailboxAddress(d)));
            messageEmail.To.AddRange(Recipient);
            messageEmail.Subject = message.About;
            var image = builder.LinkedResources.Add(@"C:\Users\Otthon\dotNet.png");
            image.ContentId = MimeUtils.GenerateMessageId();
            if (type == "create")
            {
                builder.HtmlBody = string.Format(@" 
            <div style = 'display: grid;width: 100%;'>
                <img  style = 'display: block;margin:auto;' src=""cid:{0}""> 
                <p style = 'font-size: 15px;display: inline; margin: auto;'> Quase lá... </p> 
                <div style = 'display: grid; width:300px; height:55px; background-color: blue;margin:auto;border-radius: 15px;font-size: 16px'> 
                     <a style = 'display: grid; width:auto; height:auto; background-color: blue;font-size: 16px;color: white;margin-top: auto;margin-bottom: auto;border-radius: 9px;margin-left: auto;margin-right: auto;text-decoration: none' href={1}>
                        Clique aqui e confirme seu cadastro
                    </a> 
                </div> 
                <div>
                <p style='color:red'> Caso deseconheça ignore esse e-mail</p>
                </div> 
            </div> "
                , image.ContentId, message.Content);
                messageEmail.Body = builder.ToMessageBody();
            }
            else
            {
                builder.HtmlBody = string.Format(@" 
            <div style = 'display: grid;width: 100%;'>
                <img  style = 'display: block;margin:auto;' src=""cid:{0}""> 
                <p style = 'font-size: 15px;display: inline; margin: auto;'> Quase lá... </p> 
                <div style = 'display: grid; width:300px; height:55px; background-color: blue;margin:auto;border-radius: 15px;font-size: 16px'> 
                     <a style = 'display: grid; width:auto; height:auto; background-color: blue;font-size: 16px;color: white;margin-top: auto;margin-bottom: auto;border-radius: 9px;margin-left: auto;margin-right: auto;text-decoration: none' href={1}>
                        Clique aqui para alterar senha
                    </a> 
                </div> 
                <div>
                <p style='color:red'> Caso deseconheça ignore esse e-mail</p>
                </div> 
            </div> "
                , image.ContentId, message.Content);
                messageEmail.Body = builder.ToMessageBody();
            }
            return messageEmail;

        }
    }

}
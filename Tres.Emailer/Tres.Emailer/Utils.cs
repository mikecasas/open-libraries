using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using Tres.Emailer.Models;

namespace Tres.Emailer
{
    public class Utils
    {
        public static MimeMessage CreateMessage(string subject, string body, IEnumerable<MailboxAddress> toMailboxes, 
            MailboxAddress fromAddress)
        {
            var m = new MimeMessage();
            
            m.From.Add(fromAddress);

            foreach(var i in toMailboxes)
            {
                m.To.Add(i);
            }
                      
            m.Subject = subject;

            var builder = new BodyBuilder();
            builder.TextBody = body;

            //message.Body = new TextPart("plain")
            //{
            //    Text = confirmMessage + ". Processed on " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString()
            //};


            //    We may also want to attach a calendar event for Monica's party...
            //    builder.Attachments.Add(@"C:\Users\mc\Documents\test.txt");

            m.Body = builder.ToMessageBody();

            return m;
        }

        public static void SendEmailToGoogleServer(MimeMessage message, UserCredentials user)
        {                     
            SendSmtpEmailToServer(message, Config.GoogleSmtp(),user);
        }

        internal static IEnumerable<MimeMessage> GetEmailFromGoogleServer(UserCredentials user)
        {
            return GetEmailFromInBox(Config.ImapServer(), user);
        }

        private static void SendSmtpEmailToServer(MimeMessage message, MailServiceConnectionConfig config, UserCredentials user)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(config.Host, config.Port, config.UseSsl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(user.UserName, user.Password);

                client.Send(message);
                client.Disconnect(true);
            }
        }
               
        private static IEnumerable<MimeMessage> GetEmailFromInBox(MailServiceConnectionConfig config, UserCredentials user)
        {        
            var mail = new List<MimeMessage>();

            using (var client = new ImapClient())
            {
                // For demo-purposes, accept all SSL certificates
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(config.Host, config.Port,config.UseSsl);
                client.Authenticate(user.UserName, user.Password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                if (inbox.Count > 0)
                {
                    for (int i = 0; i < inbox.Count; i++)
                    {
                        var message = inbox.GetMessage(i);
                        mail.Add(message);
                    }
                }               

                client.Disconnect(true);
            }

            return mail;
        }
    }
}
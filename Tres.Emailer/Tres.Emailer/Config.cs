using System;
using System.Collections.Generic;
using System.Text;
using Tres.Emailer.Models;

namespace Tres.Emailer
{
    class Config
    {
        internal static MailServiceConnectionConfig GoogleSmtp()
        {
            var config = new MailServiceConnectionConfig()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseSsl = false
            };
            return config;
        }

        internal static MailServiceConnectionConfig ImapServer()
        {
            var config = new MailServiceConnectionConfig()
            {
                Host = "imap.gmail.com",
                Port = 993,
                UseSsl = true
            };
            return config;
        }
    }
}
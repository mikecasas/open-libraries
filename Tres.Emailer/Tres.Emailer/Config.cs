using System;
using System.Collections.Generic;
using System.Text;
using Tres.Emailer.Models;

namespace Tres.Emailer
{
    class Config
    {

        //m.To.Add(new MailboxAddress("Mike Casas", "mc@ynot.us"));
        //m.To.Add(new MailboxAddress("Mike Casas", "mkcsas0@gmail.com"));
        // from new MailboxAddress("YNOT Emailer", Config.EmailBox)

        internal const string EmailBox = "rcpv@ynot.us";
        internal const string Pw = "Mike@Casas2";
 
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
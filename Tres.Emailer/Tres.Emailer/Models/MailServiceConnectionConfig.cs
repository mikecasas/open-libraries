using System;
using System.Collections.Generic;
using System.Text;

namespace Tres.Emailer.Models
{
    class MailServiceConnectionConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }
}

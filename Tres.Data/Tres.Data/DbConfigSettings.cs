using System;
using System.Collections.Generic;
using System.Text;

namespace Tres.Data
{
    public class DbConfigSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string DbName { get; set; }

        public string GetConnectionString { get
            {
                return Config.GetConnectionString(Server, DbName, UserName, Password);
            } 
        }
    }
}

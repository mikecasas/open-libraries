using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Diagnostics;

namespace Tres.Data
{
    [DebuggerStepThrough]
    public static class Config
    {
        //public static string Connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public static string GetConnectionString(string sqlServer, string db, string userName, string pw)
        {
            return $"Data Source={sqlServer};Initial Catalog={db};Persist Security Info=True;User ID={userName};Password={pw}";
        }
    }
}
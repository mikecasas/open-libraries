//https://gist.github.com/jsauve/ffa2f0dc534aee3a3f16
// Yes, I know this doesn't fit definition of a generic repository, 
// but the assumption is that I have no idea how you want to get 
// your data. That's up to you. This Base repo exists for the 
// sole purpoose of providing SQL connection management.
using Dapper;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tres.UtilityString;

namespace Tres.Data
{
    public abstract class BaseRepository
    {
        protected readonly string _Conn;

        protected BaseRepository(string connectionString)
        {
            _Conn = connectionString;
        }

        // use for buffered queries that return a type
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var conn = new SqlConnection(_Conn))
                {
                    await conn.OpenAsync();
                    return await getData(conn);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        // use for buffered queries that do not return a type
        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            try
            {
                using (var conn = new SqlConnection(_Conn))
                {
                    await conn.OpenAsync();
                    await getData(conn);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        // use for non-buffered queries that return a type
        protected async Task<TResult> WithConnection<TRead, TResult>(Func<IDbConnection, Task<TRead>> getData, Func<TRead, Task<TResult>> process)
        {
            try
            {
                using (var conn = new SqlConnection(_Conn))
                {
                    await conn.OpenAsync();
                    var data = await getData(conn);
                    return await process(data);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        protected string InsertStatement(string tableName, string fields)
        {
            var sb = new StringBuilder();

            sb.Append("(" + fields + ") VALUES (");
            sb.Append(BuildParameters(fields) + ")");

            return $"INSERT INTO [{tableName}] {sb.ToString()};";
        }

        private string BuildParameters(string fields)
        {
            var items = fields.Split(',');
            StringBuilder sb = new StringBuilder();

            foreach (var item in items)
            {
                sb.Append("@" + item + ",");
            }

            return sb.ToString().TrimLast();
        }

        protected string InsertStatementWithIdentityReturn(string tableName, string fields)
        {
            //https://stackoverflow.com/questions/8270205/how-do-i-perform-an-insert-and-return-inserted-identity-with-dapper
                                 
            return $"{InsertStatement(tableName,fields)} SELECT CAST(SCOPE_IDENTITY() as int);";
        }
      
        private string TrimLastFive(string val)
        {
            var l = val.Length;
            return val.Substring(0, (l - 5));
        }

        protected string UpdateStatement(string tableName, string fields, string whereFields)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();

            var items1 = fields.Split(',');
            var items2 = whereFields.Split(',');

            foreach (var item1 in items1)
            {
                sb1.Append($"{item1}=@{item1},");
            }

            var ss1 = sb1.ToString();

            foreach (var item2 in items2)
            {
                sb2.Append($"{item2}=@{item2} AND ");
            }

            var ss2 = sb2.ToString();

            return $"UPDATE [{tableName}] SET {ss1.TrimLast()} WHERE {ss2.TrimFromBackN(5)};";
        }
    }
}
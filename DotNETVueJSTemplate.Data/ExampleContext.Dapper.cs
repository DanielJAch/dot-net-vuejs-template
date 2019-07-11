using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;

namespace DotNETVueJSTemplate.Data
{
    public partial class ExampleContext
    {
        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? timeout = null, CommandType? commandType = null)
        {
            if (this._logAllSqlCalls && this.Database.Log != null)
            {
                this.Database.Log.Invoke(sql);

                if (param != null)
                {
                    this.Database.Log.Invoke(JsonConvert.SerializeObject(param));
                }
            }

            return this.Database.Connection.QueryAsync<T>(sql, param, transaction, timeout, commandType);
        }
    }
}

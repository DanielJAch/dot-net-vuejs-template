using System.Configuration;

namespace DotNETVueJSTemplate.Api
{
    public static class Constants
    {
        public static readonly bool LogAllSqlCalls = bool.Parse(ConfigurationManager.AppSettings["LogAllSqlCalls"]);
        public static readonly string HttpRequestGuidKey = "HttpRequestGuidKey";
        public static readonly string ConnectionStringName = "ExampleDbContext";

        public class Routes
        {
            public static readonly string Api = "/api";
            public static readonly string SignalR = "/signalr";
        }

        public class Urls
        {
            public static readonly string ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            public static readonly string WebUrl = ConfigurationManager.AppSettings["WebUrl"];
        }
    }
}
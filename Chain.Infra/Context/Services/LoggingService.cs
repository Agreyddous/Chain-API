using Newtonsoft.Json.Linq;
using NLog;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Shared;
using Chain.Shared.Context.Enums;
using System;
using System.IO;
using System.Text;

namespace Chain.Infra.Context.Services
{
    public class LoggingService : ILoggingService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void Log(Type type, ELogType logType, string directory, ELogLevel levelType, object data)
        {
            string className = type.FullName;

            string level = GetLevel(levelType);

            Save(Newtonsoft.Json.JsonConvert.SerializeObject(new { Type = GetLogType(logType), Level = level, Class = className, Time = GetCurrentDateTime(), Data = data }), levelType);
        }

        public void Log(Type type, ELogType logType, string directory, ELogLevel levelType, string method, object data)
        {
            string className = type.FullName;

            string level = GetLevel(levelType);

            Save(Newtonsoft.Json.JsonConvert.SerializeObject(new { Type = GetLogType(logType), Level = level, Class = className, Method = method, Time = GetCurrentDateTime(), Data = data }), levelType);
        }

        public void Log(Type type, ELogType logType, ELogLevel levelType, object data)
        {
            string className = type.FullName;

            string level = GetLevel(levelType);

            Save(Newtonsoft.Json.JsonConvert.SerializeObject(new { Type = GetLogType(logType), Level = level, Class = className, Time = GetCurrentDateTime(), Data = data }), levelType);
        }

        public void Log(Type type, ELogType logType, ELogLevel levelType, object data, string method)
        {
            string className = type.FullName;

            string level = GetLevel(levelType);

            Save(Newtonsoft.Json.JsonConvert.SerializeObject(new { Type = GetLogType(logType), Level = level, Class = className, Method = method, Time = GetCurrentDateTime(), Data = data }), levelType);
        }

        public void Log(Type type, ELogType logType, string directory, ELogLevel levelType, object data, Exception exception)
        {
            string className = type.FullName;

            string level = GetLevel(levelType);

            Save(Newtonsoft.Json.JsonConvert.SerializeObject(new { Type = GetLogType(logType), Level = level, Class = className, Exception = new { exception.TargetSite.Name, Type = exception.TargetSite.GetType(), Parameters = exception.TargetSite.GetParameters(), exception.TargetSite.Attributes, exception.TargetSite.DeclaringType }, exception.StackTrace, exception.Message, Time = GetCurrentDateTime(), Data = data }), levelType);
        }

        public void Log(Type type, ELogType logType, ELogLevel levelType, object data, Exception exception)
        {
            string className = type.FullName;

            string level = GetLevel(levelType);

            Save(Newtonsoft.Json.JsonConvert.SerializeObject(new { Type = GetLogType(logType), Level = level, Class = className, Method = new { exception.TargetSite.Name, exception.TargetSite.DeclaringType }, exception.StackTrace, exception.Message, Time = GetCurrentDateTime(), Data = data }), levelType);
        }

        private string GetApplicationName(Type type) => type.Assembly.GetName().Name;

        private string GetLevel(ELogLevel levelType)
        {
            string level = "";

            switch (levelType)
            {
                case ELogLevel.Fatal:
                    level = "Fatal";
                    break;

                case ELogLevel.Error:
                    level = "Error";
                    break;

                case ELogLevel.Warn:
                    level = "Warn";
                    break;

                case ELogLevel.Info:
                    level = "Info";
                    break;

                case ELogLevel.Debug:
                    level = "Debug";
                    break;
            }

            return level;
        }

        private string GetLogType(ELogType logType)
        {
            string type = "";
            
            switch (logType)
            {
                case ELogType.Input:
                    type = "Input";

                    break;

                case ELogType.Output:
                    type = "Output";

                    break;

                case ELogType.Neutral:
                    type = "Neutral";

                    break;
            }

            return type;
        }

        private void Save(string data, ELogLevel level)
        {
            switch (level)
            {
                case ELogLevel.Info:
                    logger.Info(data);

                    break;

                case ELogLevel.Debug:
                    if (Settings.DetailedLog)
                        logger.Debug(data);

                    break;

                case ELogLevel.Warn:
                    logger.Warn(data);

                    break;

                case ELogLevel.Error:
                    logger.Error(data);

                    break;

                case ELogLevel.Fatal:
                    logger.Fatal(data);

                    break;
            }
        }

        private string GetCurrentDateTime() => DateTime.Now.ToString(@"MM/dd/yyyy HH:mm:ss.fff");
    }
}
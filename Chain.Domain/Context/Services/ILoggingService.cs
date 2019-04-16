using System;
using Chain.Shared.Context.Enums;

namespace Chain.Domain.Context.Services
{
    public interface ILoggingService
    {
        void Log(Type type, ELogType logType, string directory, ELogLevel levelType, object data);
        void Log(Type type, ELogType logType, string directory, ELogLevel levelType, string method, object data);
        void Log(Type type, ELogType logType, ELogLevel levelType, object data);
        void Log(Type type, ELogType logType, ELogLevel levelType, object data, string method);
        void Log(Type type, ELogType logType, string directory, ELogLevel levelType, object data, Exception exception);
        void Log(Type type, ELogType logType, ELogLevel levelType, object data, Exception exception);
    }
}
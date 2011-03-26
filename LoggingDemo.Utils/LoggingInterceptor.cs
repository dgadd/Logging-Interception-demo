using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Castle.DynamicProxy;
using log4net;

namespace LoggingDemo.Utils
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly ILog _logger;
        public LoggingInterceptor()
        {
            _logger = CreateLog4NetLogger();
        }

        public void Intercept(IInvocation invocation)
        {
            // capture input params before
            string parameterStrings = GetParameterStrings(invocation);
            string aopMethodCallDetails = string.Format("Method {0} | Params {1} | ThreadId {2}",
                invocation.Method.Name, parameterStrings,
                Thread.CurrentThread.ManagedThreadId);

            LogNow(invocation.Method.DeclaringType, aopMethodCallDetails);

            // don't catch method invocation errors; let them bubble up
            invocation.Proceed();

            // capture return type or exception afterwards
            var methodReturnDto = GetMethodReturnDTO(invocation);
            string aopMethodReturnTypeDetials = string.Format(
                "Method {0} | ReturnType {1} | ReturnValue {2} | ThreadId {3}",
                invocation.Method.Name, methodReturnDto.ReturnTypeName,
                methodReturnDto.ReturnValue, Thread.CurrentThread.ManagedThreadId);

            LogNow(invocation.Method.DeclaringType, aopMethodReturnTypeDetials);
        }

        private MethodReturnDTO GetMethodReturnDTO(IInvocation invocation)
        {
            try
            {
                return new MethodReturnDTO
                {
                    ReturnTypeName = invocation.Method.ReturnType.Name,
                    ReturnValue =
                        (invocation.ReturnValue != null)
                            ? invocation.ReturnValue.ToString()
                            : "null"
                };

            }
            catch (Exception ex)
            {
                string errorMsg =
                    string.Format("LoggingInterceptor.GetMethodReturnDTO() threw an error: {0}; StackTrace: {1}",
                                  ex.Message, ex.StackTrace);
                LogNow(invocation.Method.DeclaringType, errorMsg);
                return new MethodReturnDTO { ReturnTypeName = "retrieve failed", ReturnValue = "retrieve failed" };
            }
        }

        private string GetParameterStrings(IInvocation invocation)
        {
            string parameterStrings = "";

            try
            {
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    var parameterInfo = invocation.GetConcreteMethod().GetParameters()[i];
                    var parameterValue = invocation.Arguments[i];
                    var detailString = (parameterValue != null) ? parameterValue.ToString() : "null";
                    parameterStrings += (string.Format("[name:{0}, type:{1}, value:{2}] ",
                                                       parameterInfo.Name, parameterInfo.ParameterType.Name,
                                                       detailString.Replace("\r\n", " ")));
                }
            }
            catch (Exception ex)
            {
                string errorMsg = string.Format("LoggingInterceptor.GetParameterStrings() threw an error: {0}; StackTrace: {1}",
                                                            ex.Message, ex.StackTrace);
                LogNow(invocation.Method.DeclaringType, errorMsg);
            }
            return parameterStrings;
        }

        public void LogNow(Type type, string logMessage)
        {
            if (_logger.IsInfoEnabled) 
                _logger.Info(string.Format("Type: {0} [{1}]", type.Name, logMessage));
        }

        private static ILog CreateLog4NetLogger()
        {
            var method = new StackTrace().GetFrame(1).GetMethod();
            var log = log4net.LogManager.GetLogger(method.DeclaringType);

            if (log4net.LogManager.GetRepository().Configured == false)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Debug("XmlConfigurator now configured");
            }

            log.DebugFormat(CultureInfo.InvariantCulture, "Logging {0}", log.Logger.Name);

            return log;
        }

    }
}

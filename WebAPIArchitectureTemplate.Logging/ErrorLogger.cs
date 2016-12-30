using System;
using System.Diagnostics;

namespace WebAPIArchitectureTemplate.Logging
{
    public static class ErrorLogger
    {
        public static void LogError(Exception exception) => Debug.WriteLine(exception);

        public static void LogMessage(string messsage) => Debug.WriteLine(messsage);
    }

}
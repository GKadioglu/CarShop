using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger() : base(name: "JsonFileLogger")
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LogDetailWithException: LogDetail
    {
         public string ExceptionMessage { get; set; }
    }
}
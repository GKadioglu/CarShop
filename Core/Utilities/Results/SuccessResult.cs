using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {

        public SuccessResult(string message) : base(success:true, message)
        {
        }
        public SuccessResult() : base(true)
        {
        }

    }
}
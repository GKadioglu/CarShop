using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)] // burada, aşağıdaki yapının nasıl ve nerelerde kullanılabileceğini tanımlıyoruz. Örneğin class'larda, metotlarda kullanılabilir. Ve çoklu kullanımlarda, true olarak işaretledik. Inteherit de true dedik.
    public abstract class MethodInterceptionBaseAttirbute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
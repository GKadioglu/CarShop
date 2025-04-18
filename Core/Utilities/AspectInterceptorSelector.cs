using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Interceptors;

namespace Core.Utilities
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttirbute>(inherit: true).ToList();

            var methodAttributes = type.GetMethod(method.Name)
                ?.GetCustomAttributes<MethodInterceptionBaseAttirbute>(inherit: true)
                ?? Enumerable.Empty<MethodInterceptionBaseAttirbute>(); // Null kontrolü

            classAttributes.AddRange(methodAttributes);

            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
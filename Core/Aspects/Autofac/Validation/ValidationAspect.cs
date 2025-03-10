using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = Activator.CreateInstance(_validatorType);
            if (validator is null)
            {
                throw new InvalidOperationException("Validator instance could not be created.");
            }

            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                var genericValidator = validator as IValidator<object>;
                if (genericValidator == null)
                {
                    throw new InvalidOperationException($"Validator type {_validatorType.Name} is not compatible with IValidator<object>.");
                }
                ValidationTool.Validate(genericValidator, entity);
            }
        }
    }
}
using GenerateUmbracoDocTypeModels.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GenerateUmbracoDocTypeModels.Helpers
{
    public static class InitializationHelper
    {
        public static TResult EnsureTarget<TResult>(TResult target)
            where TResult : BaseElementModel
        {
            if (target == null)
            {
                target = InitializeType<TResult>();
            }
            return target;
        }

        public static TResult InitializeType<TResult>()
            where TResult : BaseElementModel
        {
            ConstructorInfo ctor = typeof(TResult).GetConstructors().First();
            ObjectActivator<TResult> activator = GetActivator<TResult>(ctor);

            return activator();
        }

        public static object InitializeType(Type type)
        {
            if (!type.IsAssignableFrom(typeof(BaseElementModel)))
            {
                throw new InvalidOperationException("Attempt to initialize to invalid type");
            }

            var method =
                typeof(InitializationHelper)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(InitializationHelper.InitializeType) && m.IsGenericMethod);

            var genericMethod = method.MakeGenericMethod(type);
            var result = genericMethod.Invoke(null, new object[0]);

            return result;
        }

        private delegate T ObjectActivator<T>(params object[] args);

        private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda =
                Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }
    }
}

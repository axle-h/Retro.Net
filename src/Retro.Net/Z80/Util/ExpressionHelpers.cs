using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Retro.Net.Z80.Util
{
    /// <summary>
    /// Extension methods for expression trees.
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// Gets the property expression defined by the specified lambda and instance expressions.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyLambda">The property lambda.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public static MemberExpression GetPropertyExpression<TSource, TProperty>(this Expression instance,
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof (TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
            }

            var typeInfo = type.GetTypeInfo();
            var propertyTypeInfo = propInfo.PropertyType.GetTypeInfo();

            if (type != propInfo.DeclaringType && !typeInfo.IsSubclassOf(propInfo.DeclaringType) && !propertyTypeInfo.IsAssignableFrom(type))
            {
                throw new ArgumentException($"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");
            }

            return Expression.Property(instance, propInfo);
        }

        /// <summary>
        /// Gets the method information of the method called by <see cref="methodLambda"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TArg">The type of the argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="methodLambda">The method lambda.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Expression. Expression should consist of a Method call only.</exception>
        public static MethodInfo GetMethodInfo<TSource, TArg, TResult>(Expression<Func<TSource, TArg, TResult>> methodLambda)
        {
            var outermostExpression = methodLambda.Body as MethodCallExpression;

            if (outermostExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return outermostExpression.Method;
        }

        /// <summary>
        /// Gets the method information of the method called by <see cref="methodLambda"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TArg1">The type of the arg1.</typeparam>
        /// <typeparam name="TArg2">The type of the arg2.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="methodLambda">The method lambda.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Expression. Expression should consist of a Method call only.</exception>
        public static MethodInfo GetMethodInfo<TSource, TArg1, TArg2, TResult>(
            Expression<Func<TSource, TArg1, TArg2, TResult>> methodLambda)
        {
            var outermostExpression = methodLambda.Body as MethodCallExpression;

            if (outermostExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return outermostExpression.Method;
        }

        /// <summary>
        /// Gets the method information of the method called by <see cref="methodLambda"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TArg1">The type of the arg1.</typeparam>
        /// <typeparam name="TArg2">The type of the arg2.</typeparam>
        /// <typeparam name="TArg3">The type of the arg3.</typeparam>
        /// <param name="methodLambda">The method lambda.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Expression. Expression should consist of a Method call only.</exception>
        public static MethodInfo GetMethodInfo<TSource, TArg1, TArg2, TArg3>(
            Expression<Action<TSource, TArg1, TArg2, TArg3>> methodLambda)
        {
            var outermostExpression = methodLambda.Body as MethodCallExpression;

            if (outermostExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return outermostExpression.Method;
        }

        /// <summary>
        /// Gets the method information of the method called by <see cref="methodLambda"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TArg1">The type of the arg1.</typeparam>
        /// <typeparam name="TArg2">The type of the arg2.</typeparam>
        /// <param name="methodLambda">The method lambda.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Expression. Expression should consist of a Method call only.</exception>
        public static MethodInfo GetMethodInfo<TSource, TArg1, TArg2>(Expression<Action<TSource, TArg1, TArg2>> methodLambda)
        {
            var outermostExpression = methodLambda.Body as MethodCallExpression;

            if (outermostExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return outermostExpression.Method;
        }

        /// <summary>
        /// Gets the method information of the method called by <see cref="methodLambda"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TArg1">The type of the arg1.</typeparam>
        /// <param name="methodLambda">The method lambda.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Expression. Expression should consist of a Method call only.</exception>
        public static MethodInfo GetMethodInfo<TSource, TArg1>(Expression<Action<TSource, TArg1>> methodLambda)
        {
            var outermostExpression = methodLambda.Body as MethodCallExpression;

            if (outermostExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return outermostExpression.Method;
        }

        /// <summary>
        /// Gets the method information of the method called by <see cref="methodLambda"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="methodLambda">The method lambda.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Expression. Expression should consist of a Method call only.</exception>
        public static MethodInfo GetMethodInfo<TSource>(Expression<Action<TSource>> methodLambda)
        {
            var outermostExpression = methodLambda.Body as MethodCallExpression;

            if (outermostExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return outermostExpression.Method;
        }
    }
}
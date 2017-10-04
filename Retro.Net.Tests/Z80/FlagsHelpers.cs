using System;
using System.Linq.Expressions;
using System.Reflection;
using Moq;
using Retro.Net.Z80.Registers;
using Shouldly;

namespace Retro.Net.Tests.Z80
{
    internal static class FlagsHelpers
    {
        public static void VerifyFlag(Mock<IFlagsRegister> flags, Expression<Func<IFlagsRegister, bool>> propertyLambda, bool? value)
        {
            var flagsExpression = Expression.Parameter(typeof(IFlagsRegister), "flags");
            var property = GetPropertyExpression(flagsExpression, propertyLambda);
            var getLambda = Expression.Lambda<Func<IFlagsRegister, bool>>(property, flagsExpression);

            if (!value.HasValue)
            {
                flags.Verify(getLambda, Times.Never);
                return;
            }

            var setExpression = Expression.Assign(property, Expression.Constant(value.Value));
            var setLambda = Expression.Lambda<Action<IFlagsRegister>>(setExpression, flagsExpression);

            flags.VerifySet(setLambda.Compile(), Times.AtLeastOnce);

            var flag = getLambda.Compile()(flags.Object);
            if (value.Value)
            {
                flag.ShouldBeTrue(getLambda.ToString());
                return;
            }

            flag.ShouldBeFalse(getLambda.ToString());
        }

        public static MemberExpression GetPropertyExpression<TSource, TProperty>(Expression instance,
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
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

            return Expression.Property(instance, propInfo);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Retro.Net.Tests.Util
{
    public static class RngFactory
    {
        private const int MinimumCollectionSize = 2;
        private const int MaximumCollectionSize = 8;

        public static Func<TObject> Build<TObject>()
        {
            var objectFactoryExpression = NextFactory(typeof(TObject), new RecursionGuard());
            var lambda = Expression.Lambda<Func<TObject>>(objectFactoryExpression, Enumerable.Empty<ParameterExpression>());
            return lambda.Compile();
        }

        private static Expression NextFactory(Type type, RecursionGuard recursion)
        {
            if (recursion.TypeInRecursionStack(type))
            {
                // End the recursion.
                return Expression.Constant(null, type);
            }

            if (TryGetTypeFactory(type, out var tf))
            {
                return tf;
            }

            if (TryGetDictionaryFactory(type, recursion, out var df))
            {
                return df;
            }

            if (TryGetListFactory(type, recursion, out var lf))
            {
                return lf;
            }

            if (TryGetCollectionFactory(type, recursion, out var af))
            {
                return af;
            }

            var nextRecursion = new RecursionGuard(recursion, type);

            var defaultContructor = type.GetConstructor(Type.EmptyTypes);
            if (defaultContructor != null)
            {
                var properties = type.GetProperties().Where(x => x.CanWrite).ToArray();
                if (properties.Any())
                {
                    // We have an empty constructor and some public proeprties lets just call that and set... them.
                    var propertyBindings = properties
                        .Select(p => (MemberBinding)Expression.Bind(p, NextFactory(p.PropertyType, nextRecursion)))
                        .ToArray();
                    return Expression.MemberInit(Expression.New(type), propertyBindings);
                }
            }

            var (constructor, parameters) = type.GetConstructors()
                .Select(c => (constructor: c, parameters: c.GetParameters()))
                .OrderByDescending(x => x.parameters.Length)
                .FirstOrDefault();

            if (constructor == null)
            {
                throw new ArgumentException("No public constructors: " + type, nameof(type));
            }

            // Use constructor... it's probably there for a reason.
            var parameterExpressions = parameters.Select(p => NextFactory(p.ParameterType, nextRecursion)).ToArray();
            return Expression.New(constructor, parameterExpressions);
        }

        private static bool TryGetDictionaryFactory(Type type, RecursionGuard recursion, out Expression f)
        {
            var (key, value) = GetDictionaryElementTypes(type);
            if (key == null || value == null)
            {
                f = null;
                return false;
            }

            var nextRecursion = new RecursionGuard(recursion, type);
            var keyFactory = NextFactory(key, nextRecursion);
            var valueFactory = NextFactory(value, nextRecursion);

            var dictionaryType = typeof(Dictionary<,>).MakeGenericType(key, value);
            var addMethod = dictionaryType.GetMethod("Add");

            var size = Rng.Int(MinimumCollectionSize, MaximumCollectionSize);
            var entries = Enumerable.Range(0, size).Select(_ => Expression.ElementInit(addMethod, keyFactory, valueFactory)).ToArray();

            f = Expression.ListInit(Expression.New(dictionaryType), entries);
            return true;
        }

        private static bool TryGetListFactory(Type type, RecursionGuard recursion, out Expression f)
        {
            var elementType = IsGenericType(type, typeof(List<>)) ? type.GenericTypeArguments.FirstOrDefault() : null;
            if (elementType == null)
            {
                f = null;
                return false;
            }

            var nextRecursion = new RecursionGuard(recursion, type);
            var entryFactory = NextFactory(elementType, nextRecursion);
            var addMethod = type.GetMethod("Add");
            var size = Rng.Int(MinimumCollectionSize, MaximumCollectionSize);
            var entries = Enumerable.Range(0, size).Select(_ => Expression.ElementInit(addMethod, entryFactory)).ToArray();

            f = Expression.ListInit(Expression.New(type), entries);
            return true;
        }

        private static bool TryGetCollectionFactory(Type type, RecursionGuard recursion, out Expression f)
        {
            var elementType = GetCollectionElementType(type);
            if (elementType == null)
            {
                f = null;
                return false;
            }

            var nextRecursion = new RecursionGuard(recursion, type);
            var elementTypeFactory = NextFactory(elementType, nextRecursion);
            var size = Rng.Int(MinimumCollectionSize, MaximumCollectionSize);
            f = Expression.NewArrayInit(elementType, Enumerable.Repeat(elementTypeFactory, size));

            return true;
        }

        private static Type GetCollectionElementType(Type type)
        {
            if (type.IsArray && type.HasElementType)
            {
                return type.GetElementType();
            }

            if (IsGenericInterface(type, typeof(IEnumerable<>)))
            {
                return type.GenericTypeArguments.FirstOrDefault();
            }

            return type.GetInterfaces()
                .FirstOrDefault(t => IsGenericType(t, typeof(IEnumerable<>)))
                ?.GenericTypeArguments
                .FirstOrDefault();
        }

        private static (Type key, Type value) GetDictionaryElementTypes(Type type)
        {
            if (IsGenericInterface(type, typeof(IDictionary<,>)))
            {
                var genericTypes = type.GenericTypeArguments;
                return (genericTypes[0], genericTypes[1]);
            }

            var interfaceTypes = type.GetInterfaces()
                                     .FirstOrDefault(t => IsGenericType(t, typeof(IDictionary<,>)))
                                     ?.GenericTypeArguments ?? Type.EmptyTypes;

            if (interfaceTypes.Length == 2)
            {
                return (interfaceTypes[0], interfaceTypes[1]);
            }

            return default((Type, Type));
        }

        private static bool IsGenericInterface(Type type, Type iface)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsInterface && typeInfo.IsGenericType && type.GetGenericTypeDefinition() == iface;
        }

        private static bool IsGenericType(Type type, Type genericType)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }

        private static MethodInfo GetRngMethod(string name) => typeof(Rng).GetMethod(name, BindingFlags.Static | BindingFlags.Public);

        private static Expression CallPrimitiveRngMethod<TPrimtive>(string name, TPrimtive min, TPrimtive max)
        {
            var method = GetRngMethod(name);
            return Expression.Call(null, method, Expression.Constant(min), Expression.Constant(max));
        }

        private static bool TryGetTypeFactory(Type type, out Expression f)
        {
            if (type == typeof(long))
            {
                f = CallPrimitiveRngMethod(nameof(Rng.Long), long.MinValue, long.MaxValue);
                return true;
            }

            if (type == typeof(int))
            {
                f = CallPrimitiveRngMethod(nameof(Rng.Int), int.MinValue, int.MaxValue);
                return true;
            }

            if (type == typeof(short))
            {
                f = CallPrimitiveRngMethod(nameof(Rng.Short), short.MinValue, short.MaxValue);
                return true;
            }

            if (type == typeof(sbyte))
            {
                f = CallPrimitiveRngMethod(nameof(Rng.SByte), sbyte.MinValue, sbyte.MaxValue);
                return true;
            }

            if (type == typeof(ulong))
            {
                f = CallPrimitiveRngMethod(nameof(Rng.UnsignedLong), ulong.MinValue, ulong.MaxValue);
                return true;
            }

            if (type == typeof(uint))
            {
                f = CallPrimitiveRngMethod(nameof(Rng.UnsignedInt), uint.MinValue, uint.MaxValue);
                return true;
            }

            if (type == typeof(ushort))
            {
                f = CallPrimitiveRngMethod(nameof(Rng.Word), ushort.MinValue, ushort.MaxValue);
                return true;
            }

            if (type == typeof(byte))
            {
                f = CallPrimitiveRngMethod(nameof(Byte), byte.MinValue, byte.MaxValue);
                return true;
            }

            // Cheeky shortcut for array of bytes.
            if (type == typeof(byte[]))
            {
                var sizeMethodCall = CallPrimitiveRngMethod(nameof(Rng.Int), 8, 64);
                f = Expression.Call(null, GetRngMethod(nameof(Rng.Bytes)), sizeMethodCall);
                return true;
            }

            if (type == typeof(bool))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.Boolean)));
                return true;
            }

            if (type == typeof(char))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.Char)));
                return true;
            }

            if (type == typeof(double))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.Double)));
                return true;
            }

            if (type == typeof(float))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.Float)));
                return true;
            }

            if (type == typeof(decimal))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.Decimal)));
                return true;
            }

            if (type.GetTypeInfo().IsEnum)
            {
                f = Expression.Convert(Expression.Call(null, GetRngMethod(nameof(Rng.Enum)), Expression.Constant(type)), type);
                return true;
            }

            if (type == typeof(string))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.String)));
                return true;
            }

            if (type == typeof(Guid))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.Guid)));
                return true;
            }

            if (type == typeof(DateTime))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.DateTime)));
                return true;
            }

            if (type == typeof(DateTimeOffset))
            {
                f = Expression.Call(null, GetRngMethod(nameof(Rng.DateTimeOffset)));
                return true;
            }

            f = null;
            return false;
        }
    }
}
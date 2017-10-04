using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Util
{
    public abstract class RngFactorySpec<TObject>
    {
        protected readonly ICollection<TObject> Objects;

        protected RngFactorySpec()
        {
            var factory = RngFactory.Build<TObject>();
            Objects = Enumerable.Range(0, 10).Select(_ => factory()).ToArray();
        }

        [Fact] public void it_should_generate_non_empty_array() => Objects.ShouldNotBeEmpty();

        [Fact] public void all_test_objects_should_not_be_null() => Objects.ShouldAllBe(x => x != null);

        [Fact] public void there_should_not_be_loads_of_defaults() => Objects.ShouldAllBe(x => HasSomeNonDefaultProperties(x));

        private static bool HasSomeNonDefaultProperties(TObject value)
        {
            var properties = typeof(TObject).GetProperties().Where(x => x.CanRead).ToArray();
            if (!properties.Any())
            {
                return true;
            }

            var defaultPropertyCount = properties.Count(p => IsDefault(p.PropertyType, p.GetValue(value)));
            return defaultPropertyCount < properties.Length / 2;
        }

        private static bool IsDefault(Type type, object o) => type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type).Equals(o) : o == null;
    }

    public class When_generating_value_type : RngFactorySpec<int>
    {
    }

    public class When_generating_simple_poco : RngFactorySpec<SimplePoco>
    {
    }

    public class When_generating_simple_poco_with_constructor : RngFactorySpec<SimplePocoWithConstructor>
    {
    }

    public class When_generating_poco_with_collections : RngFactorySpec<PocoWithCollections>
    {
    }
}
using System;
using Autofac.Core;
using Autofac.Extras.Moq;
using Moq;
using Shouldly;

namespace Retro.Net.Tests.Util
{

    public abstract class WithSubject<TSubject> : IDisposable
        where TSubject : class
    {
        private readonly AutoMock _mock;
        private readonly Lazy<TSubject> _subject;
        private Exception _constructionException;

        protected WithSubject()
        {
            _mock = AutoMock.GetLoose();
            _subject = new Lazy<TSubject>(() =>
            {
                try
                {
                    return _mock.Create<TSubject>();
                }
                catch (DependencyResolutionException e)
                {
                    _constructionException = e.GetBaseException();
                    return null;
                }
            });
        }

        protected TSubject Subject => _subject.Value;

        protected TException ConstructionShouldThrow<TException>() where TException : Exception
        {
            var subject = _subject.Value;
            _constructionException.ShouldBeOfType<TException>();
            return _constructionException as TException;
        }

        protected void ConstructionShouldNotThrow()
        {
            var subject = _subject.Value;
            _constructionException.ShouldBeNull();
        }

        protected Mock<TDependency> The<TDependency>() where TDependency : class => _mock.Mock<TDependency>();
        
        protected void Register<TDependency>(TDependency dependency) where TDependency : class => _mock.Provide(dependency);

        public virtual void Dispose() => _mock.Dispose();
    }
}
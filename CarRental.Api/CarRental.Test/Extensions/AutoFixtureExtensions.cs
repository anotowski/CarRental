using AutoFixture;
using Moq;

namespace CarRental.Test.Extensions
{
    public static class AutoFixtureExtensions
    {
        public static Mock<T> FreezeMoq<T>(this Fixture fixture) where T : class
        {
            var mock = new Mock<T>();
            fixture.Inject(mock.Object);
            return mock;
        }
    }
}

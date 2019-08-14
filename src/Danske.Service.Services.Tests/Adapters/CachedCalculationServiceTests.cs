using Danske.Service.Core;
using Danske.Service.Services.Adapters;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;

namespace Danske.Service.Services.Tests.Adapters
{
    namespace CalculationServiceTests
    {
        public class CachedCalculationServiceTests
        {
            protected CachedCalculationService Sut;
            protected Mock<ICalculationService> CalculationServiceMock;
            protected Mock<IMemoryCache> MemoryCacheMock;

            [SetUp]
            public void GivenTheSut()
            {
                CalculationServiceMock = new Mock<ICalculationService>();
                MemoryCacheMock = new Mock<IMemoryCache>();

                Sut = new CachedCalculationService(CalculationServiceMock.Object, MemoryCacheMock.Object);
            }
        }

        [TestFixture]
        public class TraverseTests : CachedCalculationServiceTests
        {
            private Graph _graph;

            [SetUp]
            public void SetupTheScenario()
            {
                _graph = new Graph
                {
                    IsTraversable = true,
                    Path = new [] {0, 2, 4}
                };
                CalculationServiceMock.Setup(m => m.Traverse(It.IsAny<int[]>())).Returns(_graph);

                // Mocking methods called by extension
                object value;
                var cacheEntryMock = new Mock<ICacheEntry>();
                MemoryCacheMock.Setup(m => m.TryGetValue(It.IsAny<object>(), out value)).Returns(false);
                MemoryCacheMock.Setup(m => m.CreateEntry(It.IsAny<string>())).Returns(cacheEntryMock.Object);
            }

            [Test]
            public void ItShouldCalculationServiceTraversalMethod()
            {
                var input = new[] { 1, 2, 3, 4 };
                var response = Sut.Traverse(input);

                // Checking if it was called with correct params and with any params only once
                CalculationServiceMock.Verify(m => m.Traverse(It.IsAny<int[]>()), Times.Once);
                CalculationServiceMock.Verify(m => m.Traverse(It.Is((int[] path) => path.Equals(input))), Times.Once);
                response.Should().Be(_graph);
            }

            [Test]
            public void ItShouldOnlyCallCalculationServiceTraversalMethodOnceIfItemIsCached()
            {
                object value;
                MemoryCacheMock.SetupSequence(m => m.TryGetValue(It.IsAny<object>(), out value))
                    .Returns(false)
                    .Returns(true);

                var input = new[] { 1, 2, 3, 4 };
                var response = Sut.Traverse(input);

                // Checking if it was called with correct params and with any params only once
                CalculationServiceMock.Verify(m => m.Traverse(It.IsAny<int[]>()), Times.Once);
                CalculationServiceMock.Verify(m => m.Traverse(It.Is((int[] path) => path.Equals(input))), Times.Once);
                response.Should().Be(_graph);
            }
        }
    }
}
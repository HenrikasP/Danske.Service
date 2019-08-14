using FluentAssertions;
using NUnit.Framework;

namespace Danske.Service.Services.Tests
{
    namespace CalculationServiceTests
    {
        public class CalculationServiceTests
        {
            protected CalculationService Sut;

            [SetUp]
            public void GivenTheSut()
            {
                Sut = new CalculationService();
            }
        }

        [TestFixture]
        public class TraverseTests : CalculationServiceTests
        {
            [TestCase(new [] { 1, 2, 0, 3, 0, 2, 0 }, new [] {0, 1, 3, 6})]
            [TestCase(new [] { 2, 1, 3, 4, 2, 1, 0 }, new [] {0, 2, 5, 6})]
            [TestCase(new [] { 3, 5, 6, 1, 1, 1, 0, 0 }, new [] {0, 2, 7})]
            [TestCase(new [] { 2, 1, 4, 5, 2, 1, 0 }, new [] {0, 2, 6})]
            [TestCase(new[] { 0 }, new[] { 0 })]
            public void ItShouldMapGraphCorrectly(int [] input, int [] path)
            {
                var response = Sut.Traverse(input);

                response.IsTraversable.Should().Be(true);
                response.Path.Should().BeEquivalentTo(path);
            }

            [TestCase(new [] { 1, 1, 1, 1, 0, 0 })]
            [TestCase(new [] { 3, 1, 1, 0, 1 })]
            [TestCase(new[] { 1, 2, 0, 1, 0, 2, 0 })]
            public void ItShouldMapBadGraphCorrectly(int [] input)
            {
                var response = Sut.Traverse(input);

                response.IsTraversable.Should().Be(false);
            }
        }
    }
}
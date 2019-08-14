using Danske.Service.Host.Mappers;
using FluentAssertions;
using NUnit.Framework;

namespace Danske.Service.Host.Tests.Mappers
{
    namespace ModulesOperationTests
    {
        public class GraphMapperTests
        {
            protected GraphMapper Sut;

            [SetUp]
            public void GivenTheSut()
            {
                Sut = new GraphMapper();
            }
        }

        [TestFixture]
        public class MapTests : GraphMapperTests
        {
            [Test]
            public void ItShouldMapGraphCorrectly()
            {
                var graph = new Core.Graph
                {
                    IsTraversable = true,
                    Path = new []{ 1,3,5,7,9}
                };

                var response = Sut.Map(graph);

                response.IsTraversable.Should().Be(graph.IsTraversable);
                response.Path.Should().BeEquivalentTo(graph.Path);
            }
        }
    }
}
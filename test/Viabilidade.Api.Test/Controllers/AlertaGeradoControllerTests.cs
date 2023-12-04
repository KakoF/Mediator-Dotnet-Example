using Bogus;
using Castle.Core.Resource;
using MediatR;
using Moq;
using System.Collections.Generic;
using Viabilidade.API.Controllers;
using Viabilidade.Application.Commands.Alert.Alert.GetAll;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Api.Tests.Controllers
{
    public class AlertaGeradoControllerTests
    {
        private readonly AlertController _sut;
        private readonly MockRepository _mock;
        private readonly Mock<IMediator> _mediator;

        public AlertaGeradoControllerTests()
        {
            _mock = new MockRepository(MockBehavior.Loose);
            _mediator = _mock.Create<IMediator>();
            _sut = new AlertController(_mediator.Object);
        }

        [Fact]
        public async Task GetAsync_Should_Return_List_AlertaGeradoAsync()
        {
            // Arrange 
            var dummyData = new Faker<AlertModel>()
                .RuleFor(c => c.Id, f => f.IndexFaker);
            var list = dummyData.Generate(10);

            _mediator.Setup(m => m.Send(It.IsAny<GetAllRequest>(), default(CancellationToken))).ReturnsAsync(list);

            // Act
            var result = await _sut.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<AlertModel>>(result);
        }

        [Fact]
        public async Task GetAsync_Should_Return_EmptyList()
        {
            // Arrange 
            var dummyData = new Faker<AlertModel>()
                .RuleFor(c => c.Id, f => f.IndexFaker);
            var list = dummyData.Generate(0);

            _mediator.Setup(m => m.Send(It.IsAny<GetAllRequest>(), default(CancellationToken))).ReturnsAsync(list);

            // Act
            var result = await _sut.GetAsync();

            // Assert
            Assert.Empty(result);
            Assert.IsType<List<AlertModel>>(result);
        }
    }
}

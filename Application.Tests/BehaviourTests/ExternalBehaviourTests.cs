using Application.Behavior;
using Application.Permisos.Create;
using Domain.Shared;
using Infraestructure.ElasticSearch;
using Infraestructure.Kafka;
using MediatR;

namespace Application.Tests.BehaviourTests;

public class ExternalBehaviourTests
{
    private Mock<IElasticService> MoqElasticService { get; set; } = new();
    private Mock<IKafkaProducerService> MoqKafkaService { get; set; } = new();
    private ExternalBehaviour<CreatePermisoCommand, Result> Behavior { get; set; }
    
    public ExternalBehaviourTests()
    {
         Behavior = new ExternalBehaviour<CreatePermisoCommand, Result>(MoqElasticService.Object, MoqKafkaService.Object);
    }

    [Fact]
    public async Task Handle_ShouldInvokeGlobalActions()
    {
        var mockNext = new Mock<RequestHandlerDelegate<Result>>();
        var returnResult = new Result(It.IsAny<object>());
        var request = new CreatePermisoCommand(
            "name",
            "surname",
            1,
            DateTime.Now
            );
        
        mockNext.Setup(next => next())
            .ReturnsAsync(returnResult);

        var result = await Behavior.Handle(request, mockNext.Object, default);
        
        Assert.Equal(returnResult, result);
        mockNext.Verify(next=>next(), Times.Once);

    }
}
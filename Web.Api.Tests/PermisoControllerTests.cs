using Application.Permisos.Create;
using Domain.Permisos;
using Domain.TipoPermiso;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Web.Api.Controllers;
using Result = Domain.Shared.Result;

namespace Web.Api.Tests
{
    public class PermisoControllerTests
    {
        private Mock<IMediator> MoqMediator { get; set; } = new();
        private Mock<IElasticClient> MoqElasticClient { get; set; } = new();
        private PermisoController Controller { get; set; }

        public PermisoControllerTests()
        {
            Controller = new PermisoController(MoqMediator.Object, MoqElasticClient.Object);
        }

        [Fact]
        public async Task PermisoController_CreatePermiso_OK()
        {
            //arrange
            var request = new CreatePermisoCommand(
               "OK",
               "Surname",
               1,
               DateTime.Now);

            var moqResult = new Result(new Permiso("", "", DateTime.Now, new TipoPermiso()));
            MoqMediator.Setup(
                    x => x.Send(request, It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(moqResult);
            
            var elasticResponse = new Mock<IndexResponse>();
            elasticResponse.Setup(x=>x.IsValid).Returns(true);
            MoqElasticClient.Setup(
                x => x.IndexDocumentAsync(It.IsAny<object>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync(elasticResponse.Object);
            
            //act
            var result = await Controller.Create(request);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task PermisoController_CreatePermiso_BadRequest()
        {
            //arrange
            var request = new CreatePermisoCommand(
               "Bad",
               "Surname",
               1,
               DateTime.Now);

            var moqResult = new Result("X");

            MoqMediator.Setup(
                x => x.Send(request, It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(moqResult);

            //act
            var result = await Controller.Create(request);

            //assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}
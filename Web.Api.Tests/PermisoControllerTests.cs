using Application.Permisos.Create;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Controllers;

namespace Web.Api.Tests
{
    public class PermisoControllerTests
    {
        [Fact]
        public async Task PermisoController_CreatePermiso_OK()
        {
            //arrage
            var request = new CreatePermisoCommand(
               "Name",
               "Surname",
               1,
               DateTime.Now);

            var moqResult = new Result(It.IsAny<object>());

            var service = new Mock<IMediator>();
            service.Setup(
                x => x.Send(It.IsAny<CreatePermisoCommand>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(moqResult);

            var controller = new PermisoController(service.Object);

            //act
            var result = await controller.Create(request);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task PermisoController_CreatePermiso_BadRequest()
        {
            //arrage
            var request = new CreatePermisoCommand(
               "Name",
               "Surname",
               1,
               DateTime.Now);

            var moqResult = new Result("");

            var service = new Mock<IMediator>();
            service.Setup(
                x => x.Send(It.IsAny<CreatePermisoCommand>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(moqResult);

            var controller = new PermisoController(service.Object);

            //act
            var result = await controller.Create(request);

            //assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}